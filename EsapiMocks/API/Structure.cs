using System;
using System.Collections.Generic;
using System.Linq;
using VMS.TPS.Common.Model.Types;

namespace VMS.TPS.Common.Model.API
{
    public class Structure : ApiDataObject
    {
        public Structure()
        {
        }

        public SegmentVolume And(SegmentVolume other) => default;
        public bool CanConvertToHighResolution() => default;
        public bool CanEditSegmentVolume(out string errorMessage)
        {
            errorMessage = default;
            return default;
        }

        public bool CanSetAssignedHU(out string errorMessage)
        {
            errorMessage = default;
            return default;
        }

        public void ClearAllContoursOnImagePlane(int z) { }
        public void ConvertToHighResolution() { }
        public bool GetAssignedHU(out double huValue)
        {
            huValue = default;
            return default;
        }

        public int GetNumberOfSeparateParts() => default;
        public SegmentVolume Margin(double marginInMM) => default;
        public SegmentVolume Not() => default;
        public SegmentVolume Or(SegmentVolume other) => default;
        public bool ResetAssignedHU() => default;
        public void SetAssignedHU(double huValue) { }
        public SegmentVolume Sub(SegmentVolume other) => default;
        public SegmentVolume Xor(SegmentVolume other) => default;
        public System.Windows.Media.Color Color { get; set; }
        public string DicomType { get; set; }
        public bool HasCalculatedPlans { get; set; }
        public bool HasSegment { get; set; }
        public bool IsApproved { get; set; }
        public bool IsEmpty { get; set; }
        public bool IsHighResolution { get; set; }
        public bool IsTarget { get; set; }
        public System.Windows.Media.Media3D.MeshGeometry3D MeshGeometry { get; set; }
        public int ROINumber { get; set; }
        public SegmentVolume SegmentVolume { get; set; }
        public StructureCode StructureCode { get; set; }
        public double Volume { get; set; }
    }
}
