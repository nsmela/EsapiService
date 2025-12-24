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
    public class AsyncPatientSupportDevice : AsyncApiDataObject, IPatientSupportDevice, IEsapiWrapper<VMS.TPS.Common.Model.API.PatientSupportDevice>
    {
        internal new readonly VMS.TPS.Common.Model.API.PatientSupportDevice _inner;

        // Store the inner ESAPI object reference
        // internal so other wrappers can access it
        // new to override any inherited _inner fields
        internal new readonly IEsapiService _service;

        public AsyncPatientSupportDevice(VMS.TPS.Common.Model.API.PatientSupportDevice inner, IEsapiService service) : base(inner, service)
        {
            if (inner is null) throw new ArgumentNullException(nameof(inner));
            if (service is null) throw new ArgumentNullException(nameof(service));

            _inner = inner;
            _service = service;

            PatientSupportAccessoryCode = inner.PatientSupportAccessoryCode;
            PatientSupportDeviceType = inner.PatientSupportDeviceType;
        }


        public string PatientSupportAccessoryCode { get; private set; }


        public string PatientSupportDeviceType { get; private set; }


        public Task RunAsync(Action<VMS.TPS.Common.Model.API.PatientSupportDevice> action) => _service.PostAsync((context) => action(_inner));
        public Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.PatientSupportDevice, T> func) => _service.PostAsync<T>((context) => func(_inner));

        // updates simple properties that might have changed
        public new void Refresh()
        {
            base.Refresh();

            PatientSupportAccessoryCode = _inner.PatientSupportAccessoryCode;
            PatientSupportDeviceType = _inner.PatientSupportDeviceType;
        }

        public static implicit operator VMS.TPS.Common.Model.API.PatientSupportDevice(AsyncPatientSupportDevice wrapper) => wrapper._inner;

        // Internal Explicit Implementation to expose _inner safely for covariance
        VMS.TPS.Common.Model.API.PatientSupportDevice IEsapiWrapper<VMS.TPS.Common.Model.API.PatientSupportDevice>.Inner => _inner;

        // Explicit or Implicit implementation of Service
        // Since _service is private, we expose it via the interface
        IEsapiService IEsapiWrapper<VMS.TPS.Common.Model.API.PatientSupportDevice>.Service => _service;
    }
}
