using System;
using System.Collections.Generic;
using System.Linq;
using VMS.TPS.Common.Model.Types;

namespace VMS.TPS.Common.Model.API
{
    public class BeamParameters
    {
        public BeamParameters()
        {
        }

        public void SetAllLeafPositions(float[,] leafPositions) { }
        public void SetJawPositions(VRect<double> positions) { }
        public IEnumerable<ControlPointParameters> ControlPoints { get; set; }
        public double WeightFactor { get; set; }
    }
}
