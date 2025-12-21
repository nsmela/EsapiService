using System;
using System.Collections.Generic;
using System.Linq;
using VMS.TPS.Common.Model.Types;

namespace VMS.TPS.Common.Model.API
{
    public class ScriptContext
    {
        public ScriptContext()
        {
            PlansInScope = new List<PlanSetup>();
            ExternalPlansInScope = new List<ExternalPlanSetup>();
            BrachyPlansInScope = new List<BrachyPlanSetup>();
            IonPlansInScope = new List<IonPlanSetup>();
            PlanSumsInScope = new List<PlanSum>();
        }

        public User CurrentUser { get; set; }
        public Course Course { get; set; }
        public Image Image { get; set; }
        public StructureSet StructureSet { get; set; }
        public Calculation Calculation { get; set; }
        public ActiveStructureCodeDictionaries StructureCodes { get; set; }
        public Equipment Equipment { get; set; }
        public Patient Patient { get; set; }
        public PlanSetup PlanSetup { get; set; }
        public ExternalPlanSetup ExternalPlanSetup { get; set; }
        public BrachyPlanSetup BrachyPlanSetup { get; set; }
        public IonPlanSetup IonPlanSetup { get; set; }
        public IEnumerable<PlanSetup> PlansInScope { get; set; }
        public IEnumerable<ExternalPlanSetup> ExternalPlansInScope { get; set; }
        public IEnumerable<BrachyPlanSetup> BrachyPlansInScope { get; set; }
        public IEnumerable<IonPlanSetup> IonPlansInScope { get; set; }
        public IEnumerable<PlanSum> PlanSumsInScope { get; set; }
        public PlanSum PlanSum { get; set; }
        public string ApplicationName { get; set; }
        public string VersionInfo { get; set; }

        /* --- Skipped Members (Not generated) ---
           - .ctor: Explicitly ignored by name
        */
    }
}
