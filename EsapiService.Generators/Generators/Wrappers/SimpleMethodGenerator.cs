using EsapiService.Generators.Contexts;

namespace EsapiService.Generators.Generators.Wrappers;

public static class SimpleMethodGenerator {
    public static string Generate(SimpleMethodContext member) =>
@$"        // Simple Method
        public Task<{member.ReturnType}> {NamingConvention.GetMethodName(member.Name)}{member.Signature} => 
            _service.PostAsync(context => _inner.{member.Name}({WrapperTools.BuildCallArguments(member.Parameters)}));";
}
