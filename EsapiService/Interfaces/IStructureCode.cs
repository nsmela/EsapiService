namespace VMS.TPS.Common.Model.API
{
    public interface IStructureCode : ISerializableObject
    {
        VMS.TPS.Common.Model.Types.StructureCodeInfo ToStructureCodeInfo();
        bool Equals(VMS.TPS.Common.Model.API.StructureCode other);
        bool Equals(object obj);
        string ToString();
        int GetHashCode();
        void WriteXml(System.Xml.XmlWriter writer);
        string Code { get; }
        string CodeMeaning { get; }
        string CodingScheme { get; }
        string DisplayName { get; }
        bool IsEncompassStructureCode { get; }
    }
}
