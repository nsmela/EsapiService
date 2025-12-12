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
        }

        public void GetVoxels(int planeIndex, int[,] preallocatedBuffer) { }
        public IEnumerable<Isodose> Isodoses { get; set; }
        public Series Series { get; set; }
        public string SeriesUID { get; set; }
        public string UID { get; set; }
        public double XRes { get; set; }
        public int XSize { get; set; }
        public double YRes { get; set; }
        public int YSize { get; set; }
        public double ZRes { get; set; }
        public int ZSize { get; set; }
    }
}
