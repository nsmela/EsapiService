namespace VMS.TPS.Common.Model.API
{
    public interface IEvaluationDose : IDose
    {
        void WriteXml(System.Xml.XmlWriter writer);
        int DoseValueToVoxel(VMS.TPS.Common.Model.Types.DoseValue doseValue);
        void SetVoxels(int planeIndex, int[,] values);
    }
}
