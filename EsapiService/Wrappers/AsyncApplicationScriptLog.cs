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
    public class AsyncApplicationScriptLog : AsyncApiDataObject, IApplicationScriptLog, IEsapiWrapper<VMS.TPS.Common.Model.API.ApplicationScriptLog>
    {
        internal new readonly VMS.TPS.Common.Model.API.ApplicationScriptLog _inner;

        // Store the inner ESAPI object reference
        // internal so other wrappers can access it
        // new to override any inherited _inner fields
        internal new readonly IEsapiService _service;

        public AsyncApplicationScriptLog(VMS.TPS.Common.Model.API.ApplicationScriptLog inner, IEsapiService service) : base(inner, service)
        {
            if (inner is null) throw new ArgumentNullException(nameof(inner));
            if (service is null) throw new ArgumentNullException(nameof(service));

            _inner = inner;
            _service = service;
        }


        public string CourseId =>
            _inner.CourseId;


        public string PatientId =>
            _inner.PatientId;


        public string PlanSetupId =>
            _inner.PlanSetupId;


        public string PlanUID =>
            _inner.PlanUID;


        public async Task<IApplicationScript> GetScriptAsync()
        {
            return await _service.PostAsync(context => {
                var innerResult = _inner.Script is null ? null : new AsyncApplicationScript(_inner.Script, _service);
                return innerResult;
            });
        }

        public string ScriptFullName =>
            _inner.ScriptFullName;


        public string StructureSetId =>
            _inner.StructureSetId;


        public string StructureSetUID =>
            _inner.StructureSetUID;


        public Task RunAsync(Action<VMS.TPS.Common.Model.API.ApplicationScriptLog> action) => _service.PostAsync((context) => action(_inner));
        public Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.ApplicationScriptLog, T> func) => _service.PostAsync<T>((context) => func(_inner));

        // --- Validates --- //
        /// <summary>
        /// Verifies is the wrapped ESAPI object isn't null.
        /// </summary>
        public new bool IsValid() => !IsNotValid();

        /// <summary>
        /// Verifies is the wrapped ESAPI object is null.
        /// </summary>
        public new bool IsNotValid() => _inner is null;

        public static implicit operator VMS.TPS.Common.Model.API.ApplicationScriptLog(AsyncApplicationScriptLog wrapper) => wrapper._inner;

        // Internal Explicit Implementation to expose _inner safely for covariance
        VMS.TPS.Common.Model.API.ApplicationScriptLog IEsapiWrapper<VMS.TPS.Common.Model.API.ApplicationScriptLog>.Inner => _inner;

        // Explicit or Implicit implementation of Service
        // Since _service is private, we expose it via the interface
        IEsapiService IEsapiWrapper<VMS.TPS.Common.Model.API.ApplicationScriptLog>.Service => _service;
    }
}
