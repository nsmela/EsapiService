using System;
using System.Collections.Generic;
using System.Linq;
using VMS.TPS.Common.Model.Types;

namespace VMS.TPS.Common.Model.API
{
    public class ControlPointParameters
    {
        public ControlPointParameters()
        {
            JawPositions = new List<double>();
        }

        public double CollimatorAngle { get; set; }
        public int Index { get; set; }
        public VRect<double> JawPositions { get; set; }
        public float[,] LeafPositions { get; set; }
        public double PatientSupportAngle { get; set; }
        public double TableTopLateralPosition { get; set; }
        public double TableTopLongitudinalPosition { get; set; }
        public double TableTopVerticalPosition { get; set; }
        public double GantryAngle { get; set; }
        public double MetersetWeight { get; set; }
    }
}
