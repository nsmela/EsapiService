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
    public class AsyncOptimizationLineObjective : AsyncOptimizationObjective, IOptimizationLineObjective, IEsapiWrapper<VMS.TPS.Common.Model.API.OptimizationLineObjective>
    {
        internal new readonly VMS.TPS.Common.Model.API.OptimizationLineObjective _inner;

        // Store the inner ESAPI object reference
        // internal so other wrappers can access it
        // new to override any inherited _inner fields
        internal new readonly IEsapiService _service;

        public AsyncOptimizationLineObjective(VMS.TPS.Common.Model.API.OptimizationLineObjective inner, IEsapiService service) : base(inner, service)
        {
            if (inner is null) throw new ArgumentNullException(nameof(inner));
            if (service is null) throw new ArgumentNullException(nameof(service));

            _inner = inner;
            _service = service;
        }


        public DVHPoint[] CurveData =>
            _inner.CurveData;


        public Task RunAsync(Action<VMS.TPS.Common.Model.API.OptimizationLineObjective> action) => _service.PostAsync((context) => action(_inner));
        public Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.OptimizationLineObjective, T> func) => _service.PostAsync<T>((context) => func(_inner));

        public static implicit operator VMS.TPS.Common.Model.API.OptimizationLineObjective(AsyncOptimizationLineObjective wrapper) => wrapper._inner;

        // Internal Explicit Implementation to expose _inner safely for covariance
        VMS.TPS.Common.Model.API.OptimizationLineObjective IEsapiWrapper<VMS.TPS.Common.Model.API.OptimizationLineObjective>.Inner => _inner;

        // Explicit or Implicit implementation of Service
        // Since _service is private, we expose it via the interface
        IEsapiService IEsapiWrapper<VMS.TPS.Common.Model.API.OptimizationLineObjective>.Service => _service;
    }
}
