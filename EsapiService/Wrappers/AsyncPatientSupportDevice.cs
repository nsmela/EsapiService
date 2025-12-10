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
    public class AsyncPatientSupportDevice : AsyncApiDataObject, IPatientSupportDevice
    {
        internal new readonly VMS.TPS.Common.Model.API.PatientSupportDevice _inner;

        // Store the inner ESAPI object reference
        // internal so other wrappers can access it
        // new to override any inherited _inner fields
        internal new readonly IEsapiService _service;

        public AsyncPatientSupportDevice(VMS.TPS.Common.Model.API.PatientSupportDevice inner, IEsapiService service) : base(inner, service)
        {
            _inner = inner;
            _service = service;

            PatientSupportAccessoryCode = inner.PatientSupportAccessoryCode;
            PatientSupportDeviceType = inner.PatientSupportDeviceType;
        }


        public string PatientSupportAccessoryCode { get; }

        public string PatientSupportDeviceType { get; }

        public Task RunAsync(Action<VMS.TPS.Common.Model.API.PatientSupportDevice> action) => _service.PostAsync((context) => action(_inner));
        public Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.PatientSupportDevice, T> func) => _service.PostAsync<T>((context) => func(_inner));
    }
}
