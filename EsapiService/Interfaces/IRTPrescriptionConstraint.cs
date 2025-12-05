namespace VMS.TPS.Common.Model.API
{
    public interface IRTPrescriptionConstraint : ISerializableObject
    {
        void WriteXml(System.Xml.XmlWriter writer);
        VMS.TPS.Common.Model.Types.RTPrescriptionConstraintType ConstraintType { get; }
        string Unit1 { get; }
        string Unit2 { get; }
        string Value1 { get; }
        string Value2 { get; }
    }
}
