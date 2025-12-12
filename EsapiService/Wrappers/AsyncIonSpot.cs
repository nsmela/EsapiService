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
    public class AsyncIonSpot : AsyncSerializableObject, IIonSpot, IEsapiWrapper<VMS.TPS.Common.Model.API.IonSpot>
    {
        internal new readonly VMS.TPS.Common.Model.API.IonSpot _inner;

        // Store the inner ESAPI object reference
        // internal so other wrappers can access it
        // new to override any inherited _inner fields
        internal new readonly IEsapiService _service;

public AsyncIonSpot(VMS.TPS.Common.Model.API.IonSpot inner, IEsapiService service) : base(inner, service)
        {
            _inner = inner;
            _service = service;

            Weight = inner.Weight;
        }

        public float Weight { get; }

        public Task RunAsync(Action<VMS.TPS.Common.Model.API.IonSpot> action) => _service.PostAsync((context) => action(_inner));
        public Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.IonSpot, T> func) => _service.PostAsync<T>((context) => func(_inner));

        public static implicit operator VMS.TPS.Common.Model.API.IonSpot(AsyncIonSpot wrapper) => wrapper;

        // Internal Explicit Implementation to expose _inner safely for covariance
        VMS.TPS.Common.Model.API.IonSpot IEsapiWrapper<VMS.TPS.Common.Model.API.IonSpot>.Inner => _inner;
    }
}
