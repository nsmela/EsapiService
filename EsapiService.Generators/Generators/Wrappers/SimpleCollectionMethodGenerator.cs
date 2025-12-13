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
        public async Task<{member.InterfaceName}> {NamingConvention.GetMethodName(member.Name)}{member.Signature} => 
            await _service.PostAsync(context => _inner.{member.Name}({WrapperTools.BuildCallArguments(member.Parameters)})?.ToList());";

}

