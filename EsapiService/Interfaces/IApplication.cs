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
        Task DisposeAsync();
        Task<IPatient> OpenPatientAsync(VMS.TPS.Common.Model.API.PatientSummary patientSummary);
        Task<IPatient> OpenPatientByIdAsync(string id);
        Task ClosePatientAsync();
        Task SaveModificationsAsync();
        Task WriteXmlAsync(System.Xml.XmlWriter writer);
        Task<IUser> GetCurrentUserAsync();
        string SiteProgramDataDir { get; }
        System.Collections.Generic.IReadOnlyList<IPatientSummary> PatientSummaries { get; }
        Task<ICalculation> GetCalculationAsync();
        Task<IActiveStructureCodeDictionaries> GetStructureCodesAsync();
        Task<IEquipment> GetEquipmentAsync();
        Task<IScriptEnvironment> GetScriptEnvironmentAsync();

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
