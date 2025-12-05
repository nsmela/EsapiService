namespace EsapiService.Wrappers
{
    public class AsyncRangeModulatorSettings : IRangeModulatorSettings
    {
        internal readonly VMS.TPS.Common.Model.API.RangeModulatorSettings _inner;

        // Store the inner ESAPI object reference
        // internal so other wrappers can access it
        // new to override any inherited _inner fields
        internal new readonly IEsapiService _service;

        public AsyncRangeModulatorSettings(VMS.TPS.Common.Model.API.RangeModulatorSettings inner, IEsapiService service) : base(inner, service)
        {
            _inner = inner;
            _service = service;

            IsocenterToRangeModulatorDistance = inner.IsocenterToRangeModulatorDistance;
            RangeModulatorGatingStartValue = inner.RangeModulatorGatingStartValue;
            RangeModulatorGatingStarWaterEquivalentThickness = inner.RangeModulatorGatingStarWaterEquivalentThickness;
            RangeModulatorGatingStopValue = inner.RangeModulatorGatingStopValue;
            RangeModulatorGatingStopWaterEquivalentThickness = inner.RangeModulatorGatingStopWaterEquivalentThickness;
        }

        public void WriteXml(System.Xml.XmlWriter writer) => _inner.WriteXml(writer);
        public double IsocenterToRangeModulatorDistance { get; }
        public double RangeModulatorGatingStartValue { get; }
        public double RangeModulatorGatingStarWaterEquivalentThickness { get; }
        public double RangeModulatorGatingStopValue { get; }
        public double RangeModulatorGatingStopWaterEquivalentThickness { get; }
        public IRangeModulator ReferencedRangeModulator => _inner.ReferencedRangeModulator is null ? null : new AsyncRangeModulator(_inner.ReferencedRangeModulator, _service);


        public Task RunAsync(Action<VMS.TPS.Common.Model.API.RangeModulatorSettings> action) => _service.RunAsync(() => action(_inner));
        public Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.RangeModulatorSettings, T> func) => _service.RunAsync(() => func(_inner));
    }
}
    }
}
