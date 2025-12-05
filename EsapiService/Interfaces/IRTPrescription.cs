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
        Task WriteXmlAsync(System.Xml.XmlWriter writer);
        string BolusFrequency { get; }
        string BolusThickness { get; }
        System.Collections.Generic.IReadOnlyList<string> Energies { get; }
        System.Collections.Generic.IReadOnlyList<string> EnergyModes { get; }
        string Gating { get; }
        Task<IRTPrescription> GetLatestRevisionAsync();
        string Notes { get; }
        System.Collections.Generic.IReadOnlyList<int> NumberOfFractions { get; }
        System.Collections.Generic.IReadOnlyList<IRTPrescriptionOrganAtRisk> OrgansAtRisk { get; }
        string PhaseType { get; }
        Task<IRTPrescription> GetPredecessorPrescriptionAsync();
        int RevisionNumber { get; }
        System.Collections.Generic.IReadOnlyList<bool> SimulationNeeded { get; }
        string Site { get; }
        string Status { get; }
        System.Collections.Generic.IReadOnlyList<IRTPrescriptionTargetConstraints> TargetConstraintsWithoutTargetLevel { get; }
        System.Collections.Generic.IReadOnlyList<IRTPrescriptionTarget> Targets { get; }
        string Technique { get; }

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
