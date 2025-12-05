namespace EsapiService.Wrappers
{
    public class AsyncRangeShifterSettings : IRangeShifterSettings
    {
        internal readonly VMS.TPS.Common.Model.API.RangeShifterSettings _inner;

        // Store the inner ESAPI object reference
        // internal so other wrappers can access it
        // new to override any inherited _inner fields
        internal new readonly IEsapiService _service;

        public AsyncRangeShifterSettings(VMS.TPS.Common.Model.API.RangeShifterSettings inner, IEsapiService service) : base(inner, service)
        {
            _inner = inner;
            _service = service;

            IsocenterToRangeShifterDistance = inner.IsocenterToRangeShifterDistance;
            RangeShifterSetting = inner.RangeShifterSetting;
            RangeShifterWaterEquivalentThickness = inner.RangeShifterWaterEquivalentThickness;
        }

        public void WriteXml(System.Xml.XmlWriter writer) => _inner.WriteXml(writer);
        public double IsocenterToRangeShifterDistance { get; }
        public string RangeShifterSetting { get; }
        public double RangeShifterWaterEquivalentThickness { get; }
        public IRangeShifter ReferencedRangeShifter => _inner.ReferencedRangeShifter is null ? null : new AsyncRangeShifter(_inner.ReferencedRangeShifter, _service);

    }
}
