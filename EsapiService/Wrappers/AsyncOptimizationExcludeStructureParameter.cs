namespace EsapiService.Wrappers
{
    public class AsyncOptimizationExcludeStructureParameter : IOptimizationExcludeStructureParameter
    {
        internal readonly VMS.TPS.Common.Model.API.OptimizationExcludeStructureParameter _inner;

        // Store the inner ESAPI object reference
        // internal so other wrappers can access it
        // new to override any inherited _inner fields
        internal new readonly IEsapiService _service;

        public AsyncOptimizationExcludeStructureParameter(VMS.TPS.Common.Model.API.OptimizationExcludeStructureParameter inner, IEsapiService service) : base(inner, service)
        {
            _inner = inner;
            _service = service;

        }

        public void WriteXml(System.Xml.XmlWriter writer) => _inner.WriteXml(writer);
        public IStructure Structure => _inner.Structure is null ? null : new AsyncStructure(_inner.Structure, _service);

    }
}
