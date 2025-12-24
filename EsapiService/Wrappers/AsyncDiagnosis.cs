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
    public class AsyncDiagnosis : AsyncApiDataObject, IDiagnosis, IEsapiWrapper<VMS.TPS.Common.Model.API.Diagnosis>
    {
        internal new readonly VMS.TPS.Common.Model.API.Diagnosis _inner;

        // Store the inner ESAPI object reference
        // internal so other wrappers can access it
        // new to override any inherited _inner fields
        internal new readonly IEsapiService _service;

        public AsyncDiagnosis(VMS.TPS.Common.Model.API.Diagnosis inner, IEsapiService service) : base(inner, service)
        {
            if (inner is null) throw new ArgumentNullException(nameof(inner));
            if (service is null) throw new ArgumentNullException(nameof(service));

            _inner = inner;
            _service = service;

            ClinicalDescription = inner.ClinicalDescription;
            Code = inner.Code;
            CodeTable = inner.CodeTable;
        }


        public string ClinicalDescription { get; private set; }


        public string Code { get; private set; }


        public string CodeTable { get; private set; }


        public Task RunAsync(Action<VMS.TPS.Common.Model.API.Diagnosis> action) => _service.PostAsync((context) => action(_inner));
        public Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.Diagnosis, T> func) => _service.PostAsync<T>((context) => func(_inner));

        // updates simple properties that might have changed
        public new void Refresh()
        {
            base.Refresh();

            ClinicalDescription = _inner.ClinicalDescription;
            Code = _inner.Code;
            CodeTable = _inner.CodeTable;
        }

        public static implicit operator VMS.TPS.Common.Model.API.Diagnosis(AsyncDiagnosis wrapper) => wrapper._inner;

        // Internal Explicit Implementation to expose _inner safely for covariance
        VMS.TPS.Common.Model.API.Diagnosis IEsapiWrapper<VMS.TPS.Common.Model.API.Diagnosis>.Inner => _inner;

        // Explicit or Implicit implementation of Service
        // Since _service is private, we expose it via the interface
        IEsapiService IEsapiWrapper<VMS.TPS.Common.Model.API.Diagnosis>.Service => _service;
    }
}
