namespace EsapiService.Wrappers
{
    public class AsyncOptimizationObjective : IOptimizationObjective
    {
        internal readonly VMS.TPS.Common.Model.API.OptimizationObjective _inner;

        // Store the inner ESAPI object reference
        // internal so other wrappers can access it
        // new to override any inherited _inner fields
        internal new readonly IEsapiService _service;

        public AsyncOptimizationObjective(VMS.TPS.Common.Model.API.OptimizationObjective inner, IEsapiService service) : base(inner, service)
        {
            _inner = inner;
            _service = service;

            Operator = inner.Operator;
            Priority = inner.Priority;
            StructureId = inner.StructureId;
        }

        public void WriteXml(System.Xml.XmlWriter writer) => _inner.WriteXml(writer);
        public bool Equals(object obj) => _inner.Equals(obj);
        public int GetHashCode() => _inner.GetHashCode();
        public VMS.TPS.Common.Model.Types.OptimizationObjectiveOperator Operator { get; }
        public double Priority { get; }
        public IStructure Structure => _inner.Structure is null ? null : new AsyncStructure(_inner.Structure, _service);

        public string StructureId { get; }

        public Task RunAsync(Action<VMS.TPS.Common.Model.API.OptimizationObjective> action) => _service.RunAsync(() => action(_inner));
        public Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.OptimizationObjective, T> func) => _service.RunAsync(() => func(_inner));
    }
}
    }
}
