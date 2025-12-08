using System;
using System.Collections.Generic;
using System.Linq;
using VMS.TPS.Common.Model.Types;

namespace VMS.TPS.Common.Model.API
{
    public class Application : SerializableObject
    {
        public Application()
        {
            PatientSummaries = new List<PatientSummary>();
        }

        public void Dispose() { }
        public Patient OpenPatient(PatientSummary patientSummary) => default;
        public Patient OpenPatientById(string id) => default;
        public void ClosePatient() { }
        public void SaveModifications() { }
        public User CurrentUser { get; set; }
        public string SiteProgramDataDir { get; set; }
        public IEnumerable<PatientSummary> PatientSummaries { get; set; }
        public Calculation Calculation { get; set; }
        public ActiveStructureCodeDictionaries StructureCodes { get; set; }
        public Equipment Equipment { get; set; }
        public ScriptEnvironment ScriptEnvironment { get; set; }
    }
}
