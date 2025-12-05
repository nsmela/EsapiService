namespace EsapiService.Wrappers
{
    public class AsyncOptimizationIMRTBeamParameter : IOptimizationIMRTBeamParameter
    {
        internal readonly VMS.TPS.Common.Model.API.OptimizationIMRTBeamParameter _inner;

        // Store the inner ESAPI object reference
        // internal so other wrappers can access it
        // new to override any inherited _inner fields
        internal new readonly IEsapiService _service;

        public AsyncOptimizationIMRTBeamParameter(VMS.TPS.Common.Model.API.OptimizationIMRTBeamParameter inner, IEsapiService service) : base(inner, service)
        {
            _inner = inner;
            _service = service;

            BeamId = inner.BeamId;
            Excluded = inner.Excluded;
            FixedJaws = inner.FixedJaws;
            SmoothX = inner.SmoothX;
            SmoothY = inner.SmoothY;
        }

        public void WriteXml(System.Xml.XmlWriter writer) => _inner.WriteXml(writer);
        public IBeam Beam => _inner.Beam is null ? null : new AsyncBeam(_inner.Beam, _service);

        public string BeamId { get; }
        public bool Excluded { get; }
        public bool FixedJaws { get; }
        public double SmoothX { get; }
        public double SmoothY { get; }
    }
}
