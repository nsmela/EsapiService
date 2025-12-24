using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VMS.TPS.Common.Model.API;
using VMS.TPS.Common.Model.Types;
using Esapi.Interfaces;
using Esapi.Services;

namespace Esapi.Wrappers
{
    public class AsyncSerializableObject : ISerializableObject, IEsapiWrapper<VMS.TPS.Common.Model.API.SerializableObject>
    {
        internal readonly VMS.TPS.Common.Model.API.SerializableObject _inner;

        // Store the inner ESAPI object reference
        // internal so other wrappers can access it
        // new to override any inherited _inner fields
        internal readonly IEsapiService _service;

        public AsyncSerializableObject(VMS.TPS.Common.Model.API.SerializableObject inner, IEsapiService service)
        {
            if (inner is null) throw new ArgumentNullException(nameof(inner));
            if (service is null) throw new ArgumentNullException(nameof(service));

            _inner = inner;
            _service = service;

        }


        // Simple Method
        public Task<System.Xml.Schema.XmlSchema> GetSchemaAsync() => 
            _service.PostAsync(context => _inner.GetSchema());

        // Simple Void Method
        public Task ReadXmlAsync(System.Xml.XmlReader reader) 
        {
            _service.PostAsync(context => _inner.ReadXml(reader));
            Refresh();
            return Task.CompletedTask;
        }

        // Simple Void Method
        public Task WriteXmlAsync(System.Xml.XmlWriter writer) 
        {
            _service.PostAsync(context => _inner.WriteXml(writer));
            Refresh();
            return Task.CompletedTask;
        }

        public Task RunAsync(Action<VMS.TPS.Common.Model.API.SerializableObject> action) => _service.PostAsync((context) => action(_inner));
        public Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.SerializableObject, T> func) => _service.PostAsync<T>((context) => func(_inner));

        // updates simple properties that might have changed
        public void Refresh()
        {
        }

        public static implicit operator VMS.TPS.Common.Model.API.SerializableObject(AsyncSerializableObject wrapper) => wrapper._inner;

        // Internal Explicit Implementation to expose _inner safely for covariance
        VMS.TPS.Common.Model.API.SerializableObject IEsapiWrapper<VMS.TPS.Common.Model.API.SerializableObject>.Inner => _inner;

        // Explicit or Implicit implementation of Service
        // Since _service is private, we expose it via the interface
        IEsapiService IEsapiWrapper<VMS.TPS.Common.Model.API.SerializableObject>.Service => _service;

        /* --- Skipped Members (Not generated) ---
           - ClearSerializationHistory: Static members are not supported
        */
    }
}
