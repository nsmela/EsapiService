
namespace Esapi.Services {
    // "out T" allows covariance, so IEsapiWrapper<PlanSetup> 
    // can be treated as IEsapiWrapper<PlanningItem> if needed.
    internal interface IEsapiWrapper<out T> {
        T Inner { get; }
    }
}
