using System.Threading.Tasks;
using VMS.TPS.Common.Model.API;
using VMS.TPS.Common.Model.Types;

namespace EsapiService.Wrappers
{
    public class AsyncDiagnosis : IDiagnosis
    {
        internal readonly VMS.TPS.Common.Model.API.Diagnosis _inner;

        // Store the inner ESAPI object reference
        // internal so other wrappers can access it
        // new to override any inherited _inner fields
        internal new readonly IEsapiService _service;

        public AsyncDiagnosis(VMS.TPS.Common.Model.API.Diagnosis inner, IEsapiService service) : base(inner, service)
        {
            _inner = inner;
            _service = service;

            ClinicalDescription = inner.ClinicalDescription;
            Code = inner.Code;
            CodeTable = inner.CodeTable;
        }

        public string ClinicalDescription { get; }
        public string Code { get; }
        public string CodeTable { get; }

        public Task RunAsync(Action<VMS.TPS.Common.Model.API.Diagnosis> action) => _service.RunAsync(() => action(_inner));
        public Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.Diagnosis, T> func) => _service.RunAsync(() => func(_inner));
    }
}
    }
}
