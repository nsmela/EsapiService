using System;
using System.Collections.Generic;
using System.Linq;
using VMS.TPS.Common.Model.Types;

namespace VMS.TPS.Common.Model.API
{
    public class ApplicationScriptLog : ApiDataObject
    {
        public ApplicationScriptLog()
        {
        }

        public string CourseId { get; set; }
        public string PatientId { get; set; }
        public string PlanSetupId { get; set; }
        public string PlanUID { get; set; }
        public ApplicationScript Script { get; set; }
        public string ScriptFullName { get; set; }
        public string StructureSetId { get; set; }
        public string StructureSetUID { get; set; }
    }
}
