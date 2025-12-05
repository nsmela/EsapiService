namespace VMS.TPS.Common.Model.API
{
    public interface IDose : IApiDataObject
    {
        void WriteXml(System.Xml.XmlWriter writer);
        VMS.TPS.Common.Model.Types.DoseProfile GetDoseProfile(VMS.TPS.Common.Model.Types.VVector start, VMS.TPS.Common.Model.Types.VVector stop, double[] preallocatedBuffer);
        VMS.TPS.Common.Model.Types.DoseValue GetDoseToPoint(VMS.TPS.Common.Model.Types.VVector at);
        void GetVoxels(int planeIndex, int[,] preallocatedBuffer);
        VMS.TPS.Common.Model.Types.DoseValue VoxelToDoseValue(int voxelValue);
        VMS.TPS.Common.Model.Types.DoseValue DoseMax3D { get; }
        VMS.TPS.Common.Model.Types.VVector DoseMax3DLocation { get; }
        System.Collections.Generic.IReadOnlyList<IIsodose> Isodoses { get; }
        VMS.TPS.Common.Model.Types.VVector Origin { get; }
        ISeries Series { get; }
        string SeriesUID { get; }
        string UID { get; }
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
