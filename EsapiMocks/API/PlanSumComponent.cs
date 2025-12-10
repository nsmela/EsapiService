using System;
using System.Collections.Generic;
using System.Linq;
using VMS.TPS.Common.Model.Types;

namespace VMS.TPS.Common.Model.API
{
    public class PlanSumComponent : ApiDataObject
    {
        public PlanSumComponent()
        {
        }

        public string PlanSetupId { get; set; }
        public double PlanWeight { get; set; }
    }
}
