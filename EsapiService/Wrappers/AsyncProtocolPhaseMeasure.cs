namespace EsapiService.Wrappers
{
    public class AsyncProtocolPhaseMeasure : IProtocolPhaseMeasure
    {
        internal readonly VMS.TPS.Common.Model.API.ProtocolPhaseMeasure _inner;

        // Store the inner ESAPI object reference
        // internal so other wrappers can access it
        // new to override any inherited _inner fields
        internal new readonly IEsapiService _service;

        public AsyncProtocolPhaseMeasure(VMS.TPS.Common.Model.API.ProtocolPhaseMeasure inner, IEsapiService service) : base(inner, service)
        {
            _inner = inner;
            _service = service;

            TargetValue = inner.TargetValue;
            ActualValue = inner.ActualValue;
            Modifier = inner.Modifier;
            StructureId = inner.StructureId;
            Type = inner.Type;
            TypeText = inner.TypeText;
        }

        public void WriteXml(System.Xml.XmlWriter writer) => _inner.WriteXml(writer);
        public double TargetValue { get; }
        public double ActualValue { get; }
        public System.Collections.Generic.IReadOnlyList<bool> TargetIsMet => _inner.TargetIsMet?.ToList();
        public VMS.TPS.Common.Model.Types.MeasureModifier Modifier { get; }
        public string StructureId { get; }
        public VMS.TPS.Common.Model.Types.MeasureType Type { get; }
        public string TypeText { get; }
    }
}
