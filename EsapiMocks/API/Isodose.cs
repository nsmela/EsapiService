using System;
using System.Collections.Generic;
using System.Linq;
using VMS.TPS.Common.Model.Types;

namespace VMS.TPS.Common.Model.API
{
    public class Isodose : SerializableObject
    {
        public Isodose()
        {
        }

        public System.Windows.Media.Color Color { get; set; }
        public DoseValue Level { get; set; }
        public System.Windows.Media.Media3D.MeshGeometry3D MeshGeometry { get; set; }
    }
}
