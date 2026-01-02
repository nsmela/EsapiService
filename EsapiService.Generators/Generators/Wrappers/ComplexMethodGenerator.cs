using EsapiService.Generators.Contexts;
using System.Text;

namespace EsapiService.Generators.Generators.Wrappers;
public static class ComplexMethodGenerator {
    public static string Generate(ComplexMethodContext member) =>
$@"        public {(member.IsShadowing ? "new " : "")}async Task<{member.InterfaceName}> {NamingConvention.GetMethodName(member.Name)}{member.Signature}
        {{
            return await _service.PostAsync(context => 
                _inner.{member.Name}({WrapperTools.BuildCallArguments(member.Parameters)}) is var result && result is null ? null : new {member.WrapperName}(result, _service));
        }}";

}

