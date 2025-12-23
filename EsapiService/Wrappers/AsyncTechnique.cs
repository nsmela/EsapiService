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
    public class AsyncTechnique : AsyncApiDataObject, ITechnique, IEsapiWrapper<VMS.TPS.Common.Model.API.Technique>
    {
        internal new readonly VMS.TPS.Common.Model.API.Technique _inner;

        // Store the inner ESAPI object reference
        // internal so other wrappers can access it
        // new to override any inherited _inner fields
        internal new readonly IEsapiService _service;

        public AsyncTechnique(VMS.TPS.Common.Model.API.Technique inner, IEsapiService service) : base(inner, service)
        {
            if (inner is null) throw new ArgumentNullException(nameof(inner));
            if (service is null) throw new ArgumentNullException(nameof(service));

            _inner = inner;
            _service = service;

            IsArc = inner.IsArc;
            IsModulatedScanning = inner.IsModulatedScanning;
            IsProton = inner.IsProton;
            IsScanning = inner.IsScanning;
            IsStatic = inner.IsStatic;
        }


        public bool IsArc { get; private set; }

        public bool IsModulatedScanning { get; private set; }

        public bool IsProton { get; private set; }

        public bool IsScanning { get; private set; }

        public bool IsStatic { get; private set; }

        public Task RunAsync(Action<VMS.TPS.Common.Model.API.Technique> action) => _service.PostAsync((context) => action(_inner));
        public Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.Technique, T> func) => _service.PostAsync<T>((context) => func(_inner));

        // updates simple properties that might have changed
        public new void Refresh()
        {
            base.Refresh();

            IsArc = _inner.IsArc;
            IsModulatedScanning = _inner.IsModulatedScanning;
            IsProton = _inner.IsProton;
            IsScanning = _inner.IsScanning;
            IsStatic = _inner.IsStatic;
        }

        public static implicit operator VMS.TPS.Common.Model.API.Technique(AsyncTechnique wrapper) => wrapper._inner;

        // Internal Explicit Implementation to expose _inner safely for covariance
        VMS.TPS.Common.Model.API.Technique IEsapiWrapper<VMS.TPS.Common.Model.API.Technique>.Inner => _inner;

        // Explicit or Implicit implementation of Service
        // Since _service is private, we expose it via the interface
        IEsapiService IEsapiWrapper<VMS.TPS.Common.Model.API.Technique>.Service => _service;
    }
}
