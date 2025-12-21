using System;
using System.Collections.Generic;
using System.Linq;
using VMS.TPS.Common.Model.Types;

namespace VMS.TPS.Common.Model.API
{
    public class RTPrescription : ApiDataObject
    {
        public RTPrescription()
        {
            OrgansAtRisk = new List<RTPrescriptionOrganAtRisk>();
            TargetConstraintsWithoutTargetLevel = new List<RTPrescriptionTargetConstraints>();
            Targets = new List<RTPrescriptionTarget>();
        }

        public string BolusFrequency { get; set; }
        public string BolusThickness { get; set; }
        public string Gating { get; set; }
        public RTPrescription LatestRevision { get; set; }
        public string Notes { get; set; }
        public int? NumberOfFractions { get; set; }
        public IEnumerable<RTPrescriptionOrganAtRisk> OrgansAtRisk { get; set; }
        public string PhaseType { get; set; }
        public RTPrescription PredecessorPrescription { get; set; }
        public int RevisionNumber { get; set; }
        public bool? SimulationNeeded { get; set; }
        public string Site { get; set; }
        public string Status { get; set; }
        public IEnumerable<RTPrescriptionTargetConstraints> TargetConstraintsWithoutTargetLevel { get; set; }
        public IEnumerable<RTPrescriptionTarget> Targets { get; set; }
        public string Technique { get; set; }

        /* --- Skipped Members (Not generated) ---
           - Energies: No matching factory found (Not Implemented)
           - EnergyModes: No matching factory found (Not Implemented)
        */
    }
}
