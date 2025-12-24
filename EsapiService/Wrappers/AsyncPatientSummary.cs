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

            CreationDateTime = inner.CreationDateTime;
            DateOfBirth = inner.DateOfBirth;
            FirstName = inner.FirstName;
            Id = inner.Id;
            Id2 = inner.Id2;
            LastName = inner.LastName;
            MiddleName = inner.MiddleName;
            Sex = inner.Sex;
            SSN = inner.SSN;
        }


        public DateTime? CreationDateTime { get; private set; }


        public DateTime? DateOfBirth { get; private set; }


        public string FirstName { get; private set; }


        public string Id { get; private set; }


        public string Id2 { get; private set; }


        public string LastName { get; private set; }


        public string MiddleName { get; private set; }


        public string Sex { get; private set; }


        public string SSN { get; private set; }


        public Task RunAsync(Action<VMS.TPS.Common.Model.API.PatientSummary> action) => _service.PostAsync((context) => action(_inner));
        public Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.PatientSummary, T> func) => _service.PostAsync<T>((context) => func(_inner));

        // updates simple properties that might have changed
        public new void Refresh()
        {
            base.Refresh();

            CreationDateTime = _inner.CreationDateTime;
            DateOfBirth = _inner.DateOfBirth;
            FirstName = _inner.FirstName;
            Id = _inner.Id;
            Id2 = _inner.Id2;
            LastName = _inner.LastName;
            MiddleName = _inner.MiddleName;
            Sex = _inner.Sex;
            SSN = _inner.SSN;
        }

        public static implicit operator VMS.TPS.Common.Model.API.PatientSummary(AsyncPatientSummary wrapper) => wrapper._inner;

        // Internal Explicit Implementation to expose _inner safely for covariance
        VMS.TPS.Common.Model.API.PatientSummary IEsapiWrapper<VMS.TPS.Common.Model.API.PatientSummary>.Inner => _inner;

        // Explicit or Implicit implementation of Service
        // Since _service is private, we expose it via the interface
        IEsapiService IEsapiWrapper<VMS.TPS.Common.Model.API.PatientSummary>.Service => _service;
    }
}
