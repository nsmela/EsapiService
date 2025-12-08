using System.Threading.Tasks;
using VMS.TPS.Common.Model.API;
using VMS.TPS.Common.Model.Types;

namespace EsapiService.Wrappers
{
    public class AsyncSerializableObject : ISerializableObject
    {
        internal readonly VMS.TPS.Common.Model.API.SerializableObject _inner;

        // Store the inner ESAPI object reference
        // internal so other wrappers can access it
        // new to override any inherited _inner fields
        internal readonly IEsapiService _service;

        public AsyncSerializableObject(VMS.TPS.Common.Model.API.SerializableObject inner, IEsapiService service)
        {
            _inner = inner;
            _service = service;

        }

        public Task<Xml.Schema.XmlSchema> GetSchemaAsync() => _service.RunAsync(() => _inner.GetSchema());
        public Task ReadXmlAsync(Xml.XmlReader reader) => _service.RunAsync(() => _inner.ReadXml(reader));
        public Task WriteXmlAsync(Xml.XmlWriter writer) => _service.RunAsync(() => _inner.WriteXml(writer));

        public Task RunAsync(Action<VMS.TPS.Common.Model.API.SerializableObject> action) => _service.RunAsync(() => action(_inner));
        public Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.SerializableObject, T> func) => _service.RunAsync(() => func(_inner));
    }
}
    }
}
