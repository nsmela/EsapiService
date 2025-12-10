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
    public class AsyncOptimizationPointObjective : AsyncOptimizationObjective, IOptimizationPointObjective
    {
        internal new readonly VMS.TPS.Common.Model.API.OptimizationPointObjective _inner;

        // Store the inner ESAPI object reference
        // internal so other wrappers can access it
        // new to override any inherited _inner fields
        internal new readonly IEsapiService _service;

        public AsyncOptimizationPointObjective(VMS.TPS.Common.Model.API.OptimizationPointObjective inner, IEsapiService service) : base(inner, service)
        {
            _inner = inner;
            _service = service;

            IsRobustObjective = inner.IsRobustObjective;
            Volume = inner.Volume;
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

        public double Volume { get; }

        public Task RunAsync(Action<VMS.TPS.Common.Model.API.OptimizationPointObjective> action) => _service.PostAsync((context) => action(_inner));
        public Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.OptimizationPointObjective, T> func) => _service.PostAsync<T>((context) => func(_inner));

        public static implicit operator VMS.TPS.Common.Model.API.OptimizationPointObjective(AsyncOptimizationPointObjective wrapper) => wrapper._inner;
    }
}
