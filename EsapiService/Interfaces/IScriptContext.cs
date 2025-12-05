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
        Task<IUser> GetCurrentUserAsync();
        Task<ICourse> GetCourseAsync();
        Task<IImage> GetImageAsync();
        Task<IStructureSet> GetStructureSetAsync();
        Task<ICalculation> GetCalculationAsync();
        Task<IActiveStructureCodeDictionaries> GetStructureCodesAsync();
        Task SetStructureCodesAsync(IActiveStructureCodeDictionaries value);
        Task<IEquipment> GetEquipmentAsync();
        Task SetEquipmentAsync(IEquipment value);
        Task<IPatient> GetPatientAsync();
        Task<IPlanSetup> GetPlanSetupAsync();
        Task<IExternalPlanSetup> GetExternalPlanSetupAsync();
        Task<IBrachyPlanSetup> GetBrachyPlanSetupAsync();
        Task<IIonPlanSetup> GetIonPlanSetupAsync();
        System.Collections.Generic.IReadOnlyList<IPlanSetup> PlansInScope { get; }
        System.Collections.Generic.IReadOnlyList<IExternalPlanSetup> ExternalPlansInScope { get; }
        System.Collections.Generic.IReadOnlyList<IBrachyPlanSetup> BrachyPlansInScope { get; }
        System.Collections.Generic.IReadOnlyList<IIonPlanSetup> IonPlansInScope { get; }
        System.Collections.Generic.IReadOnlyList<IPlanSum> PlanSumsInScope { get; }
        Task<IPlanSum> GetPlanSumAsync();
        string ApplicationName { get; }
        string VersionInfo { get; }

        /// <summary>
        /// Runs a function against the raw ESAPI VMS.TPS.Common.Model.API.ScriptContext object safely on the ESAPI thread.
        /// </summary>
        Task RunAsync(Action<VMS.TPS.Common.Model.API.ScriptContext> action);

        /// <summary>
        /// Runs a function against the raw ESAPI VMS.TPS.Common.Model.API.ScriptContext object safely on the ESAPI thread.
        /// </summary>
        Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.ScriptContext, T> func);
    }
}
