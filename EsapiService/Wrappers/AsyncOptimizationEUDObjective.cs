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
    public class AsyncOptimizationEUDObjective : AsyncOptimizationObjective, IOptimizationEUDObjective, IEsapiWrapper<VMS.TPS.Common.Model.API.OptimizationEUDObjective>
    {
        internal new readonly VMS.TPS.Common.Model.API.OptimizationEUDObjective _inner;

        // Store the inner ESAPI object reference
        // internal so other wrappers can access it
        // new to override any inherited _inner fields
        internal new readonly IEsapiService _service;

        public AsyncOptimizationEUDObjective(VMS.TPS.Common.Model.API.OptimizationEUDObjective inner, IEsapiService service) : base(inner, service)
        {
            if (inner is null) throw new ArgumentNullException(nameof(inner));
            if (service is null) throw new ArgumentNullException(nameof(service));

            _inner = inner;
            _service = service;
        }


        public DoseValue Dose =>
            _inner.Dose;


        public double ParameterA =>
            _inner.ParameterA;


        public Task RunAsync(Action<VMS.TPS.Common.Model.API.OptimizationEUDObjective> action) => _service.PostAsync((context) => action(_inner));
        public Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.OptimizationEUDObjective, T> func) => _service.PostAsync<T>((context) => func(_inner));

        public static implicit operator VMS.TPS.Common.Model.API.OptimizationEUDObjective(AsyncOptimizationEUDObjective wrapper) => wrapper._inner;

        // Internal Explicit Implementation to expose _inner safely for covariance
        VMS.TPS.Common.Model.API.OptimizationEUDObjective IEsapiWrapper<VMS.TPS.Common.Model.API.OptimizationEUDObjective>.Inner => _inner;

        // Explicit or Implicit implementation of Service
        // Since _service is private, we expose it via the interface
        IEsapiService IEsapiWrapper<VMS.TPS.Common.Model.API.OptimizationEUDObjective>.Service => _service;
    }
}
