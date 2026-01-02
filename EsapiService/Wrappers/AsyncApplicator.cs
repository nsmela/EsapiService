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
    public class AsyncApplicator : AsyncAddOn, IApplicator, IEsapiWrapper<VMS.TPS.Common.Model.API.Applicator>
    {
        internal new readonly VMS.TPS.Common.Model.API.Applicator _inner;

        // Store the inner ESAPI object reference
        // internal so other wrappers can access it
        // new to override any inherited _inner fields
        internal new readonly IEsapiService _service;

        public AsyncApplicator(VMS.TPS.Common.Model.API.Applicator inner, IEsapiService service) : base(inner, service)
        {
            if (inner is null) throw new ArgumentNullException(nameof(inner));
            if (service is null) throw new ArgumentNullException(nameof(service));

            _inner = inner;
            _service = service;
        }


        public double ApplicatorLengthInMM =>
            _inner.ApplicatorLengthInMM;


        public double DiameterInMM =>
            _inner.DiameterInMM;


        public double FieldSizeX =>
            _inner.FieldSizeX;


        public double FieldSizeY =>
            _inner.FieldSizeY;


        public bool IsStereotactic =>
            _inner.IsStereotactic;


        public Task RunAsync(Action<VMS.TPS.Common.Model.API.Applicator> action) => _service.PostAsync((context) => action(_inner));
        public Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.Applicator, T> func) => _service.PostAsync<T>((context) => func(_inner));

        // --- Validates --- //
        /// <summary>
        /// Verifies is the wrapped ESAPI object isn't null.
        /// </summary>
        public new bool IsValid() => !IsNotValid();

        /// <summary>
        /// Verifies is the wrapped ESAPI object is null.
        /// </summary>
        public new bool IsNotValid() => _inner is null;

        public static implicit operator VMS.TPS.Common.Model.API.Applicator(AsyncApplicator wrapper) => wrapper._inner;

        // Internal Explicit Implementation to expose _inner safely for covariance
        VMS.TPS.Common.Model.API.Applicator IEsapiWrapper<VMS.TPS.Common.Model.API.Applicator>.Inner => _inner;

        // Explicit or Implicit implementation of Service
        // Since _service is private, we expose it via the interface
        IEsapiService IEsapiWrapper<VMS.TPS.Common.Model.API.Applicator>.Service => _service;
    }
}
