namespace EsapiService.Wrappers
{
    public class AsyncRadioactiveSource : IRadioactiveSource
    {
        internal readonly VMS.TPS.Common.Model.API.RadioactiveSource _inner;

        // Store the inner ESAPI object reference
        // internal so other wrappers can access it
        // new to override any inherited _inner fields
        internal new readonly IEsapiService _service;

        public AsyncRadioactiveSource(VMS.TPS.Common.Model.API.RadioactiveSource inner, IEsapiService service) : base(inner, service)
        {
            _inner = inner;
            _service = service;

            NominalActivity = inner.NominalActivity;
            SerialNumber = inner.SerialNumber;
            Strength = inner.Strength;
        }

        public void WriteXml(System.Xml.XmlWriter writer) => _inner.WriteXml(writer);
        public System.Collections.Generic.IReadOnlyList<System.DateTime> CalibrationDate => _inner.CalibrationDate?.ToList();
        public bool NominalActivity { get; }
        public IRadioactiveSourceModel RadioactiveSourceModel => _inner.RadioactiveSourceModel is null ? null : new AsyncRadioactiveSourceModel(_inner.RadioactiveSourceModel, _service);

        public string SerialNumber { get; }
        public double Strength { get; }
    }
}
