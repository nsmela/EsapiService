using System;
using System.Collections.Generic;
using System.Linq;
using VMS.TPS.Common.Model.Types;

namespace VMS.TPS.Common.Model.API
{
    public class Dose : ApiDataObject
    {
        public Dose()
        {
            Isodoses = new List<Isodose>();
        }

        public DoseProfile GetDoseProfile(VVector start, VVector stop, double[] preallocatedBuffer) => default;
        public DoseValue GetDoseToPoint(VVector at) => default;
        public void GetVoxels(int planeIndex, int[,] preallocatedBuffer) { }
        public DoseValue VoxelToDoseValue(int voxelValue) => default;
        public DoseValue DoseMax3D { get; set; }
        public VVector DoseMax3DLocation { get; set; }
        public IEnumerable<Isodose> Isodoses { get; set; }
        public VVector Origin { get; set; }
        public Series Series { get; set; }
        public string SeriesUID { get; set; }
        public string UID { get; set; }
        public VVector XDirection { get; set; }
        public double XRes { get; set; }
        public int XSize { get; set; }
        public VVector YDirection { get; set; }
        public double YRes { get; set; }
        public int YSize { get; set; }
        public VVector ZDirection { get; set; }
        public double ZRes { get; set; }
        public int ZSize { get; set; }
    }
}
