namespace VMS.TPS.Common.Model.API
{
    public interface IApplicationPackage : IApiDataObject
    {
        void WriteXml(System.Xml.XmlWriter writer);
        VMS.TPS.Common.Model.Types.ApplicationScriptApprovalStatus ApprovalStatus { get; }
        string Description { get; }
        System.Collections.Generic.IReadOnlyList<System.DateTime> ExpirationDate { get; }
        string PackageId { get; }
        string PackageName { get; }
        string PackageVersion { get; }
        string PublisherData { get; }
        string PublisherName { get; }
    }
}
