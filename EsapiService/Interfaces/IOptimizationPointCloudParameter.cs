namespace VMS.TPS.Common.Model.API
{
    public interface IOptimizationPointCloudParameter : IOptimizationParameter
    {
        void WriteXml(System.Xml.XmlWriter writer);
        double PointResolutionInMM { get; }
        IStructure Structure { get; }
    }
}
