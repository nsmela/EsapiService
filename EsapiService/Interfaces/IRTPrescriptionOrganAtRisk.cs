namespace VMS.TPS.Common.Model.API
{
    public interface IRTPrescriptionOrganAtRisk : ISerializableObject
    {
        void WriteXml(System.Xml.XmlWriter writer);
        System.Collections.Generic.IReadOnlyList<IRTPrescriptionConstraint> Constraints { get; }
        string OrganAtRiskId { get; }
    }
}
