namespace VMS.TPS.Common.Model.API
{
    public interface IPatientSummary : ISerializableObject
    {
        void WriteXml(System.Xml.XmlWriter writer);
        System.Collections.Generic.IReadOnlyList<System.DateTime> CreationDateTime { get; }
        System.Collections.Generic.IReadOnlyList<System.DateTime> DateOfBirth { get; }
        string FirstName { get; }
        string Id { get; }
        string Id2 { get; }
        string LastName { get; }
        string MiddleName { get; }
        string Sex { get; }
        string SSN { get; }
    }
}
