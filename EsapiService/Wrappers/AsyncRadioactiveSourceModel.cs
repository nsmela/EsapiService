namespace EsapiService.Wrappers
{
    public class AsyncRadioactiveSourceModel : IRadioactiveSourceModel
    {
        internal readonly VMS.TPS.Common.Model.API.RadioactiveSourceModel _inner;

        // Store the inner ESAPI object reference
        // internal so other wrappers can access it
        // new to override any inherited _inner fields
        internal new readonly IEsapiService _service;

        public AsyncRadioactiveSourceModel(VMS.TPS.Common.Model.API.RadioactiveSourceModel inner, IEsapiService service) : base(inner, service)
        {
            _inner = inner;
            _service = service;

            ActiveSize = inner.ActiveSize;
            ActivityConversionFactor = inner.ActivityConversionFactor;
            CalculationModel = inner.CalculationModel;
            DoseRateConstant = inner.DoseRateConstant;
            HalfLife = inner.HalfLife;
            LiteratureReference = inner.LiteratureReference;
            Manufacturer = inner.Manufacturer;
            SourceType = inner.SourceType;
            Status = inner.Status;
            StatusUserName = inner.StatusUserName;
        }

        public void WriteXml(System.Xml.XmlWriter writer) => _inner.WriteXml(writer);
        public VMS.TPS.Common.Model.Types.VVector ActiveSize { get; }
        public double ActivityConversionFactor { get; }
        public string CalculationModel { get; }
        public double DoseRateConstant { get; }
        public double HalfLife { get; }
        public string LiteratureReference { get; }
        public string Manufacturer { get; }
        public string SourceType { get; }
        public string Status { get; }
        public System.Collections.Generic.IReadOnlyList<System.DateTime> StatusDate => _inner.StatusDate?.ToList();
        public string StatusUserName { get; }
    }
}
