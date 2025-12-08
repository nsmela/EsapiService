using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;
using VMS.TPS.Common.Model.API;
using VMS.TPS.Common.Model.Types;
using Esapi.Interfaces;
using Esapi.Services;

namespace Esapi.Wrappers
{
    public class AsyncScriptContext : IScriptContext
    {
        internal readonly VMS.TPS.Common.Model.API.ScriptContext _inner;

        // Store the inner ESAPI object reference
        // internal so other wrappers can access it
        // new to override any inherited _inner fields
        internal readonly IEsapiService _service;

        public AsyncScriptContext(VMS.TPS.Common.Model.API.ScriptContext inner, IEsapiService service)
        {
            _inner = inner;
            _service = service;

            ApplicationName = inner.ApplicationName;
            VersionInfo = inner.VersionInfo;
        }


        public async Task<IUser> GetCurrentUserAsync()
        {
            return await _service.RunAsync(() => 
                _inner.CurrentUser is null ? null : new AsyncUser(_inner.CurrentUser, _service));
        }

        public async Task<ICourse> GetCourseAsync()
        {
            return await _service.RunAsync(() => 
                _inner.Course is null ? null : new AsyncCourse(_inner.Course, _service));
        }

        public async Task<IImage> GetImageAsync()
        {
            return await _service.RunAsync(() => 
                _inner.Image is null ? null : new AsyncImage(_inner.Image, _service));
        }

        public async Task<IStructureSet> GetStructureSetAsync()
        {
            return await _service.RunAsync(() => 
                _inner.StructureSet is null ? null : new AsyncStructureSet(_inner.StructureSet, _service));
        }

        public async Task<ICalculation> GetCalculationAsync()
        {
            return await _service.RunAsync(() => 
                _inner.Calculation is null ? null : new AsyncCalculation(_inner.Calculation, _service));
        }

        public async Task<IActiveStructureCodeDictionaries> GetStructureCodesAsync()
        {
            return await _service.RunAsync(() => 
                _inner.StructureCodes is null ? null : new AsyncActiveStructureCodeDictionaries(_inner.StructureCodes, _service));
        }

        public async Task SetStructureCodesAsync(IActiveStructureCodeDictionaries value)
        {
            // Handle null assignment
            if (value is null)
            {
                await _service.RunAsync(() => _inner.StructureCodes = null);
                return;
            }
            // Unwrap the interface to get the Varian object
            if (value is AsyncActiveStructureCodeDictionaries wrapper)
            {
                 await _service.RunAsync(() => _inner.StructureCodes = wrapper._inner);
                 return;
            }
            throw new System.ArgumentException("Value must be of type AsyncActiveStructureCodeDictionaries");
        }

        public async Task<IEquipment> GetEquipmentAsync()
        {
            return await _service.RunAsync(() => 
                _inner.Equipment is null ? null : new AsyncEquipment(_inner.Equipment, _service));
        }

        public async Task SetEquipmentAsync(IEquipment value)
        {
            // Handle null assignment
            if (value is null)
            {
                await _service.RunAsync(() => _inner.Equipment = null);
                return;
            }
            // Unwrap the interface to get the Varian object
            if (value is AsyncEquipment wrapper)
            {
                 await _service.RunAsync(() => _inner.Equipment = wrapper._inner);
                 return;
            }
            throw new System.ArgumentException("Value must be of type AsyncEquipment");
        }

        public async Task<IPatient> GetPatientAsync()
        {
            return await _service.RunAsync(() => 
                _inner.Patient is null ? null : new AsyncPatient(_inner.Patient, _service));
        }

        public async Task<IPlanSetup> GetPlanSetupAsync()
        {
            return await _service.RunAsync(() => 
                _inner.PlanSetup is null ? null : new AsyncPlanSetup(_inner.PlanSetup, _service));
        }

        public async Task<IExternalPlanSetup> GetExternalPlanSetupAsync()
        {
            return await _service.RunAsync(() => 
                _inner.ExternalPlanSetup is null ? null : new AsyncExternalPlanSetup(_inner.ExternalPlanSetup, _service));
        }

        public async Task<IBrachyPlanSetup> GetBrachyPlanSetupAsync()
        {
            return await _service.RunAsync(() => 
                _inner.BrachyPlanSetup is null ? null : new AsyncBrachyPlanSetup(_inner.BrachyPlanSetup, _service));
        }

        public async Task<IIonPlanSetup> GetIonPlanSetupAsync()
        {
            return await _service.RunAsync(() => 
                _inner.IonPlanSetup is null ? null : new AsyncIonPlanSetup(_inner.IonPlanSetup, _service));
        }

        public async Task<IReadOnlyList<IPlanSetup>> GetPlansInScopeAsync()
        {
            return await _service.RunAsync(() => 
                _inner.PlansInScope?.Select(x => new AsyncPlanSetup(x, _service)).ToList());
        }


        public async Task<IReadOnlyList<IExternalPlanSetup>> GetExternalPlansInScopeAsync()
        {
            return await _service.RunAsync(() => 
                _inner.ExternalPlansInScope?.Select(x => new AsyncExternalPlanSetup(x, _service)).ToList());
        }


        public async Task<IReadOnlyList<IBrachyPlanSetup>> GetBrachyPlansInScopeAsync()
        {
            return await _service.RunAsync(() => 
                _inner.BrachyPlansInScope?.Select(x => new AsyncBrachyPlanSetup(x, _service)).ToList());
        }


        public async Task<IReadOnlyList<IIonPlanSetup>> GetIonPlansInScopeAsync()
        {
            return await _service.RunAsync(() => 
                _inner.IonPlansInScope?.Select(x => new AsyncIonPlanSetup(x, _service)).ToList());
        }


        public async Task<IReadOnlyList<IPlanSum>> GetPlanSumsInScopeAsync()
        {
            return await _service.RunAsync(() => 
                _inner.PlanSumsInScope?.Select(x => new AsyncPlanSum(x, _service)).ToList());
        }


        public async Task<IPlanSum> GetPlanSumAsync()
        {
            return await _service.RunAsync(() => 
                _inner.PlanSum is null ? null : new AsyncPlanSum(_inner.PlanSum, _service));
        }

        public string ApplicationName { get; }

        public string VersionInfo { get; }

        public Task RunAsync(Action<VMS.TPS.Common.Model.API.ScriptContext> action) => _service.RunAsync(() => action(_inner));
        public Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.ScriptContext, T> func) => _service.RunAsync(() => func(_inner));
    }
}
