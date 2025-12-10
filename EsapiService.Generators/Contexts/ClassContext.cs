using System.Collections.Immutable;

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

        // --- Enums --- //
        public bool IsEnum { get; set; }
        public List<string> EnumMembers { get; set; } = new List<string>();

        // --- Structs --- //
        public bool IsStruct { get; init; } = false;
        public ImmutableList<ClassContext> NestedTypes { get; init; } = ImmutableList<ClassContext>.Empty;
        public bool HasImplicitStringConversion { get; init; }

    }
}
