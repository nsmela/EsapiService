using EsapiService.Generators.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EsapiService.Generators.Generators.Wrappers;

public static class SimpleCollectionMethodGenerator {
    public static string Generate(SimpleCollectionMethodContext member) =>
@$"        // Simple Collection Method
        public Task<{member.InterfaceName}> {NamingConvention.GetMethodName(member.Name)}{member.Signature} => 
            _service.PostAsync(context => _inner.{member.Name}({WrapperTools.BuildCallArguments(member.Parameters)})?.ToList());";

}

