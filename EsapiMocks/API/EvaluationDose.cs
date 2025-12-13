using System;
using System.Collections.Generic;
using System.Linq;
using VMS.TPS.Common.Model.Types;

namespace VMS.TPS.Common.Model.API
{
    public class EvaluationDose : Dose
    {
        public EvaluationDose()
        {
        }

        public int DoseValueToVoxel(DoseValue doseValue) => default;
        public void SetVoxels(int planeIndex, int[,] values) { }
    }
}
