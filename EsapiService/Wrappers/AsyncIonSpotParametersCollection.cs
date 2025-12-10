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
    public class AsyncIonSpotParametersCollection : AsyncSerializableObject, IIonSpotParametersCollection
    {
        internal new readonly VMS.TPS.Common.Model.API.IonSpotParametersCollection _inner;

        // Store the inner ESAPI object reference
        // internal so other wrappers can access it
        // new to override any inherited _inner fields
        internal new readonly IEsapiService _service;

        public AsyncIonSpotParametersCollection(VMS.TPS.Common.Model.API.IonSpotParametersCollection inner, IEsapiService service) : base(inner, service)
        {
            _inner = inner;
            _service = service;

            Count = inner.Count;
        }




        public int Count { get; }

        public Task RunAsync(Action<VMS.TPS.Common.Model.API.IonSpotParametersCollection> action) => _service.PostAsync((context) => action(_inner));
        public Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.IonSpotParametersCollection, T> func) => _service.PostAsync<T>((context) => func(_inner));
    }
}
