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
    public interface IPatientSummary : ISerializableObject
    {
        // --- Simple Properties --- //
        DateTime? CreationDateTime { get; }
        DateTime? DateOfBirth { get; }
        string FirstName { get; }
        string Id { get; }
        string Id2 { get; }
        string LastName { get; }
        string MiddleName { get; }
        string Sex { get; }
        string SSN { get; }

        // --- RunAsync --- //
        /// <summary>
        /// Runs a function against the raw ESAPI VMS.TPS.Common.Model.API.PatientSummary object safely on the ESAPI thread.
        /// </summary>
        Task RunAsync(Action<VMS.TPS.Common.Model.API.PatientSummary> action);

        /// <summary>
        /// Runs a function against the raw ESAPI VMS.TPS.Common.Model.API.PatientSummary object safely on the ESAPI thread.
        /// </summary>
        Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.PatientSummary, T> func);
    }
}
