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
    public class AsyncPatientSummary : AsyncSerializableObject, IPatientSummary
    {
        internal readonly VMS.TPS.Common.Model.API.PatientSummary _inner;

        // Store the inner ESAPI object reference
        // internal so other wrappers can access it
        // new to override any inherited _inner fields
        internal new readonly IEsapiService _service;

        public AsyncPatientSummary(VMS.TPS.Common.Model.API.PatientSummary inner, IEsapiService service) : base(inner, service)
        {
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


        public DateTime? CreationDateTime { get; }

        public DateTime? DateOfBirth { get; }

        public string FirstName { get; }

        public string Id { get; }

        public string Id2 { get; }

        public string LastName { get; }

        public string MiddleName { get; }

        public string Sex { get; }

        public string SSN { get; }

        public Task RunAsync(Action<VMS.TPS.Common.Model.API.PatientSummary> action) => _service.PostAsync((context) => action(_inner));
        public Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.PatientSummary, T> func) => _service.PostAsync<T>((context) => func(_inner));
    }
}
