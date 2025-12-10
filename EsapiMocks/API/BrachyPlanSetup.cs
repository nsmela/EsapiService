using System;
using System.Collections.Generic;
using System.Linq;
using VMS.TPS.Common.Model.Types;

namespace VMS.TPS.Common.Model.API
{
    public class BrachyPlanSetup : PlanSetup
    {
        public BrachyPlanSetup()
        {
            Catheters = new List<Catheter>();
            ReferenceLines = new List<Structure>();
            SeedCollections = new List<SeedCollection>();
            SolidApplicators = new List<BrachySolidApplicator>();
        }

        public Catheter AddCatheter(string catheterId, BrachyTreatmentUnit treatmentUnit, System.Text.StringBuilder outputDiagnostics, bool appendChannelNumToId, int channelNum) => default;
        public ReferencePoint AddReferencePoint(bool target, string id) => default;
        public CalculateBrachy3DDoseResult CalculateTG43Dose() => default;
        public string ApplicationSetupType { get; set; }
        public IEnumerable<Catheter> Catheters { get; set; }
        public int? NumberOfPdrPulses { get; set; }
        public double? PdrPulseInterval { get; set; }
        public IEnumerable<Structure> ReferenceLines { get; set; }
        public IEnumerable<SeedCollection> SeedCollections { get; set; }
        public IEnumerable<BrachySolidApplicator> SolidApplicators { get; set; }
        public DateTime? TreatmentDateTime { get; set; }
    }
}
