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
    public partial interface IRTPrescription : IApiDataObject
    {
        // --- Simple Properties --- //
        string BolusFrequency { get; } // simple property
        string BolusThickness { get; } // simple property
        string Gating { get; } // simple property
        string Notes { get; } // simple property
        int? NumberOfFractions { get; } // simple property
        string PhaseType { get; } // simple property
        int RevisionNumber { get; } // simple property
        bool? SimulationNeeded { get; } // simple property
        string Site { get; } // simple property
        string Status { get; } // simple property
        string Technique { get; } // simple property

        // --- Accessors --- //
        Task<IRTPrescription> GetLatestRevisionAsync(); // read complex property
        Task<IRTPrescription> GetPredecessorPrescriptionAsync(); // read complex property

        // --- Collections --- //
        Task<IReadOnlyList<IRTPrescriptionOrganAtRisk>> GetOrgansAtRiskAsync(); // collection property context
        Task<IReadOnlyList<IRTPrescriptionTargetConstraints>> GetTargetConstraintsWithoutTargetLevelAsync(); // collection property context
        Task<IReadOnlyList<IRTPrescriptionTarget>> GetTargetsAsync(); // collection property context

        // --- RunAsync --- //
        /// <summary>
        /// Runs a function against the raw ESAPI VMS.TPS.Common.Model.API.RTPrescription object safely on the ESAPI thread.
        /// </summary>
        Task RunAsync(Action<VMS.TPS.Common.Model.API.RTPrescription> action);

        /// <summary>
        /// Runs a function against the raw ESAPI VMS.TPS.Common.Model.API.RTPrescription object safely on the ESAPI thread.
        /// </summary>
        Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.RTPrescription, T> func);

        // --- Validates --- //
        /// <summary>
        /// Verifies is the wrapped ESAPI object isn't null.
        /// </summary>
        new bool IsValid();

        /// <summary>
        /// Verifies is the wrapped ESAPI object is null.
        /// </summary>
        new bool IsNotValid();

        /* --- Skipped Members (Not generated) ---
           - Energies: No matching factory found (Not Implemented)
           - EnergyModes: No matching factory found (Not Implemented)
        */
    }
}
