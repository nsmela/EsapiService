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
            if (inner == null) throw new ArgumentNullException(nameof(inner));
            if (service == null) throw new ArgumentNullException(nameof(service));

            _inner = inner;
            _service = service;

            ApplicationName = inner.ApplicationName;
            VersionInfo = inner.VersionInfo;
        }

        public async Task<IUser> GetCurrentUserAsync()
        {
            var result = await _service.PostAsync(context => 
                _inner.CurrentUser is null ? null : new AsyncUser(_inner.CurrentUser, _service));
            return result;
        }

        public async Task<ICourse> GetCourseAsync()
        {
            var result = await _service.PostAsync(context => 
                _inner.Course is null ? null : new AsyncCourse(_inner.Course, _service));
            return result;
        }

        public async Task<IImage> GetImageAsync()
        {
            var result = await _service.PostAsync(context => 
                _inner.Image is null ? null : new AsyncImage(_inner.Image, _service));
            return result;
        }

        public async Task<IStructureSet> GetStructureSetAsync()
        {
            var result = await _service.PostAsync(context => 
                _inner.StructureSet is null ? null : new AsyncStructureSet(_inner.StructureSet, _service));
            return result;
        }

        public async Task<ICalculation> GetCalculationAsync()
        {
            var result = await _service.PostAsync(context => 
                _inner.Calculation is null ? null : new AsyncCalculation(_inner.Calculation, _service));
            return result;
        }

        public async Task<IActiveStructureCodeDictionaries> GetStructureCodesAsync()
        {
            var result = await _service.PostAsync(context => 
                _inner.StructureCodes is null ? null : new AsyncActiveStructureCodeDictionaries(_inner.StructureCodes, _service));
            return result;
        }

        public async Task SetStructureCodesAsync(IActiveStructureCodeDictionaries value)
        {
            if (value is null)
            {
                await _service.PostAsync(context => _inner.StructureCodes = null);
                return;
            }
            if (value is IEsapiWrapper<ActiveStructureCodeDictionaries> wrapper)
            {
                 await _service.PostAsync(context => _inner.StructureCodes = wrapper.Inner);
                 return;
            }
            throw new System.ArgumentException("Value must be of type AsyncActiveStructureCodeDictionaries");
        }

        public async Task<IEquipment> GetEquipmentAsync()
        {
            var result = await _service.PostAsync(context => 
                _inner.Equipment is null ? null : new AsyncEquipment(_inner.Equipment, _service));
            return result;
        }

        public async Task SetEquipmentAsync(IEquipment value)
        {
            if (value is null)
            {
                await _service.PostAsync(context => _inner.Equipment = null);
                return;
            }
            if (value is IEsapiWrapper<Equipment> wrapper)
            {
                 await _service.PostAsync(context => _inner.Equipment = wrapper.Inner);
                 return;
            }
            throw new System.ArgumentException("Value must be of type AsyncEquipment");
        }

        public async Task<IPatient> GetPatientAsync()
        {
            var result = await _service.PostAsync(context => 
                _inner.Patient is null ? null : new AsyncPatient(_inner.Patient, _service));
            return result;
        }

        public async Task<IPlanSetup> GetPlanSetupAsync()
        {
            var result = await _service.PostAsync(context => 
                _inner.PlanSetup is null ? null : new AsyncPlanSetup(_inner.PlanSetup, _service));
            return result;
        }

        public async Task<IExternalPlanSetup> GetExternalPlanSetupAsync()
        {
            var result = await _service.PostAsync(context => 
                _inner.ExternalPlanSetup is null ? null : new AsyncExternalPlanSetup(_inner.ExternalPlanSetup, _service));
            return result;
        }

        public async Task<IBrachyPlanSetup> GetBrachyPlanSetupAsync()
        {
            var result = await _service.PostAsync(context => 
                _inner.BrachyPlanSetup is null ? null : new AsyncBrachyPlanSetup(_inner.BrachyPlanSetup, _service));
            return result;
        }

        public async Task<IIonPlanSetup> GetIonPlanSetupAsync()
        {
            var result = await _service.PostAsync(context => 
                _inner.IonPlanSetup is null ? null : new AsyncIonPlanSetup(_inner.IonPlanSetup, _service));
            return result;
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


        public async Task<IPlanSum> GetPlanSumAsync()
        {
            var result = await _service.PostAsync(context => 
                _inner.PlanSum is null ? null : new AsyncPlanSum(_inner.PlanSum, _service));
            return result;
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
