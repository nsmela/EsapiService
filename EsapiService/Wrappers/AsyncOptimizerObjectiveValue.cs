namespace EsapiService.Wrappers
{
    public class AsyncOptimizerObjectiveValue : IOptimizerObjectiveValue
    {
        internal readonly VMS.TPS.Common.Model.API.OptimizerObjectiveValue _inner;

        // Store the inner ESAPI object reference
        // internal so other wrappers can access it
        // new to override any inherited _inner fields
        internal readonly IEsapiService _service;

        public AsyncOptimizerObjectiveValue(VMS.TPS.Common.Model.API.OptimizerObjectiveValue inner, IEsapiService service)
        {
            _inner = inner;
            _service = service;

            Value = inner.Value;
        }

        public IStructure Structure => _inner.Structure is null ? null : new AsyncStructure(_inner.Structure, _service);

        public double Value { get; }
    }
}
