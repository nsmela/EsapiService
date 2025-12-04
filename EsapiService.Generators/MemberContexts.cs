
namespace EsapiService.Generators.Contexts;

/* Anatomy of a Member
 * 
 * public new override virtual async Task<IReadOnlyList<{ReturnType}>> {MemberName}({InputParameters})
 * public string Name { get; set; } // simple property 
 * public MeshGeometry3D MeshGeometry { get; } // complex
 * public IEnumerable<Structure> Structures {get;} // complex collection
 * 
 */

public interface IMemberContext {
    string Name { get; }
    string Symbol { get; } 
}

// For properties like 'string Id', 'int Count'
public record SimplePropertyContext(string Name, string Symbol) : IMemberContext;

// For properties like 'PlanSetup Plan', which need to be wrapped as 'IPlanSetup Plan'
public record ComplexPropertyContext(string Name, string Symbol, string WrapperName, string InterfaceName) : IMemberContext;

// For methods
public record MethodContext(string Name, string Symbol, string Arguments, string Signature) : IMemberContext;

// For properties like 'IEnumerable<Structure> Structures'
public record CollectionPropertyContext(
    string Name,
    string Symbol,           // The full type: "IEnumerable<Structure>"
    string InnerType,        // The unwrapped type: "Structure"
    string WrapperName,      // The full wrapped type: "IEnumerable<AsyncStructure>"
    string InterfaceName,    // The full interface name: "IEnumerable<IStructure>"
    string WrapperItemName,  // The wrapper: "AsyncStructure"
    string InterfaceItemName // The interface: "IStructure"
) : IMemberContext;