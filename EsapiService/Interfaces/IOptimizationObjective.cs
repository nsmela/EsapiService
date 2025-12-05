namespace VMS.TPS.Common.Model.API
{
    public interface IOptimizationObjective : ISerializableObject
    {
        void WriteXml(System.Xml.XmlWriter writer);
        bool Equals(object obj);
        int GetHashCode();
        VMS.TPS.Common.Model.Types.OptimizationObjectiveOperator Operator { get; }
        double Priority { get; }
        IStructure Structure { get; }
        string StructureId { get; }
    }
}
