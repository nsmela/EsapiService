using EsapiService.Generators.Contexts;
using System.Text;

namespace EsapiService.Generators.Generators.Wrappers;

public static class IndexPropertyGenerator {
    public static string Generate(IndexerContext member) {
        var parameters = member.Parameters;

        var fullIndexer = string.Join(", ", parameters.Select(p => $"{p.InterfaceType} {p.Name}"));
        var indexer = string.Join(", ", parameters.Select(p => p.Name));

        var sb = new StringBuilder();
        sb.AppendLine($"        public async Task<{member.InterfaceName}> GetItemAsync({fullIndexer}) // indexer context");
        sb.AppendLine($"        {{");
        sb.AppendLine($"            return await _service.PostAsync(context => ");
        sb.AppendLine($"                _inner[{indexer}] is null ? null : new {member.WrapperName}(_inner[{indexer}], _service));");
        sb.AppendLine($"        }}");

        // Set Item (if not readonly)
        if (!member.IsReadOnly) {
            sb.AppendLine();
            sb.AppendLine($"        public async Task SetItemAsync({fullIndexer}, {member.InterfaceName} value)");
            sb.AppendLine("        {");
            sb.AppendLine("             // Logic similar to ComplexProperty setter");
            sb.AppendLine($"             if (value is IEsapiWrapper<{member.WrapperName.Replace("Async", "")}> wrapper)");
            sb.AppendLine($"                 await _service.PostAsync(context => _inner[{indexer}] = wrapper.Inner);");
            sb.AppendLine("        }");
        }

        // GetAllItems (Helper method)
        sb.AppendLine();
        sb.AppendLine($"        public async Task<IReadOnlyList<{member.InterfaceName}>> GetAllItemsAsync()");
        sb.AppendLine($"        {{");
        sb.AppendLine($"            return await _service.PostAsync(context => ");
        sb.AppendLine($"                _inner{member.EnumerableSource}.Select(x => new {member.WrapperName}(x, _service)).ToList());");
        sb.AppendLine($"        }}");

        return sb.ToString().TrimEnd();
    }
}

