using System;
using System.Collections.Generic;
using System.Linq;
using VMS.TPS.Common.Model.Types;

namespace VMS.TPS.Common.Model.API
{
    public class BrachyTreatmentUnit : ApiDataObject
    {
        public BrachyTreatmentUnit()
        {
        }

        public RadioactiveSource GetActiveRadioactiveSource() => default;
        public string DoseRateMode { get; set; }
        public double DwellTimeResolution { get; set; }
        public string MachineInterface { get; set; }
        public string MachineModel { get; set; }
        public double MaxDwellTimePerChannel { get; set; }
        public double MaxDwellTimePerPos { get; set; }
        public double MaxDwellTimePerTreatment { get; set; }
        public double MaximumChannelLength { get; set; }
        public int MaximumDwellPositionsPerChannel { get; set; }
        public double MaximumStepSize { get; set; }
        public double MinAllowedSourcePos { get; set; }
        public double MinimumChannelLength { get; set; }
        public double MinimumStepSize { get; set; }
        public int NumberOfChannels { get; set; }
        public double SourceCenterOffsetFromTip { get; set; }
        public string SourceMovementType { get; set; }
        public double StepSizeResolution { get; set; }
    }
}
