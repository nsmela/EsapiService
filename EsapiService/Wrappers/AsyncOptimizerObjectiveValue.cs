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
    public class AsyncOptimizerObjectiveValue : IOptimizerObjectiveValue, IEsapiWrapper<VMS.TPS.Common.Model.API.OptimizerObjectiveValue>
    {
        internal readonly VMS.TPS.Common.Model.API.OptimizerObjectiveValue _inner;

        // Store the inner ESAPI object reference
        // internal so other wrappers can access it
        // new to override any inherited _inner fields
        internal readonly IEsapiService _service;

        public AsyncOptimizerObjectiveValue(VMS.TPS.Common.Model.API.OptimizerObjectiveValue inner, IEsapiService service)
        {
            if (inner is null) throw new ArgumentNullException(nameof(inner));
            if (service is null) throw new ArgumentNullException(nameof(service));

            _inner = inner;
            _service = service;

            Value = inner.Value;
        }


        public async Task<IStructure> GetStructureAsync()
        {
            return await _service.PostAsync(context => {
                var innerResult = _inner.Structure is null ? null : new AsyncStructure(_inner.Structure, _service);
                return innerResult;
            });
        }

        public double Value { get; private set; }


        public Task RunAsync(Action<VMS.TPS.Common.Model.API.OptimizerObjectiveValue> action) => _service.PostAsync((context) => action(_inner));
        public Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.OptimizerObjectiveValue, T> func) => _service.PostAsync<T>((context) => func(_inner));

        // updates simple properties that might have changed
        public void Refresh()
        {

            Value = _inner.Value;
        }

        public static implicit operator VMS.TPS.Common.Model.API.OptimizerObjectiveValue(AsyncOptimizerObjectiveValue wrapper) => wrapper._inner;

        // Internal Explicit Implementation to expose _inner safely for covariance
        VMS.TPS.Common.Model.API.OptimizerObjectiveValue IEsapiWrapper<VMS.TPS.Common.Model.API.OptimizerObjectiveValue>.Inner => _inner;

        // Explicit or Implicit implementation of Service
        // Since _service is private, we expose it via the interface
        IEsapiService IEsapiWrapper<VMS.TPS.Common.Model.API.OptimizerObjectiveValue>.Service => _service;
    }
}
