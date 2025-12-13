using EsapiService.Generators.Contexts;
using System.Text;

namespace EsapiService.Generators.Generators.Wrappers;

public static class OutParameterMethodGenerator {
    public static string Generate(OutParameterMethodContext member) {
        var sb = new StringBuilder();

        // 1. Input Args
        var inputArgs = member.Parameters
            .Where(p => !p.IsOut)
            .Select(p => $"{p.InterfaceType} {p.Name}");

        sb.AppendLine($"        public async Task<{member.ReturnTupleSignature}> {NamingConvention.GetMethodName(member.Name)}({string.Join(", ", inputArgs)})");
        sb.AppendLine("        {");
        sb.AppendLine($"            var postResult = await _service.PostAsync(context => {{");

        // 2. Prepare Temp Variables
        foreach (var p in member.Parameters) {
            if (p.IsOut) {
                sb.AppendLine($"                {p.Type} {p.Name}_temp = default({p.Type});");
                continue;
            }

            if (!p.IsRef ) { continue; } // if not ref, skip the rest

            string valueSource = string.Empty;

            if (p.IsCollection) {
                if (p.IsWrappable) {
                    valueSource = $"{p.Name}?.Select(x => ((IEsapiWrapper<{p.InnerType}>)x).Inner).ToList()"; 
                }
                else {
                    valueSource = $"(({p.WrapperType}){p.Name})._inner";
                }

                sb.AppendLine($"                {p.Type} {p.Name}_temp = {valueSource};");
                continue;
            }

            if (p.IsWrappable) {
                sb.AppendLine($"                {p.Type} {p.Name}_temp = (({p.WrapperType}){p.Name})._inner;");
                continue;
            }

            valueSource = p.IsWrappable ? $"{p.Name}" : p.Name;
            sb.AppendLine($"                {p.Type} {p.Name}_temp = {valueSource};");
        }

        // 3. Build the Call String
        var callArgs = member.Parameters.Select(p => {
            if (p.IsOut) return $"out {p.Name}_temp";
            if (p.IsRef) return $"ref {p.Name}_temp";
            //return p.IsWrappable ? $"value" : p.Name;
            return p.IsWrappable ? $"(({p.WrapperType}){p.Name})._inner" : p.Name;
        });

        // Handle Void vs Non-Void Result Assignment
        // var resultModifier = !member.ReturnsVoid ? "var result = " : string.Empty;
        string resultAssignment = member.ReturnsVoid ? "" : "var result = ";

        //        _inner.GetProtocolPrescriptionsAndMeasures(ref prescriptions_temp, ref measures_temp);
        // sb.AppendLine($"                {resultModifier}_inner.{member.Name}({string.Join(", ", callArgs)});");
        sb.Append($"                {resultAssignment}_inner.{member.Name}(");
        sb.Append(string.Join(", ", callArgs));
        sb.AppendLine(");");

        // 4. Return tuple from Lambda (The RAW Varian Types)
        var lambdaReturnParts = new List<string>();
        if (!member.ReturnsVoid) lambdaReturnParts.Add("result");

        foreach (var p in member.Parameters.Where(x => x.IsOut || x.IsRef)) {
            lambdaReturnParts.Add($"{p.Name}_temp");
        }
        sb.AppendLine($"                return ({string.Join(", ", lambdaReturnParts)});");
        sb.AppendLine($"            }});");

        // 5. Wrap Results
        // We must unpack 'postResult' and wrap items into Interfaces
        var finalReturnParts = new List<string>();
        int tupleIndex = 1;

        // A. Handle Return Value
        if (!member.ReturnsVoid) {
            string itemAccess = $"postResult.Item{tupleIndex++}";

            if (member.IsReturnWrappable) {
                // Check if it is a collection (primitive check based on string, ideally should be in Context)
                if (member.WrapperReturnTypeName.StartsWith("IReadOnlyList")) {
                    // Extract inner type name explicitly if possible, or hack it
                    // Ideally, OutParameterMethodContext should have WrapperReturnItemName
                    string innerWrapper = member.WrapperReturnTypeName.Replace("IReadOnlyList<", "").Replace(">", "");
                    finalReturnParts.Add($"{itemAccess}?.Select(x => new {innerWrapper}(x, _service)).ToList()");
                }
                else {
                    finalReturnParts.Add($"{itemAccess} == null ? null : new {member.WrapperReturnTypeName}({itemAccess}, _service)");
                }
            }
            else {
                finalReturnParts.Add(itemAccess);
            }
        }

        // B. Handle Out/Ref Parameters
        foreach (var p in member.Parameters.Where(x => x.IsOut || x.IsRef)) {
            string itemAccess = $"postResult.Item{tupleIndex++}";

            if (p.IsWrappable) {
                if (p.IsCollection) {
                    finalReturnParts.Add($"{itemAccess}?.Select(x => new {p.InnerWrapperType}(x, _service)).ToList()");
                }
                else {
                    finalReturnParts.Add($"{itemAccess} == null ? null : new {p.WrapperType}({itemAccess}, _service)");
                }
            }
            else {
                finalReturnParts.Add(itemAccess);
            }
        }

        sb.AppendLine($"            return ({string.Join(",\r\n                    ", finalReturnParts)});");
        sb.AppendLine("        }");

        return sb.ToString();
    }
}

