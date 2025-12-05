namespace EsapiService.Wrappers
{
    public class AsyncIsodose : IIsodose
    {
        internal readonly VMS.TPS.Common.Model.API.Isodose _inner;

        // Store the inner ESAPI object reference
        // internal so other wrappers can access it
        // new to override any inherited _inner fields
        internal new readonly IEsapiService _service;

        public AsyncIsodose(VMS.TPS.Common.Model.API.Isodose inner, IEsapiService service) : base(inner, service)
        {
            _inner = inner;
            _service = service;

            Color = inner.Color;
            Level = inner.Level;
            MeshGeometry = inner.MeshGeometry;
        }

        public void WriteXml(System.Xml.XmlWriter writer) => _inner.WriteXml(writer);
        public System.Windows.Media.Color Color { get; }
        public VMS.TPS.Common.Model.Types.DoseValue Level { get; }
        public System.Windows.Media.Media3D.MeshGeometry3D MeshGeometry { get; }
    }
}
