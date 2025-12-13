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
    public interface IRTPrescription : IApiDataObject
    {
        // --- Simple Properties --- //
        string BolusFrequency { get; }
        string BolusThickness { get; }
        IEnumerable<string> Energies { get; }
        IEnumerable<string> EnergyModes { get; }
        string Gating { get; }
        string Notes { get; }
        int? NumberOfFractions { get; }
        IEnumerable<RTPrescriptionOrganAtRisk> OrgansAtRisk { get; }
        string PhaseType { get; }
        int RevisionNumber { get; }
        bool? SimulationNeeded { get; }
        string Site { get; }
        string Status { get; }
        IEnumerable<RTPrescriptionTargetConstraints> TargetConstraintsWithoutTargetLevel { get; }
        IEnumerable<RTPrescriptionTarget> Targets { get; }
        string Technique { get; }

        // --- Accessors --- //
        Task<IRTPrescription> GetLatestRevisionAsync(); // read complex property
        Task<IRTPrescription> GetPredecessorPrescriptionAsync(); // read complex property

        // --- RunAsync --- //
        /// <summary>
        /// Runs a function against the raw ESAPI VMS.TPS.Common.Model.API.RTPrescription object safely on the ESAPI thread.
        /// </summary>
        Task RunAsync(Action<VMS.TPS.Common.Model.API.RTPrescription> action);

        /// <summary>
        /// Runs a function against the raw ESAPI VMS.TPS.Common.Model.API.RTPrescription object safely on the ESAPI thread.
        /// </summary>
        Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.RTPrescription, T> func);
    }
}
