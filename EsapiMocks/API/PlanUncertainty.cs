using System;
using System.Collections.Generic;
using System.Linq;
using VMS.TPS.Common.Model.Types;

namespace VMS.TPS.Common.Model.API
{
    public class PlanUncertainty : ApiDataObject
    {
        public PlanUncertainty()
        {
            BeamUncertainties = new List<BeamUncertainty>();
        }

        public DVHData GetDVHCumulativeData(Structure structure, DoseValuePresentation dosePresentation, VolumePresentation volumePresentation, double binWidth) => default;
        public IEnumerable<BeamUncertainty> BeamUncertainties { get; set; }
        public double CalibrationCurveError { get; set; }
        public string DisplayName { get; set; }
        public Dose Dose { get; set; }
        public VVector IsocenterShift { get; set; }
        public PlanUncertaintyType UncertaintyType { get; set; }
    }
}
