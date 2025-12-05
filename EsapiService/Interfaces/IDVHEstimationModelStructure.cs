namespace VMS.TPS.Common.Model.API
{
    public interface IDVHEstimationModelStructure : ISerializableObject
    {
        void WriteXml(System.Xml.XmlWriter writer);
        string Id { get; }
        bool IsValid { get; }
        System.Guid ModelStructureGuid { get; }
        System.Collections.Generic.IReadOnlyList<IStructureCode> StructureCodes { get; }
        VMS.TPS.Common.Model.Types.DVHEstimationStructureType StructureType { get; }
    }
}
