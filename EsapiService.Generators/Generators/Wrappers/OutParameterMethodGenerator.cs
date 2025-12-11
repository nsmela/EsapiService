using EsapiService.Generators.Contexts;
using System.Text;

namespace EsapiService.Generators.Generators.Wrappers;

/*
public async Task<(IReadOnlyList<IProtocolPhasePrescription> prescriptions, IReadOnlyList<IProtocolPhaseMeasure> measures)> GetProtocolPrescriptionsAndMeasuresAsync(IReadOnlyList<IProtocolPhasePrescription> prescriptions, IReadOnlyList<IProtocolPhaseMeasure> measures)
{
    var postResult = await _service.PostAsync(context => {
        List<ProtocolPhasePrescription> prescriptions_temp = prescriptions?.Select(x => ((AsyncProtocolPhasePrescription)x)._inner).ToList();
        List<ProtocolPhaseMeasure> measures_temp = measures?.Select(x => ((AsyncProtocolPhaseMeasure)x)._inner).ToList();
        _inner.GetProtocolPrescriptionsAndMeasures(ref prescriptions_temp, ref measures_temp);
        return (prescriptions_temp, measures_temp);
    });
    return (
        prescriptions: postResult.Item1?.Select(x => (IProtocolPhasePrescription)new AsyncProtocolPhasePrescription(x, _service)).Where(x => x != null).ToList(),
        measures: postResult.Item2?.Select(x => (IProtocolPhaseMeasure)new AsyncProtocolPhaseMeasure(x, _service)).Where(x => x != null).ToList());
} 

public async Task<({returnValues})> {MethodName}({MethodSignatures})
{
    var postResult = await _service.PostAsync(context => {
        {Temp_Properties} List<ProtocolPhasePrescription> prescriptions_temp = prescriptions?.Select(x => ((AsyncProtocolPhasePrescription)x)._inner).ToList();

        _inner.GetProtocolPrescriptionsAndMeasures(ref prescriptions_temp, ref measures_temp);
        return (prescriptions_temp, measures_temp);
    });
    return (
        prescriptions: postResult.Item1?.Select(x => (IProtocolPhasePrescription)new AsyncProtocolPhasePrescription(x, _service)).Where(x => x != null).ToList(),
        measures: postResult.Item2?.Select(x => (IProtocolPhaseMeasure)new AsyncProtocolPhaseMeasure(x, _service)).Where(x => x != null).ToList());
} 
 */

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

            if (p.IsRef
                    && p.IsWrappable
                    && p.IsCollection) {
                string valueSource = $"{p.Name}?.Select(x => (({p.InnerType})x)._inner).ToList()";
                sb.AppendLine($"                {p.Type} {p.Name}_temp = {valueSource};");
                continue;
            }

            if (p.IsRef) {
                string valueSource = p.IsWrappable ? $"{p.Name}" : p.Name;
                sb.AppendLine($"                {p.Type} {p.Name}_temp = {valueSource};");
                continue;
            }

            if (p.IsWrappable) {
                sb.AppendLine($"                var value = (({p.WrapperType}){p.Name})._inner;");
            }
        }

        // 3. Build the Call String
        var callArgs = member.Parameters.Select(p => {
            if (p.IsOut) return $"out {p.Name}_temp";
            if (p.IsRef) return $"ref {p.Name}_temp";
            return p.IsWrappable ? $"value" : p.Name;
        });

        // Handle Void vs Non-Void Result Assignment
        var resultModifier = !member.ReturnsVoid ? "var result = " : string.Empty;

        //        _inner.GetProtocolPrescriptionsAndMeasures(ref prescriptions_temp, ref measures_temp);
        sb.AppendLine($"                {resultModifier}_inner.{member.Name}({string.Join(", ", callArgs)});");

        var returnValues = member.Parameters.Select(p => {
            if (p.IsOut) return $"{p.Name}_temp";
            if (p.IsRef) return $"{p.Name}_temp";
            return string.Empty;
        })
            .Where(x => !string.IsNullOrEmpty(x))
            .ToList();

        if (!string.IsNullOrEmpty(resultModifier)) { returnValues.Insert(0, "result"); }

        //        return (prescriptions_temp, measures_temp);
        sb.AppendLine($"                return ({string.Join(", ", returnValues)});");
        sb.AppendLine("            });");

        // 4. Build Return Tuple
        var returnParts = new List<string>();

        // A. The Result
        if (!member.ReturnsVoid) {
            // B: Wrap Result if Wrappable
            if (member.IsReturnWrappable) {
                returnParts.Add($"result is null ? null : new {member.WrapperReturnTypeName}(result, _service)");
            }
            else {
                returnParts.Add("result");
            }
        }
        
        // B. The Out/Ref values
        foreach (var p in member.Parameters.Where(x => x.IsOut || x.IsRef)) {
            if (p.IsWrappable) {
                returnParts.Add($"{p.Name}_temp is null ? null : new {p.WrapperType}({p.Name}_temp, _service)");
            }
            else {
                returnParts.Add($"{p.Name}_temp");
            }
        }

        //sb.AppendLine($"            return ({string.Join(", ", returnParts)});");
        sb.AppendLine($"            return (postResult);");
        sb.Append("        }"); // End method

        return sb.ToString();
    }
}

