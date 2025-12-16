
using System.Collections.Immutable;

namespace EsapiService.Generators.Contexts;

public interface IMemberContext {
    string Name { get; }
    string Symbol { get; }
    string XmlDocumentation { get; }
}

// For properties like 'string Id', 'int Count'
public record SimplePropertyContext(string Name, string Symbol, string XmlDocumentation, bool IsReadOnly) : IMemberContext;

// For properties like 'IEnumerable<string> History'
public record SimpleCollectionPropertyContext(
    string Name,
    string Symbol,           // "IEnumerable<string>"
    string XmlDocumentation,
    string InnerType,        // "string"
    string WrapperName,      // "IReadOnlyList<string>"
    string InterfaceName     // "IReadOnlyList<string>"
) : IMemberContext;

// For properties like 'PlanSetup Plan', which need to be wrapped as 'IPlanSetup Plan'
public record ComplexPropertyContext(
    string Name,
    string Symbol,
    string XmlDocumentation,
    string ReturnValue,
    string WrapperName,
    string InterfaceName,
    bool IsReadOnly,
    bool IsWrapped = true,
    bool IsFreezable = false
) : IMemberContext;

// For properties like 'IEnumerable<Structure> Structures'
public record CollectionPropertyContext(
    string Name,
    string Symbol,           // The full type: "IEnumerable<Structure>"
    string XmlDocumentation,
    string InnerType,        // The unwrapped type: "Structure"
    string WrapperName,      // The full wrapped type: "IEnumerable<AsyncStructure>"
    string InterfaceName,    // The full interface name: "IEnumerable<IStructure>"
    string WrapperItemName,  // The wrapper: "AsyncStructure"
    string InterfaceItemName // The interface: "IStructure"
) : IMemberContext;

// EsapiService.Generators/Contexts/MemberContexts.cs

// 1. Void Methods (e.g. void Calculate())
public record VoidMethodContext(
    string Name,
    string Symbol,
    string XmlDocumentation,
    string Signature,       // "(int options)"
    string OriginalSignature,
    string CallParameters,   // "options"
    ImmutableList<ParameterContext> Parameters
) : IMemberContext;

// 2. Simple Return Methods (e.g. string GetId())
public record SimpleMethodContext(
    string Name,
    string Symbol,
    string XmlDocumentation,
    string ReturnType,      // "string" or "int"
    string Signature,
    string OriginalSignature,
    string CallParameters,
    ImmutableList<ParameterContext> Parameters
) : IMemberContext;

// 3. Complex Return Methods (e.g. PlanSetup GetPlan())
public record ComplexMethodContext(
    string Name,
    string Symbol,
    string XmlDocumentation,
    string WrapperName,     // "AsyncPlanSetup"
    string InterfaceName,   // "IPlanSetup"
    string Signature,
    string OriginalSignature,
    string CallParameters,
    ImmutableList<ParameterContext> Parameters
) : IMemberContext;

// 4. Simple Collection Methods (e.g. IEnumerable<string> GetHistory())
public record SimpleCollectionMethodContext(
    string Name,
    string Symbol,
    string XmlDocumentation,
    string InterfaceName,   // "IReadOnlyList<string>"
    string Signature,
    string OriginalSignature,
    string CallParameters,
    ImmutableList<ParameterContext> Parameters
) : IMemberContext;

// 5. Complex Collection Methods (e.g. IEnumerable<Structure> GetStructures())
public record ComplexCollectionMethodContext(
    string Name,
    string Symbol,
    string XmlDocumentation,
    string InterfaceName,   // "IReadOnlyList<IStructure>"
    string WrapperName, // "IReadOnlyList<AsyncStructure>"
    string WrapperItemName, // AsyncStructure
    string Signature,
    string OriginalSignature,
    string CallParameters,
    ImmutableList<ParameterContext> Parameters
) : IMemberContext;

// 6. A method that returns a tuple (out, ref input arguements)
public record OutParameterMethodContext(
    string Name,
    string Symbol,
    string XmlDocumentation,
    string OriginalReturnType,         // e.g. "ChangeBrachyTreatmentUnitResult" or "void"
    bool ReturnsVoid,
    ImmutableList<ParameterContext> Parameters,
    string ReturnTupleSignature,        // Pre-calculated string: "(ChangeBrachy... Result, List<string> messages)"
    string WrapperReturnTypeName = "",  // e.g. "AsyncStructure"
    bool IsReturnWrappable = false      // true if we need to wrap 'result'
) : IMemberContext;

// 7. Indexer Context (e.g. public ControlPoint this[int index])
public record IndexerContext(
    string Name,             // "this[]"
    string Symbol,           // Return Type e.g. "ControlPoint"
    string XmlDocumentation,
    string WrapperName,      // "AsyncControlPoint"
    string InterfaceName,    // "IControlPoint"
    ImmutableList<ParameterContext> Parameters, // The index parameters
    bool IsReadOnly,
    string EnumerableSource = ""
) : IMemberContext;

// 8. Skipped Member (was not found or was filtered out)
public record SkippedMemberContext(string Name, string Reason) : IMemberContext {
    public string Symbol => "";
    public string XmlDocumentation => "";
}