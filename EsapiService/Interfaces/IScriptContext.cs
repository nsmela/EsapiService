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
    public partial interface IScriptContext
    {
        // --- Simple Properties --- //
        string ApplicationName { get; } // simple property
        string VersionInfo { get; } // simple property

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

        // --- Collections --- //
        Task<IReadOnlyList<IPlanSetup>> GetPlansInScopeAsync(); // collection property context
        Task<IReadOnlyList<IExternalPlanSetup>> GetExternalPlansInScopeAsync(); // collection property context
        Task<IReadOnlyList<IBrachyPlanSetup>> GetBrachyPlansInScopeAsync(); // collection property context
        Task<IReadOnlyList<IIonPlanSetup>> GetIonPlansInScopeAsync(); // collection property context
        Task<IReadOnlyList<IPlanSum>> GetPlanSumsInScopeAsync(); // collection property context

        // --- RunAsync --- //
        /// <summary>
        /// Runs a function against the raw ESAPI VMS.TPS.Common.Model.API.ScriptContext object safely on the ESAPI thread.
        /// </summary>
        Task RunAsync(Action<VMS.TPS.Common.Model.API.ScriptContext> action);

        /// <summary>
        /// Runs a function against the raw ESAPI VMS.TPS.Common.Model.API.ScriptContext object safely on the ESAPI thread.
        /// </summary>
        Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.ScriptContext, T> func);

        // --- Validates --- //
        /// <summary>
        /// Verifies is the wrapped ESAPI object isn't null.
        /// </summary>
        bool IsValid();

        /// <summary>
        /// Verifies is the wrapped ESAPI object is null.
        /// </summary>
        bool IsNotValid();

        /* --- Skipped Members (Not generated) ---
           - .ctor: Explicitly ignored by name
        */
    }
}
