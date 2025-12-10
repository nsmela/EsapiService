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
    public class AsyncTradeoffPlanGenerationIntermediateDoseMode : ITradeoffPlanGenerationIntermediateDoseMode
    {
        internal readonly VMS.TPS.Common.Model.API.TradeoffPlanGenerationIntermediateDoseMode _inner;

        // Store the inner ESAPI object reference
        // internal so other wrappers can access it
        // new to override any inherited _inner fields
        internal readonly IEsapiService _service;

        public AsyncTradeoffPlanGenerationIntermediateDoseMode(VMS.TPS.Common.Model.API.TradeoffPlanGenerationIntermediateDoseMode inner, IEsapiService service)
        {
            _inner = inner;
            _service = service;

            value__ = inner.value__;
        }


        public int value__ { get; private set; }
        public async Task Setvalue__Async(int value)
        {
            value__ = await _service.PostAsync(context => 
            {
                _inner.value__ = value;
                return _inner.value__;
            });
        }

        public Task RunAsync(Action<VMS.TPS.Common.Model.API.TradeoffPlanGenerationIntermediateDoseMode> action) => _service.PostAsync((context) => action(_inner));
        public Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.TradeoffPlanGenerationIntermediateDoseMode, T> func) => _service.PostAsync<T>((context) => func(_inner));

        public static implicit operator VMS.TPS.Common.Model.API.TradeoffPlanGenerationIntermediateDoseMode(AsyncTradeoffPlanGenerationIntermediateDoseMode wrapper) => wrapper._inner;
    }
}
