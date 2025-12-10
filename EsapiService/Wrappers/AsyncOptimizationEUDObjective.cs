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
    public class AsyncOptimizationEUDObjective : AsyncOptimizationObjective, IOptimizationEUDObjective
    {
        internal new readonly VMS.TPS.Common.Model.API.OptimizationEUDObjective _inner;

        // Store the inner ESAPI object reference
        // internal so other wrappers can access it
        // new to override any inherited _inner fields
        internal new readonly IEsapiService _service;

        public AsyncOptimizationEUDObjective(VMS.TPS.Common.Model.API.OptimizationEUDObjective inner, IEsapiService service) : base(inner, service)
        {
            _inner = inner;
            _service = service;

            IsRobustObjective = inner.IsRobustObjective;
            ParameterA = inner.ParameterA;
        }


        public bool IsRobustObjective { get; private set; }
        public async Task SetIsRobustObjectiveAsync(bool value)
        {
            IsRobustObjective = await _service.PostAsync(context => 
            {
                _inner.IsRobustObjective = value;
                return _inner.IsRobustObjective;
            });
        }

        public double ParameterA { get; }

        public Task RunAsync(Action<VMS.TPS.Common.Model.API.OptimizationEUDObjective> action) => _service.PostAsync((context) => action(_inner));
        public Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.OptimizationEUDObjective, T> func) => _service.PostAsync<T>((context) => func(_inner));

        public static implicit operator VMS.TPS.Common.Model.API.OptimizationEUDObjective(AsyncOptimizationEUDObjective wrapper) => wrapper._inner;
    }
}
