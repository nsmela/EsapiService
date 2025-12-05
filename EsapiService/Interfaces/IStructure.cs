namespace VMS.TPS.Common.Model.API
{
    public interface IStructure : IApiDataObject
    {
        void WriteXml(System.Xml.XmlWriter writer);
        void AddContourOnImagePlane(VMS.TPS.Common.Model.Types.VVector[] contour, int z);
        ISegmentVolume And(VMS.TPS.Common.Model.API.SegmentVolume other);
        ISegmentVolume AsymmetricMargin(VMS.TPS.Common.Model.Types.AxisAlignedMargins margins);
        bool CanConvertToHighResolution();


        void ClearAllContoursOnImagePlane(int z);
        void ConvertDoseLevelToStructure(VMS.TPS.Common.Model.API.Dose dose, VMS.TPS.Common.Model.Types.DoseValue doseLevel);
        void ConvertToHighResolution();

        VMS.TPS.Common.Model.Types.VVector[][] GetContoursOnImagePlane(int z);
        int GetNumberOfSeparateParts();
        VMS.TPS.Common.Model.Types.VVector[] GetReferenceLinePoints();
        VMS.TPS.Common.Model.Types.SegmentProfile GetSegmentProfile(VMS.TPS.Common.Model.Types.VVector start, VMS.TPS.Common.Model.Types.VVector stop, System.Collections.BitArray preallocatedBuffer);
        bool IsPointInsideSegment(VMS.TPS.Common.Model.Types.VVector point);
        ISegmentVolume Margin(double marginInMM);
        ISegmentVolume Not();
        ISegmentVolume Or(VMS.TPS.Common.Model.API.SegmentVolume other);
        bool ResetAssignedHU();
        void SetAssignedHU(double huValue);
        ISegmentVolume Sub(VMS.TPS.Common.Model.API.SegmentVolume other);
        void SubtractContourOnImagePlane(VMS.TPS.Common.Model.Types.VVector[] contour, int z);
        ISegmentVolume Xor(VMS.TPS.Common.Model.API.SegmentVolume other);
        string Id { get; }
        System.Threading.Tasks.Task SetIdAsync(string value);
        string Name { get; }
        System.Threading.Tasks.Task SetNameAsync(string value);
        string Comment { get; }
        System.Threading.Tasks.Task SetCommentAsync(string value);
        System.Collections.Generic.IReadOnlyList<VMS.TPS.Common.Model.Types.StructureApprovalHistoryEntry> ApprovalHistory { get; }
        VMS.TPS.Common.Model.Types.VVector CenterPoint { get; }
        System.Windows.Media.Color Color { get; }
        System.Threading.Tasks.Task SetColorAsync(System.Windows.Media.Color value);
        string DicomType { get; }
        bool HasCalculatedPlans { get; }
        bool HasSegment { get; }
        bool IsApproved { get; }
        bool IsEmpty { get; }
        bool IsHighResolution { get; }
        bool IsTarget { get; }
        System.Windows.Media.Media3D.MeshGeometry3D MeshGeometry { get; }
        int ROINumber { get; }
        ISegmentVolume SegmentVolume { get; }
        System.Threading.Tasks.Task SetSegmentVolumeAsync(ISegmentVolume value);
        IStructureCode StructureCode { get; }
        System.Threading.Tasks.Task SetStructureCodeAsync(IStructureCode value);
        System.Collections.Generic.IReadOnlyList<VMS.TPS.Common.Model.Types.StructureCodeInfo> StructureCodeInfos { get; }
        double Volume { get; }
    }
}
