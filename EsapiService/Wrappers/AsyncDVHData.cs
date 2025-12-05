namespace EsapiService.Wrappers
{
    public class AsyncDVHData : IDVHData
    {
        internal readonly VMS.TPS.Common.Model.API.DVHData _inner;

        // Store the inner ESAPI object reference
        // internal so other wrappers can access it
        // new to override any inherited _inner fields
        internal new readonly IEsapiService _service;

        public AsyncDVHData(VMS.TPS.Common.Model.API.DVHData inner, IEsapiService service) : base(inner, service)
        {
            _inner = inner;
            _service = service;

            Coverage = inner.Coverage;
            CurveData = inner.CurveData;
            MaxDose = inner.MaxDose;
            MaxDosePosition = inner.MaxDosePosition;
            MeanDose = inner.MeanDose;
            MedianDose = inner.MedianDose;
            MinDose = inner.MinDose;
            MinDosePosition = inner.MinDosePosition;
            SamplingCoverage = inner.SamplingCoverage;
            StdDev = inner.StdDev;
            Volume = inner.Volume;
        }

        public void WriteXml(System.Xml.XmlWriter writer) => _inner.WriteXml(writer);
        public double Coverage { get; }
        public VMS.TPS.Common.Model.Types.DVHPoint[] CurveData { get; }
        public VMS.TPS.Common.Model.Types.DoseValue MaxDose { get; }
        public VMS.TPS.Common.Model.Types.VVector MaxDosePosition { get; }
        public VMS.TPS.Common.Model.Types.DoseValue MeanDose { get; }
        public VMS.TPS.Common.Model.Types.DoseValue MedianDose { get; }
        public VMS.TPS.Common.Model.Types.DoseValue MinDose { get; }
        public VMS.TPS.Common.Model.Types.VVector MinDosePosition { get; }
        public double SamplingCoverage { get; }
        public double StdDev { get; }
        public double Volume { get; }
    }
}
