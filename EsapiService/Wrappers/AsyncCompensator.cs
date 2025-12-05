namespace EsapiService.Wrappers
{
    public class AsyncCompensator : ICompensator
    {
        internal readonly VMS.TPS.Common.Model.API.Compensator _inner;

        // Store the inner ESAPI object reference
        // internal so other wrappers can access it
        // new to override any inherited _inner fields
        internal new readonly IEsapiService _service;

        public AsyncCompensator(VMS.TPS.Common.Model.API.Compensator inner, IEsapiService service) : base(inner, service)
        {
            _inner = inner;
            _service = service;

        }

        public void WriteXml(System.Xml.XmlWriter writer) => _inner.WriteXml(writer);
        public IAddOnMaterial Material => _inner.Material is null ? null : new AsyncAddOnMaterial(_inner.Material, _service);

        public ISlot Slot => _inner.Slot is null ? null : new AsyncSlot(_inner.Slot, _service);

        public ITray Tray => _inner.Tray is null ? null : new AsyncTray(_inner.Tray, _service);

    }
}
