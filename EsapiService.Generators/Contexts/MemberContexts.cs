
using System.Collections.Immutable;

namespace EsapiService.Generators.Contexts;

/* Anatomy of a Member
 * 
 * public new override virtual async Task<IReadOnlyList<{ReturnType}>> {MemberName}({InputParameters})
 * public string Name { get; set; } // simple property 
 * public MeshGeometry3D MeshGeometry { get; } // complex
 * public IEnumerable<Structure> Structures {get;} // complex collection
 * 
 * public new string Id {
 *       get => return base.Id;
 *       set {
 *           Globals.PrecheckApiMemberCall(this, (EsapiMemberCategory)3, "Id", 47);
 *           IApiModifyingMethodGuard eSAPIClinicalModifyingMethodGuard = GetESAPIClinicalModifyingMethodGuard("Id");
 *           try {
 *               TypeBasedIdValidator.ThrowIfNotValidId(value, this);
 *               ((IDataObject)Impl).Id = value;
 *           } finally {
 *               ((IDisposable)eSAPIClinicalModifyingMethodGuard)?.Dispose();
 *           }
 *       }
 *   }
 *   
 *    public IEnumerable<ApplicationScriptLog> ApplicationScriptLogs {
 *        get {
 *            Globals.PrecheckApiMemberCall(this, (EsapiMemberCategory)2, "ApplicationScriptLogs", 54);
 *            foreach (IApplicationScriptLog applicationScriptLog in Impl.ApplicationScriptLogs) {
 *                yield return ApiObjectFactory.CreateApiDataObject<ApplicationScriptLog, IApplicationScriptLog>(applicationScriptLog);
 *            }
 *        }
 *    }
 * 
 *    public Image Image {
 *        get {
 *            Globals.PrecheckApiMemberCall(this, (EsapiMemberCategory)2, "Image", 66);
 *            IImage image = Impl.Image;
 *            if (image != null) {
 *                return ApiObjectFactory.CreateApiDataObject<Image, IImage>(image);
 *            }
 * 
 *            return null;
 *        }
 *    }
 *   

 */

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
    string WrapperName,
    string InterfaceName,
    bool IsReadOnly
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
    string CallParameters   // "options"
) : IMemberContext;

// 2. Simple Return Methods (e.g. string GetId())
public record SimpleMethodContext(
    string Name,
    string Symbol,
    string XmlDocumentation,
    string ReturnType,      // "string" or "int"
    string Signature,
    string OriginalSignature,
    string CallParameters
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
    string CallParameters
) : IMemberContext;

// 4. Simple Collection Methods (e.g. IEnumerable<string> GetHistory())
public record SimpleCollectionMethodContext(
    string Name,
    string Symbol,
    string XmlDocumentation,
    string InterfaceName,   // "IReadOnlyList<string>"
    string Signature,
    string OriginalSignature,
    string CallParameters
) : IMemberContext;

// 5. Complex Collection Methods (e.g. IEnumerable<Structure> GetStructures())
public record ComplexCollectionMethodContext(
    string Name,
    string Symbol,
    string XmlDocumentation,
    string InterfaceName,   // "IReadOnlyList<IStructure>"
    string WrapperName, // "IReadOnlyList<AsyncStructure>"
    string Signature,
    string OriginalSignature,
    string CallParameters
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