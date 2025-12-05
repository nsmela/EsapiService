namespace EsapiService.Wrappers
{
    public class AsyncWedge : IWedge
    {
        internal readonly VMS.TPS.Common.Model.API.Wedge _inner;

        // Store the inner ESAPI object reference
        // internal so other wrappers can access it
        // new to override any inherited _inner fields
        internal new readonly IEsapiService _service;

        public AsyncWedge(VMS.TPS.Common.Model.API.Wedge inner, IEsapiService service) : base(inner, service)
        {
            _inner = inner;
            _service = service;

            Direction = inner.Direction;
            WedgeAngle = inner.WedgeAngle;
        }

        public void WriteXml(System.Xml.XmlWriter writer) => _inner.WriteXml(writer);
        public double Direction { get; }
        public double WedgeAngle { get; }
    }
}
