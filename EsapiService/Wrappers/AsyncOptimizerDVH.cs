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
    public class AsyncOptimizerDVH : IOptimizerDVH
    {
        internal readonly VMS.TPS.Common.Model.API.OptimizerDVH _inner;

        // Store the inner ESAPI object reference
        // internal so other wrappers can access it
        // new to override any inherited _inner fields
        internal readonly IEsapiService _service;

        public AsyncOptimizerDVH(VMS.TPS.Common.Model.API.OptimizerDVH inner, IEsapiService service)
        {
            _inner = inner;
            _service = service;

        }


        public async Task<IStructure> GetStructureAsync()
        {
            return await _service.PostAsync(context => 
                _inner.Structure is null ? null : new AsyncStructure(_inner.Structure, _service));
        }

        public Task RunAsync(Action<VMS.TPS.Common.Model.API.OptimizerDVH> action) => _service.PostAsync((context) => action(_inner));
        public Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.OptimizerDVH, T> func) => _service.PostAsync<T>((context) => func(_inner));
    }
}
