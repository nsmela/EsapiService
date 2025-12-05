namespace VMS.TPS.Common.Model.API
{
    public interface IRTPrescriptionTargetConstraints : ISerializableObject
    {
        void WriteXml(System.Xml.XmlWriter writer);
        System.Collections.Generic.IReadOnlyList<IRTPrescriptionConstraint> Constraints { get; }
        string TargetId { get; }
    }
}
