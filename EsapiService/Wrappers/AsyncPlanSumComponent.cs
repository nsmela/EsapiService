using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VMS.TPS.Common.Model.API;
using VMS.TPS.Common.Model.Types;
using Esapi.Interfaces;
using Esapi.Services;

namespace Esapi.Wrappers
{
    public class AsyncPlanSumComponent : AsyncApiDataObject, IPlanSumComponent, IEsapiWrapper<VMS.TPS.Common.Model.API.PlanSumComponent>
    {
        internal new readonly VMS.TPS.Common.Model.API.PlanSumComponent _inner;

        // Store the inner ESAPI object reference
        // internal so other wrappers can access it
        // new to override any inherited _inner fields
        internal new readonly IEsapiService _service;

public AsyncPlanSumComponent(VMS.TPS.Common.Model.API.PlanSumComponent inner, IEsapiService service) : base(inner, service)
        {
            if (inner == null) throw new ArgumentNullException(nameof(inner));
            if (service == null) throw new ArgumentNullException(nameof(service));

            _inner = inner;
            _service = service;

            PlanSetupId = inner.PlanSetupId;
            PlanSumOperation = inner.PlanSumOperation;
            PlanWeight = inner.PlanWeight;
        }

        public string PlanSetupId { get; }

        public PlanSumOperation PlanSumOperation { get; }

        public double PlanWeight { get; }

        public Task RunAsync(Action<VMS.TPS.Common.Model.API.PlanSumComponent> action) => _service.PostAsync((context) => action(_inner));
        public Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.PlanSumComponent, T> func) => _service.PostAsync<T>((context) => func(_inner));

        public static implicit operator VMS.TPS.Common.Model.API.PlanSumComponent(AsyncPlanSumComponent wrapper) => wrapper._inner;

        // Internal Explicit Implementation to expose _inner safely for covariance
        VMS.TPS.Common.Model.API.PlanSumComponent IEsapiWrapper<VMS.TPS.Common.Model.API.PlanSumComponent>.Inner => _inner;

        // Explicit or Implicit implementation of Service
        // Since _service is private, we expose it via the interface
        IEsapiService IEsapiWrapper<VMS.TPS.Common.Model.API.PlanSumComponent>.Service => _service;
    }
}
