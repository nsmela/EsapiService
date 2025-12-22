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
    public sealed class AsyncScriptContext : IScriptContext, IEsapiWrapper<VMS.TPS.Common.Model.API.ScriptContext>
    {
        internal readonly VMS.TPS.Common.Model.API.ScriptContext _inner;

        // Store the inner ESAPI object reference
        // internal so other wrappers can access it
        // new to override any inherited _inner fields
        internal readonly IEsapiService _service;

        public AsyncScriptContext(VMS.TPS.Common.Model.API.ScriptContext inner, IEsapiService service)
        {
            if (inner is null) throw new ArgumentNullException(nameof(inner));
            if (service is null) throw new ArgumentNullException(nameof(service));

            _inner = inner;
            _service = service;

            ApplicationName = inner.ApplicationName;
            VersionInfo = inner.VersionInfo;
        }


        public async Task<IUser> GetCurrentUserAsync()
        {
            return await _service.PostAsync(context => {
                var innerResult = _inner.CurrentUser is null ? null : new AsyncUser(_inner.CurrentUser, _service);
                return innerResult;
            });
        }

        public async Task<ICourse> GetCourseAsync()
        {
            return await _service.PostAsync(context => {
                var innerResult = _inner.Course is null ? null : new AsyncCourse(_inner.Course, _service);
                return innerResult;
            });
        }

        public async Task<IImage> GetImageAsync()
        {
            return await _service.PostAsync(context => {
                var innerResult = _inner.Image is null ? null : new AsyncImage(_inner.Image, _service);
                return innerResult;
            });
        }

        public async Task<IStructureSet> GetStructureSetAsync()
        {
            return await _service.PostAsync(context => {
                var innerResult = _inner.StructureSet is null ? null : new AsyncStructureSet(_inner.StructureSet, _service);
                return innerResult;
            });
        }

        public async Task<IPatient> GetPatientAsync()
        {
            return await _service.PostAsync(context => {
                var innerResult = _inner.Patient is null ? null : new AsyncPatient(_inner.Patient, _service);
                return innerResult;
            });
        }

        public async Task<IPlanSetup> GetPlanSetupAsync()
        {
            return await _service.PostAsync(context => {
                var innerResult = _inner.PlanSetup is null ? null : new AsyncPlanSetup(_inner.PlanSetup, _service);
                return innerResult;
            });
        }

        public async Task<IExternalPlanSetup> GetExternalPlanSetupAsync()
        {
            return await _service.PostAsync(context => {
                var innerResult = _inner.ExternalPlanSetup is null ? null : new AsyncExternalPlanSetup(_inner.ExternalPlanSetup, _service);
                return innerResult;
            });
        }

        public async Task<IBrachyPlanSetup> GetBrachyPlanSetupAsync()
        {
            return await _service.PostAsync(context => {
                var innerResult = _inner.BrachyPlanSetup is null ? null : new AsyncBrachyPlanSetup(_inner.BrachyPlanSetup, _service);
                return innerResult;
            });
        }

        public async Task<IIonPlanSetup> GetIonPlanSetupAsync()
        {
            return await _service.PostAsync(context => {
                var innerResult = _inner.IonPlanSetup is null ? null : new AsyncIonPlanSetup(_inner.IonPlanSetup, _service);
                return innerResult;
            });
        }

        public async Task<IReadOnlyList<IPlanSetup>> GetPlansInScopeAsync()
        {
            return await _service.PostAsync(context => 
                _inner.PlansInScope?.Select(x => new AsyncPlanSetup(x, _service)).ToList());
        }


        public async Task<IReadOnlyList<IExternalPlanSetup>> GetExternalPlansInScopeAsync()
        {
            return await _service.PostAsync(context => 
                _inner.ExternalPlansInScope?.Select(x => new AsyncExternalPlanSetup(x, _service)).ToList());
        }


        public async Task<IReadOnlyList<IBrachyPlanSetup>> GetBrachyPlansInScopeAsync()
        {
            return await _service.PostAsync(context => 
                _inner.BrachyPlansInScope?.Select(x => new AsyncBrachyPlanSetup(x, _service)).ToList());
        }


        public async Task<IReadOnlyList<IIonPlanSetup>> GetIonPlansInScopeAsync()
        {
            return await _service.PostAsync(context => 
                _inner.IonPlansInScope?.Select(x => new AsyncIonPlanSetup(x, _service)).ToList());
        }


        public async Task<IReadOnlyList<IPlanSum>> GetPlanSumsInScopeAsync()
        {
            return await _service.PostAsync(context => 
                _inner.PlanSumsInScope?.Select(x => new AsyncPlanSum(x, _service)).ToList());
        }


        public string ApplicationName { get; }

        public string VersionInfo { get; }

        public Task RunAsync(Action<VMS.TPS.Common.Model.API.ScriptContext> action) => _service.PostAsync((context) => action(_inner));
        public Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.ScriptContext, T> func) => _service.PostAsync<T>((context) => func(_inner));

        public static implicit operator VMS.TPS.Common.Model.API.ScriptContext(AsyncScriptContext wrapper) => wrapper._inner;

        // Internal Explicit Implementation to expose _inner safely for covariance
        VMS.TPS.Common.Model.API.ScriptContext IEsapiWrapper<VMS.TPS.Common.Model.API.ScriptContext>.Inner => _inner;

        // Explicit or Implicit implementation of Service
        // Since _service is private, we expose it via the interface
        IEsapiService IEsapiWrapper<VMS.TPS.Common.Model.API.ScriptContext>.Service => _service;

        /* --- Skipped Members (Not generated) ---
           - .ctor: Explicitly ignored by name
        */
    }
}
