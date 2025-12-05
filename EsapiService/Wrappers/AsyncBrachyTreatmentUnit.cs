namespace EsapiService.Wrappers
{
    public class AsyncBrachyTreatmentUnit : IBrachyTreatmentUnit
    {
        internal readonly VMS.TPS.Common.Model.API.BrachyTreatmentUnit _inner;

        // Store the inner ESAPI object reference
        // internal so other wrappers can access it
        // new to override any inherited _inner fields
        internal new readonly IEsapiService _service;

        public AsyncBrachyTreatmentUnit(VMS.TPS.Common.Model.API.BrachyTreatmentUnit inner, IEsapiService service) : base(inner, service)
        {
            _inner = inner;
            _service = service;

            DoseRateMode = inner.DoseRateMode;
            DwellTimeResolution = inner.DwellTimeResolution;
            MachineInterface = inner.MachineInterface;
            MachineModel = inner.MachineModel;
            MaxDwellTimePerChannel = inner.MaxDwellTimePerChannel;
            MaxDwellTimePerPos = inner.MaxDwellTimePerPos;
            MaxDwellTimePerTreatment = inner.MaxDwellTimePerTreatment;
            MaximumChannelLength = inner.MaximumChannelLength;
            MaximumDwellPositionsPerChannel = inner.MaximumDwellPositionsPerChannel;
            MaximumStepSize = inner.MaximumStepSize;
            MinAllowedSourcePos = inner.MinAllowedSourcePos;
            MinimumChannelLength = inner.MinimumChannelLength;
            MinimumStepSize = inner.MinimumStepSize;
            NumberOfChannels = inner.NumberOfChannels;
            SourceCenterOffsetFromTip = inner.SourceCenterOffsetFromTip;
            SourceMovementType = inner.SourceMovementType;
            StepSizeResolution = inner.StepSizeResolution;
        }

        public void WriteXml(System.Xml.XmlWriter writer) => _inner.WriteXml(writer);
        public IRadioactiveSource GetActiveRadioactiveSource() => _inner.GetActiveRadioactiveSource() is var result && result is null ? null : new AsyncRadioactiveSource(result, _service);
        public string DoseRateMode { get; }
        public double DwellTimeResolution { get; }
        public string MachineInterface { get; }
        public string MachineModel { get; }
        public double MaxDwellTimePerChannel { get; }
        public double MaxDwellTimePerPos { get; }
        public double MaxDwellTimePerTreatment { get; }
        public double MaximumChannelLength { get; }
        public int MaximumDwellPositionsPerChannel { get; }
        public double MaximumStepSize { get; }
        public double MinAllowedSourcePos { get; }
        public double MinimumChannelLength { get; }
        public double MinimumStepSize { get; }
        public int NumberOfChannels { get; }
        public double SourceCenterOffsetFromTip { get; }
        public string SourceMovementType { get; }
        public double StepSizeResolution { get; }
    }
}
