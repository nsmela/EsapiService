using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EsapiService.Generators.Contexts
{
    public record TargetContext
    {
        public string Name { get; init; } = string.Empty;
        public string InterfaceName { get; init; } = string.Empty;
        public string WrapperName { get; init; } = string.Empty;
        public bool IsAbstract { get; init; } = false;

        // --- Inheritance --- //
        public string BaseName {  get; init; } = string.Empty;
        public string BaseInterface { get; init; } = string.Empty;
        public string BaseWrapperName { get; init; } = string.Empty;
    }
}
