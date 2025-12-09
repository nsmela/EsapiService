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
    public class AsyncTradeoffObjective : ITradeoffObjective
    {
        internal readonly VMS.TPS.Common.Model.API.TradeoffObjective _inner;

        // Store the inner ESAPI object reference
        // internal so other wrappers can access it
        // new to override any inherited _inner fields
        internal readonly IEsapiService _service;

        public AsyncTradeoffObjective(VMS.TPS.Common.Model.API.TradeoffObjective inner, IEsapiService service)
        {
            _inner = inner;
            _service = service;

            Id = inner.Id;
        }


        public int Id { get; }

        public async Task<IReadOnlyList<IOptimizationObjective>> GetOptimizationObjectivesAsync()
        {
            return await _service.PostAsync(context => 
                _inner.OptimizationObjectives?.Select(x => new AsyncOptimizationObjective(x, _service)).ToList());
        }


        public async Task<IStructure> GetStructureAsync()
        {
            return await _service.PostAsync(context => 
                _inner.Structure is null ? null : new AsyncStructure(_inner.Structure, _service));
        }

        public Task RunAsync(Action<VMS.TPS.Common.Model.API.TradeoffObjective> action) => _service.PostAsync((context) => action(_inner));
        public Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.TradeoffObjective, T> func) => _service.PostAsync<T>((context) => func(_inner));
    }
}
