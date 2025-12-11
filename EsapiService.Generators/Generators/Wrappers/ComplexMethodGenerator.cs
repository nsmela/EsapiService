using EsapiService.Generators.Contexts;
using System.Text;

namespace EsapiService.Generators.Generators.Wrappers;
public static class ComplexMethodGenerator {
    public static string Generate(ComplexMethodContext member) {
        var sb = new StringBuilder();
        sb.AppendLine($"        public async Task<{member.InterfaceName}> {NamingConvention.GetMethodName(member.Name)}{member.Signature}");
        sb.AppendLine($"        {{");
        sb.AppendLine($"            return await _service.PostAsync(context => ");
        sb.AppendLine($"                _inner.{member.Name}({WrapperTools.BuildCallArguments(member.Parameters)}) is var result && result is null ? null : new {member.WrapperName}(result, _service));");
        sb.AppendLine($"        }}");
        return sb.ToString();
    }
}

