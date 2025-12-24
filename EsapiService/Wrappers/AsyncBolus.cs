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
    public class AsyncBolus : AsyncSerializableObject, IBolus, IEsapiWrapper<VMS.TPS.Common.Model.API.Bolus>
    {
        internal new readonly VMS.TPS.Common.Model.API.Bolus _inner;

        // Store the inner ESAPI object reference
        // internal so other wrappers can access it
        // new to override any inherited _inner fields
        internal new readonly IEsapiService _service;

        public AsyncBolus(VMS.TPS.Common.Model.API.Bolus inner, IEsapiService service) : base(inner, service)
        {
            if (inner is null) throw new ArgumentNullException(nameof(inner));
            if (service is null) throw new ArgumentNullException(nameof(service));

            _inner = inner;
            _service = service;

            Id = inner.Id;
            MaterialCTValue = inner.MaterialCTValue;
            Name = inner.Name;
        }


        public string Id { get; private set; }


        public double MaterialCTValue { get; private set; }


        public string Name { get; private set; }


        public Task RunAsync(Action<VMS.TPS.Common.Model.API.Bolus> action) => _service.PostAsync((context) => action(_inner));
        public Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.Bolus, T> func) => _service.PostAsync<T>((context) => func(_inner));

        // updates simple properties that might have changed
        public new void Refresh()
        {
            base.Refresh();

            Id = _inner.Id;
            MaterialCTValue = _inner.MaterialCTValue;
            Name = _inner.Name;
        }

        public static implicit operator VMS.TPS.Common.Model.API.Bolus(AsyncBolus wrapper) => wrapper._inner;

        // Internal Explicit Implementation to expose _inner safely for covariance
        VMS.TPS.Common.Model.API.Bolus IEsapiWrapper<VMS.TPS.Common.Model.API.Bolus>.Inner => _inner;

        // Explicit or Implicit implementation of Service
        // Since _service is private, we expose it via the interface
        IEsapiService IEsapiWrapper<VMS.TPS.Common.Model.API.Bolus>.Service => _service;
    }
}
