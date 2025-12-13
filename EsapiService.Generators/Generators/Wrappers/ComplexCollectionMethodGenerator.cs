using EsapiService.Generators.Contexts;
using System.Text;

namespace EsapiService.Generators.Generators.Wrappers;

public static class ComplexCollectionMethodGenerator {
    public static string Generate(ComplexCollectionMethodContext member) {
        var sb = new StringBuilder();
        sb.AppendLine($"        public async Task<{member.InterfaceName}> {NamingConvention.GetMethodName(member.Name)}{member.Signature}");
        sb.AppendLine($"        {{");
        sb.AppendLine($"            return await _service.PostAsync(context => ");
        // Convert to List of Wrappers
        sb.AppendLine($"                _inner.{member.Name}({WrapperTools.BuildCallArguments(member.Parameters)})?.Select(x => new {member.WrapperItemName}(x, _service)).ToList());");
        sb.AppendLine($"        }}");
        return sb.ToString();
    }
}

