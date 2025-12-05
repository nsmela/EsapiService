using System.Collections.Immutable;

/* using System;
 * using System.Collections.Generic;
 * using System.ComponentModel;
 * using System.Diagnostics;
 * using System.Linq;
 * using System.Xml;
 * using VMS.TPS.Common.Model.Types;
 * 
 * namespace VMS.TPS.Common.Model.API;
 * 
 * public class StructureSet : ApiDataObject {
 * 
 * }
 */

namespace EsapiService.Generators.Contexts
{
    public record ClassContext
    {
        public string Name { get; init; } = string.Empty;
        public string InterfaceName { get; init; } = string.Empty;
        public string WrapperName { get; init; } = string.Empty;
        public bool IsAbstract { get; init; } = false;
        public string XmlDocumentation { get; init; } = string.Empty;

        // --- Inheritance --- //
        public string BaseName {  get; init; } = string.Empty;
        public string BaseInterface { get; init; } = string.Empty;
        public string BaseWrapperName { get; init; } = string.Empty;

        // --- Members --- //
        public ImmutableList<IMemberContext> Members { get; init; } = [];
    }
}
