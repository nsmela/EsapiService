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
    public class AsyncIonSpotParametersCollection : AsyncSerializableObject, IIonSpotParametersCollection, IEsapiWrapper<VMS.TPS.Common.Model.API.IonSpotParametersCollection>
    {
        internal new readonly VMS.TPS.Common.Model.API.IonSpotParametersCollection _inner;

        // Store the inner ESAPI object reference
        // internal so other wrappers can access it
        // new to override any inherited _inner fields
        internal new readonly IEsapiService _service;

        public AsyncIonSpotParametersCollection(VMS.TPS.Common.Model.API.IonSpotParametersCollection inner, IEsapiService service) : base(inner, service)
        {
            if (inner is null) throw new ArgumentNullException(nameof(inner));
            if (service is null) throw new ArgumentNullException(nameof(service));

            _inner = inner;
            _service = service;
        }


        public async Task<IIonSpotParameters> GetItemAsync(int index) // indexer context
        {
            return await _service.PostAsync(context => 
                _inner[index] is null ? null : new AsyncIonSpotParameters(_inner[index], _service));
        }

        public async Task<IReadOnlyList<IIonSpotParameters>> GetAllItemsAsync()
        {
            return await _service.PostAsync(context => 
                _inner.Select(x => new AsyncIonSpotParameters(x, _service)).ToList());
        }

        public int Count =>
            _inner.Count;


        public Task RunAsync(Action<VMS.TPS.Common.Model.API.IonSpotParametersCollection> action) => _service.PostAsync((context) => action(_inner));
        public Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.IonSpotParametersCollection, T> func) => _service.PostAsync<T>((context) => func(_inner));

        // --- Validates --- //
        /// <summary>
        /// Verifies is the wrapped ESAPI object isn't null.
        /// </summary>
        public new bool IsValid() => !IsNotValid();

        /// <summary>
        /// Verifies is the wrapped ESAPI object is null.
        /// </summary>
        public new bool IsNotValid() => _inner is null;

        public static implicit operator VMS.TPS.Common.Model.API.IonSpotParametersCollection(AsyncIonSpotParametersCollection wrapper) => wrapper._inner;

        // Internal Explicit Implementation to expose _inner safely for covariance
        VMS.TPS.Common.Model.API.IonSpotParametersCollection IEsapiWrapper<VMS.TPS.Common.Model.API.IonSpotParametersCollection>.Inner => _inner;

        // Explicit or Implicit implementation of Service
        // Since _service is private, we expose it via the interface
        IEsapiService IEsapiWrapper<VMS.TPS.Common.Model.API.IonSpotParametersCollection>.Service => _service;

        /* --- Skipped Members (Not generated) ---
           - GetEnumerator: Explicitly ignored by name
        */
    }
}
