using EsapiService.Generators.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EsapiService.Generators.Generators.Wrappers;
    public static class VoidMethodGenerator {
    public static string Generate(VoidMethodContext member) =>
@$"        // Simple Void Method
        public Task {NamingConvention.GetMethodName(member.Name)}{member.Signature} => _service.PostAsync(context => _inner.{member.Name}({WrapperTools.BuildCallArguments(member.Parameters)}));";
}

