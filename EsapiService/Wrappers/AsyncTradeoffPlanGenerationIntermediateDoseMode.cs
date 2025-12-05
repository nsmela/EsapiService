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

    }
}
