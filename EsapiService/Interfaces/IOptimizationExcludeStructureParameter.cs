namespace VMS.TPS.Common.Model.API
{
    public interface IOptimizationExcludeStructureParameter : IOptimizationParameter
    {
        void WriteXml(System.Xml.XmlWriter writer);
        IStructure Structure { get; }
    }
}
