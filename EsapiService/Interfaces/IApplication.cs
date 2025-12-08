using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Media;
using VMS.TPS.Common.Model.API;
using VMS.TPS.Common.Model.Types;
using Esapi.Services;
using Esapi.Interfaces;

namespace Esapi.Interfaces
{
    public interface IApplication : ISerializableObject
    {
        // --- Simple Properties --- //
        string SiteProgramDataDir { get; }

        // --- Accessors --- //
        Task<IUser> GetCurrentUserAsync();
        Task<ICalculation> GetCalculationAsync();
        Task<IActiveStructureCodeDictionaries> GetStructureCodesAsync();
        Task<IEquipment> GetEquipmentAsync();
        Task<IScriptEnvironment> GetScriptEnvironmentAsync();

        // --- Collections --- //
        Task<IReadOnlyList<IPatientSummary>> GetPatientSummariesAsync();

        // --- Methods --- //
        Task DisposeAsync();
        Task<IPatient> OpenPatientAsync(IPatientSummary patientSummary);
        Task<IPatient> OpenPatientByIdAsync(string id);
        Task ClosePatientAsync();
        Task SaveModificationsAsync();

        // --- RunAsync --- //
        /// <summary>
        /// Runs a function against the raw ESAPI VMS.TPS.Common.Model.API.Application object safely on the ESAPI thread.
        /// </summary>
        Task RunAsync(Action<VMS.TPS.Common.Model.API.Application> action);

        /// <summary>
        /// Runs a function against the raw ESAPI VMS.TPS.Common.Model.API.Application object safely on the ESAPI thread.
        /// </summary>
        Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.Application, T> func);
    }
}
