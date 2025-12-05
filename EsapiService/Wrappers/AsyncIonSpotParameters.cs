    using System.Threading.Tasks;
namespace EsapiService.Wrappers
{
    public class AsyncIonSpotParameters : IIonSpotParameters
    {
        internal readonly VMS.TPS.Common.Model.API.IonSpotParameters _inner;

        // Store the inner ESAPI object reference
        // internal so other wrappers can access it
        // new to override any inherited _inner fields
        internal new readonly IEsapiService _service;

        public AsyncIonSpotParameters(VMS.TPS.Common.Model.API.IonSpotParameters inner, IEsapiService service) : base(inner, service)
        {
            _inner = inner;
            _service = service;

        }

        public void WriteXml(System.Xml.XmlWriter writer) => _inner.WriteXml(writer);
        public float Weight => _inner.Weight;
        public async Task SetWeightAsync(float value) => _service.RunAsync(() => _inner.Weight = value);
        public float X => _inner.X;
        public async Task SetXAsync(float value) => _service.RunAsync(() => _inner.X = value);
        public float Y => _inner.Y;
        public async Task SetYAsync(float value) => _service.RunAsync(() => _inner.Y = value);
    }
}
