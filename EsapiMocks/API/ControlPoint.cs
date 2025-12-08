using System;
using System.Collections.Generic;
using System.Linq;
using VMS.TPS.Common.Model.Types;

namespace VMS.TPS.Common.Model.API
{
    public class ControlPoint : SerializableObject
    {
        public ControlPoint()
        {
            JawPositions = new List<double>();
        }

        public Beam Beam { get; set; }
        public double CollimatorAngle { get; set; }
        public double GantryAngle { get; set; }
        public int Index { get; set; }
        public VRect<double> JawPositions { get; set; }
        public float[,] LeafPositions { get; set; }
        public double MetersetWeight { get; set; }
        public double PatientSupportAngle { get; set; }
        public double TableTopLateralPosition { get; set; }
        public double TableTopLongitudinalPosition { get; set; }
        public double TableTopVerticalPosition { get; set; }
    }
}
