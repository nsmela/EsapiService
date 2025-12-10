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
    public interface IApplication : ISerializableObject
    {
        // --- Simple Properties --- //
        string SiteProgramDataDir { get; }

        // --- Accessors --- //
        Task<IUser> GetCurrentUserAsync(); // read complex property
        Task<ICalculation> GetCalculationAsync(); // read complex property
        Task<IActiveStructureCodeDictionaries> GetStructureCodesAsync(); // read complex property
        Task<IEquipment> GetEquipmentAsync(); // read complex property
        Task<IScriptEnvironment> GetScriptEnvironmentAsync(); // read complex property

        // --- Collections --- //
        Task<IReadOnlyList<IPatientSummary>> GetPatientSummariesAsync(); // collection proeprty context

        // --- Methods --- //
        Task DisposeAsync(); // void method
        Task<IPatient> OpenPatientAsync(IPatientSummary patientSummary); // complex method
        Task<IPatient> OpenPatientByIdAsync(string id); // complex method
        Task ClosePatientAsync(); // void method
        Task SaveModificationsAsync(); // void method

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
