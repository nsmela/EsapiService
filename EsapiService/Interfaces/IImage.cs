namespace VMS.TPS.Common.Model.API
{
    public interface IImage : IApiDataObject
    {
        void WriteXml(System.Xml.XmlWriter writer);
        void CalculateDectProtonStoppingPowers(VMS.TPS.Common.Model.API.Image rhoImage, VMS.TPS.Common.Model.API.Image zImage, int planeIndex, double[,] preallocatedBuffer);
        IStructureSet CreateNewStructureSet();
        VMS.TPS.Common.Model.Types.VVector DicomToUser(VMS.TPS.Common.Model.Types.VVector dicom, VMS.TPS.Common.Model.API.PlanSetup planSetup);
        VMS.TPS.Common.Model.Types.ImageProfile GetImageProfile(VMS.TPS.Common.Model.Types.VVector start, VMS.TPS.Common.Model.Types.VVector stop, double[] preallocatedBuffer);
        bool GetProtonStoppingPowerCurve(System.Collections.Generic.SortedList<double, double> protonStoppingPowerCurve);
        void GetVoxels(int planeIndex, int[,] preallocatedBuffer);
        VMS.TPS.Common.Model.Types.VVector UserToDicom(VMS.TPS.Common.Model.Types.VVector user, VMS.TPS.Common.Model.API.PlanSetup planSetup);
        double VoxelToDisplayValue(int voxelValue);
        string Id { get; }
        System.Threading.Tasks.Task SetIdAsync(string value);
        System.Collections.Generic.IReadOnlyList<VMS.TPS.Common.Model.Types.ImageApprovalHistoryEntry> ApprovalHistory { get; }
        System.Collections.Generic.IReadOnlyList<System.DateTime> CalibrationProtocolDateTime { get; }
        string CalibrationProtocolDescription { get; }
        string CalibrationProtocolId { get; }
        string CalibrationProtocolImageMatchWarning { get; }
        System.Collections.Generic.IReadOnlyList<System.DateTime> CalibrationProtocolLastModifiedDateTime { get; }
        VMS.TPS.Common.Model.CalibrationProtocolStatus CalibrationProtocolStatus { get; }
        VMS.TPS.Common.Model.UserInfo CalibrationProtocolUser { get; }
        string ContrastBolusAgentIngredientName { get; }
        System.Collections.Generic.IReadOnlyList<System.DateTime> CreationDateTime { get; }
        string DisplayUnit { get; }
        string FOR { get; }
        bool HasUserOrigin { get; }
        string ImageType { get; }
        string ImagingDeviceId { get; }
        VMS.TPS.Common.Model.Types.PatientOrientation ImagingOrientation { get; }
        string ImagingOrientationAsString { get; }
        bool IsProcessed { get; }
        int Level { get; }
        VMS.TPS.Common.Model.Types.SeriesModality Modality { get; }
        VMS.TPS.Common.Model.Types.VVector Origin { get; }
        ISeries Series { get; }
        string UID { get; }
        VMS.TPS.Common.Model.Types.VVector UserOrigin { get; }
        System.Threading.Tasks.Task SetUserOriginAsync(VMS.TPS.Common.Model.Types.VVector value);
        string UserOriginComments { get; }
        int Window { get; }
        VMS.TPS.Common.Model.Types.VVector XDirection { get; }
        double XRes { get; }
        int XSize { get; }
        VMS.TPS.Common.Model.Types.VVector YDirection { get; }
        double YRes { get; }
        int YSize { get; }
        VMS.TPS.Common.Model.Types.VVector ZDirection { get; }
        double ZRes { get; }
        int ZSize { get; }
    }
}
