namespace EsapiService.Wrappers
{
    using System.Linq;
    using System.Collections.Generic;
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

        public IUser CurrentUser => _inner.CurrentUser is null ? null : new AsyncUser(_inner.CurrentUser, _service);

        public ICourse Course => _inner.Course is null ? null : new AsyncCourse(_inner.Course, _service);

        public IImage Image => _inner.Image is null ? null : new AsyncImage(_inner.Image, _service);

        public IStructureSet StructureSet => _inner.StructureSet is null ? null : new AsyncStructureSet(_inner.StructureSet, _service);

        public ICalculation Calculation => _inner.Calculation is null ? null : new AsyncCalculation(_inner.Calculation, _service);

        public IActiveStructureCodeDictionaries StructureCodes => _inner.StructureCodes is null ? null : new AsyncActiveStructureCodeDictionaries(_inner.StructureCodes, _service);
        public System.Threading.Tasks.Task SetStructureCodesAsync(IActiveStructureCodeDictionaries value)
        {
            // Unwrap the interface to get the Varian object
            if (value is AsyncActiveStructureCodeDictionaries wrapper)
            {
                 return _service.RunAsync(() => _inner.StructureCodes = wrapper._inner);
            }
            throw new System.ArgumentException("Value must be of type AsyncActiveStructureCodeDictionaries");
        }

        public IEquipment Equipment => _inner.Equipment is null ? null : new AsyncEquipment(_inner.Equipment, _service);
        public System.Threading.Tasks.Task SetEquipmentAsync(IEquipment value)
        {
            // Unwrap the interface to get the Varian object
            if (value is AsyncEquipment wrapper)
            {
                 return _service.RunAsync(() => _inner.Equipment = wrapper._inner);
            }
            throw new System.ArgumentException("Value must be of type AsyncEquipment");
        }

        public IPatient Patient => _inner.Patient is null ? null : new AsyncPatient(_inner.Patient, _service);

        public IPlanSetup PlanSetup => _inner.PlanSetup is null ? null : new AsyncPlanSetup(_inner.PlanSetup, _service);

        public IExternalPlanSetup ExternalPlanSetup => _inner.ExternalPlanSetup is null ? null : new AsyncExternalPlanSetup(_inner.ExternalPlanSetup, _service);

        public IBrachyPlanSetup BrachyPlanSetup => _inner.BrachyPlanSetup is null ? null : new AsyncBrachyPlanSetup(_inner.BrachyPlanSetup, _service);

        public IIonPlanSetup IonPlanSetup => _inner.IonPlanSetup is null ? null : new AsyncIonPlanSetup(_inner.IonPlanSetup, _service);

        public System.Collections.Generic.IReadOnlyList<IPlanSetup> PlansInScope => _inner.PlansInScope?.Select(x => new AsyncPlanSetup(x, _service)).ToList();
        public System.Collections.Generic.IReadOnlyList<IExternalPlanSetup> ExternalPlansInScope => _inner.ExternalPlansInScope?.Select(x => new AsyncExternalPlanSetup(x, _service)).ToList();
        public System.Collections.Generic.IReadOnlyList<IBrachyPlanSetup> BrachyPlansInScope => _inner.BrachyPlansInScope?.Select(x => new AsyncBrachyPlanSetup(x, _service)).ToList();
        public System.Collections.Generic.IReadOnlyList<IIonPlanSetup> IonPlansInScope => _inner.IonPlansInScope?.Select(x => new AsyncIonPlanSetup(x, _service)).ToList();
        public System.Collections.Generic.IReadOnlyList<IPlanSum> PlanSumsInScope => _inner.PlanSumsInScope?.Select(x => new AsyncPlanSum(x, _service)).ToList();
        public IPlanSum PlanSum => _inner.PlanSum is null ? null : new AsyncPlanSum(_inner.PlanSum, _service);

        public string ApplicationName { get; }
        public string VersionInfo { get; }

        public Task RunAsync(Action<VMS.TPS.Common.Model.API.ScriptContext> action) => _service.RunAsync(() => action(_inner));
        public Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.ScriptContext, T> func) => _service.RunAsync(() => func(_inner));
    }
}
    }
}
