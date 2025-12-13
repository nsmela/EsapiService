using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Media;
using VMS.TPS.Common.Model.API;
using VMS.TPS.Common.Model.Types;
using Esapi.Services;

namespace Esapi.Interfaces
{
    public interface IScriptContext
    {
        // --- Simple Properties --- //
        IEnumerable<PlanSetup> PlansInScope { get; }
        IEnumerable<ExternalPlanSetup> ExternalPlansInScope { get; }
        IEnumerable<BrachyPlanSetup> BrachyPlansInScope { get; }
        IEnumerable<IonPlanSetup> IonPlansInScope { get; }
        IEnumerable<PlanSum> PlanSumsInScope { get; }
        string ApplicationName { get; }
        string VersionInfo { get; }

        // --- Accessors --- //
        Task<IUser> GetCurrentUserAsync(); // read complex property
        Task<ICourse> GetCourseAsync(); // read complex property
        Task<IImage> GetImageAsync(); // read complex property
        Task<IStructureSet> GetStructureSetAsync(); // read complex property
        Task<ICalculation> GetCalculationAsync(); // read complex property
        Task<IActiveStructureCodeDictionaries> GetStructureCodesAsync(); // read complex property
        Task SetStructureCodesAsync(IActiveStructureCodeDictionaries value); // write complex property
        Task<IEquipment> GetEquipmentAsync(); // read complex property
        Task SetEquipmentAsync(IEquipment value); // write complex property
        Task<IPatient> GetPatientAsync(); // read complex property
        Task<IPlanSetup> GetPlanSetupAsync(); // read complex property
        Task<IExternalPlanSetup> GetExternalPlanSetupAsync(); // read complex property
        Task<IBrachyPlanSetup> GetBrachyPlanSetupAsync(); // read complex property
        Task<IIonPlanSetup> GetIonPlanSetupAsync(); // read complex property
        Task<IPlanSum> GetPlanSumAsync(); // read complex property

        // --- RunAsync --- //
        /// <summary>
        /// Runs a function against the raw ESAPI VMS.TPS.Common.Model.API.ScriptContext object safely on the ESAPI thread.
        /// </summary>
        Task RunAsync(Action<VMS.TPS.Common.Model.API.ScriptContext> action);

        /// <summary>
        /// Runs a function against the raw ESAPI VMS.TPS.Common.Model.API.ScriptContext object safely on the ESAPI thread.
        /// </summary>
        Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.ScriptContext, T> func);

        /* --- Skipped Members (Not generated) ---
           - .ctor: Explicitly ignored by name
           - .ctor: Explicitly ignored by name
        */
    }
}
