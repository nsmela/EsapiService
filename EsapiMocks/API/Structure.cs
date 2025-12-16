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

        public void AddContourOnImagePlane(VVector[] contour, int z) { }
        public SegmentVolume And(SegmentVolume other) => default;
        public SegmentVolume AsymmetricMargin(AxisAlignedMargins margins) => default;
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
        public void ConvertDoseLevelToStructure(Dose dose, DoseValue doseLevel) { }
        public void ConvertToHighResolution() { }
        public bool GetAssignedHU(out double huValue)
        {
            huValue = default;
            return default;
        }

        public VVector[][] GetContoursOnImagePlane(int z) => default;
        public int GetNumberOfSeparateParts() => default;
        public VVector[] GetReferenceLinePoints() => default;
        public SegmentProfile GetSegmentProfile(VVector start, VVector stop, System.Collections.BitArray preallocatedBuffer) => default;
        public bool IsPointInsideSegment(VVector point) => default;
        public SegmentVolume Margin(double marginInMM) => default;
        public SegmentVolume Not() => default;
        public SegmentVolume Or(SegmentVolume other) => default;
        public bool ResetAssignedHU() => default;
        public void SetAssignedHU(double huValue) { }
        public SegmentVolume Sub(SegmentVolume other) => default;
        public void SubtractContourOnImagePlane(VVector[] contour, int z) { }
        public SegmentVolume Xor(SegmentVolume other) => default;
        public VVector CenterPoint { get; set; }
        public System.Windows.Media.Color Color { get; set; }
        public string DicomType { get; set; }
        public bool HasCalculatedPlans { get; set; }
        public bool HasSegment { get; set; }
        public bool IsApproved { get; set; }
        public bool IsEmpty { get; set; }
        public bool IsHighResolution { get; set; }
        public bool IsTarget { get; set; }
        public MeshGeometry3D MeshGeometry { get; set; }
        public int ROINumber { get; set; }
        public SegmentVolume SegmentVolume { get; set; }
        public StructureCode StructureCode { get; set; }
        public double Volume { get; set; }
    }
}
