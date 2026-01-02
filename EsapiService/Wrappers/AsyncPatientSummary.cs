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
    public class AsyncPatientSummary : AsyncSerializableObject, IPatientSummary, IEsapiWrapper<VMS.TPS.Common.Model.API.PatientSummary>
    {
        internal new readonly VMS.TPS.Common.Model.API.PatientSummary _inner;

        // Store the inner ESAPI object reference
        // internal so other wrappers can access it
        // new to override any inherited _inner fields
        internal new readonly IEsapiService _service;

        public AsyncPatientSummary(VMS.TPS.Common.Model.API.PatientSummary inner, IEsapiService service) : base(inner, service)
        {
            if (inner is null) throw new ArgumentNullException(nameof(inner));
            if (service is null) throw new ArgumentNullException(nameof(service));

            _inner = inner;
            _service = service;
        }


        public DateTime? CreationDateTime =>
            _inner.CreationDateTime;


        public DateTime? DateOfBirth =>
            _inner.DateOfBirth;


        public string FirstName =>
            _inner.FirstName;


        public string Id =>
            _inner.Id;


        public string Id2 =>
            _inner.Id2;


        public string LastName =>
            _inner.LastName;


        public string MiddleName =>
            _inner.MiddleName;


        public string Sex =>
            _inner.Sex;


        public string SSN =>
            _inner.SSN;


        public Task RunAsync(Action<VMS.TPS.Common.Model.API.PatientSummary> action) => _service.PostAsync((context) => action(_inner));
        public Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.PatientSummary, T> func) => _service.PostAsync<T>((context) => func(_inner));

        // --- Validates --- //
        /// <summary>
        /// Verifies is the wrapped ESAPI object isn't null.
        /// </summary>
        public new bool IsValid() => !IsNotValid();

        /// <summary>
        /// Verifies is the wrapped ESAPI object is null.
        /// </summary>
        public new bool IsNotValid() => _inner is null;

        public static implicit operator VMS.TPS.Common.Model.API.PatientSummary(AsyncPatientSummary wrapper) => wrapper._inner;

        // Internal Explicit Implementation to expose _inner safely for covariance
        VMS.TPS.Common.Model.API.PatientSummary IEsapiWrapper<VMS.TPS.Common.Model.API.PatientSummary>.Inner => _inner;

        // Explicit or Implicit implementation of Service
        // Since _service is private, we expose it via the interface
        IEsapiService IEsapiWrapper<VMS.TPS.Common.Model.API.PatientSummary>.Service => _service;
    }
}
