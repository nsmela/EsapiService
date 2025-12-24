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
        DateTime? CreationDateTime { get; } // simple property
        DateTime? DateOfBirth { get; } // simple property
        string FirstName { get; } // simple property
        string Id { get; } // simple property
        string Id2 { get; } // simple property
        string LastName { get; } // simple property
        string MiddleName { get; } // simple property
        string Sex { get; } // simple property
        string SSN { get; } // simple property

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
