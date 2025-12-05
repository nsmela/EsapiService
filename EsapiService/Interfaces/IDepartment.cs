namespace VMS.TPS.Common.Model.API
{
    public interface IDepartment : IApiDataObject
    {
        void WriteXml(System.Xml.XmlWriter writer);
        string GetFullName();
    }
}
