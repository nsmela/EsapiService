namespace VMS.TPS.Common.Model.API
{
    public interface IHospital : IApiDataObject
    {
        void WriteXml(System.Xml.XmlWriter writer);
        System.Collections.Generic.IReadOnlyList<System.DateTime> CreationDateTime { get; }
        System.Collections.Generic.IReadOnlyList<IDepartment> Departments { get; }
        string Location { get; }
    }
}
