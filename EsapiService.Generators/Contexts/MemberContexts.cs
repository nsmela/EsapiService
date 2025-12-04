
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
}

// For properties like 'string Id', 'int Count'
public record SimplePropertyContext(string Name, string Symbol, bool IsReadOnly) : IMemberContext;

// For properties like 'IEnumerable<string> History'
public record SimpleCollectionPropertyContext(
    string Name,
    string Symbol,           // "IEnumerable<string>"
    string InnerType,        // "string"
    string WrapperName,      // "IReadOnlyList<string>"
    string InterfaceName     // "IReadOnlyList<string>"
) : IMemberContext;

// For properties like 'PlanSetup Plan', which need to be wrapped as 'IPlanSetup Plan'
public record ComplexPropertyContext(string Name, string Symbol, string WrapperName, string InterfaceName) : IMemberContext;

// For methods
public record MethodContext(
    string Name,
    string Symbol,
    string Arguments,
    string Signature,
    string CallParameters // e.g. "options, name"
) : IMemberContext;

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