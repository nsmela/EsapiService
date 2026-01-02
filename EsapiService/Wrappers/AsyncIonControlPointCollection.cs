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
    public class AsyncIonControlPointCollection : AsyncSerializableObject, IIonControlPointCollection, IEsapiWrapper<VMS.TPS.Common.Model.API.IonControlPointCollection>
    {
        internal new readonly VMS.TPS.Common.Model.API.IonControlPointCollection _inner;

        // Store the inner ESAPI object reference
        // internal so other wrappers can access it
        // new to override any inherited _inner fields
        internal new readonly IEsapiService _service;

        public AsyncIonControlPointCollection(VMS.TPS.Common.Model.API.IonControlPointCollection inner, IEsapiService service) : base(inner, service)
        {
            if (inner is null) throw new ArgumentNullException(nameof(inner));
            if (service is null) throw new ArgumentNullException(nameof(service));

            _inner = inner;
            _service = service;
        }


        public async Task<IIonControlPoint> GetItemAsync(int index) // indexer context
        {
            return await _service.PostAsync(context => 
                _inner[index] is null ? null : new AsyncIonControlPoint(_inner[index], _service));
        }

        public async Task<IReadOnlyList<IIonControlPoint>> GetAllItemsAsync()
        {
            return await _service.PostAsync(context => 
                _inner.Select(x => new AsyncIonControlPoint(x, _service)).ToList());
        }

        public int Count =>
            _inner.Count;


        public Task RunAsync(Action<VMS.TPS.Common.Model.API.IonControlPointCollection> action) => _service.PostAsync((context) => action(_inner));
        public Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.IonControlPointCollection, T> func) => _service.PostAsync<T>((context) => func(_inner));

        // --- Validates --- //
        /// <summary>
        /// Verifies is the wrapped ESAPI object isn't null.
        /// </summary>
        public new bool IsValid() => !IsNotValid();

        /// <summary>
        /// Verifies is the wrapped ESAPI object is null.
        /// </summary>
        public new bool IsNotValid() => _inner is null;

        public static implicit operator VMS.TPS.Common.Model.API.IonControlPointCollection(AsyncIonControlPointCollection wrapper) => wrapper._inner;

        // Internal Explicit Implementation to expose _inner safely for covariance
        VMS.TPS.Common.Model.API.IonControlPointCollection IEsapiWrapper<VMS.TPS.Common.Model.API.IonControlPointCollection>.Inner => _inner;

        // Explicit or Implicit implementation of Service
        // Since _service is private, we expose it via the interface
        IEsapiService IEsapiWrapper<VMS.TPS.Common.Model.API.IonControlPointCollection>.Service => _service;

        /* --- Skipped Members (Not generated) ---
           - GetEnumerator: Explicitly ignored by name
        */
    }
}
