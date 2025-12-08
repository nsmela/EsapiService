using System.Threading.Tasks;
using VMS.TPS.Common.Model.API;
using VMS.TPS.Common.Model.Types;
using Esapi.Interfaces;

namespace Esapi.Wrappers
{
    public class AsyncPlanSumComponent : IPlanSumComponent
    {
        internal readonly VMS.TPS.Common.Model.API.PlanSumComponent _inner;

        // Store the inner ESAPI object reference
        // internal so other wrappers can access it
        // new to override any inherited _inner fields
        internal new readonly IEsapiService _service;

        public AsyncPlanSumComponent(VMS.TPS.Common.Model.API.PlanSumComponent inner, IEsapiService service) : base(inner, service)
        {
            _inner = inner;
            _service = service;

            PlanSetupId = inner.PlanSetupId;
            PlanSumOperation = inner.PlanSumOperation;
            PlanWeight = inner.PlanWeight;
        }


        public string PlanSetupId { get; }

        public PlanSumOperation PlanSumOperation { get; }

        public double PlanWeight { get; }

        public Task RunAsync(Action<VMS.TPS.Common.Model.API.PlanSumComponent> action) => _service.RunAsync(() => action(_inner));
        public Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.PlanSumComponent, T> func) => _service.RunAsync(() => func(_inner));
    }
}
