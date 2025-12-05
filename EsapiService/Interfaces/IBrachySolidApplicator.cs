namespace VMS.TPS.Common.Model.API
{
    public interface IBrachySolidApplicator : IApiDataObject
    {
        void WriteXml(System.Xml.XmlWriter writer);
        string ApplicatorSetName { get; }
        string ApplicatorSetType { get; }
        string Category { get; }
        System.Collections.Generic.IReadOnlyList<ICatheter> Catheters { get; }
        int GroupNumber { get; }
        string Note { get; }
        string PartName { get; }
        string PartNumber { get; }
        string Summary { get; }
        string UID { get; }
        string Vendor { get; }
        string Version { get; }
    }
}
