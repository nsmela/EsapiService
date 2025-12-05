namespace EsapiService.Wrappers
{
    using System.Linq;
    using System.Collections.Generic;
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
        public System.Collections.Generic.IReadOnlyList<IOptimizationObjective> OptimizationObjectives => _inner.OptimizationObjectives?.Select(x => new AsyncOptimizationObjective(x, _service)).ToList();
        public IStructure Structure => _inner.Structure is null ? null : new AsyncStructure(_inner.Structure, _service);

    }
}
