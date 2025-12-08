using System.Threading.Tasks;
using VMS.TPS.Common.Model.API;
using VMS.TPS.Common.Model.Types;

namespace EsapiService.Wrappers
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

        }


        public Task RunAsync(Action<VMS.TPS.Common.Model.API.TradeoffPlanGenerationIntermediateDoseMode> action) => _service.RunAsync(() => action(_inner));
        public Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.TradeoffPlanGenerationIntermediateDoseMode, T> func) => _service.RunAsync(() => func(_inner));
    }
}
    }
}
