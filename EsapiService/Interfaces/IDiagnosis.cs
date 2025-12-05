namespace VMS.TPS.Common.Model.API
{
    public interface IDiagnosis : IApiDataObject
    {
        void WriteXml(System.Xml.XmlWriter writer);
        string ClinicalDescription { get; }
        string Code { get; }
        string CodeTable { get; }
    }
}
