namespace VMS.TPS.Common.Model.API
{
    public interface IApplicationScript : IApiDataObject
    {
        void WriteXml(System.Xml.XmlWriter writer);
        VMS.TPS.Common.Model.Types.ApplicationScriptApprovalStatus ApprovalStatus { get; }
        string ApprovalStatusDisplayText { get; }
        System.Reflection.AssemblyName AssemblyName { get; }
        System.Collections.Generic.IReadOnlyList<System.DateTime> ExpirationDate { get; }
        bool IsReadOnlyScript { get; }
        bool IsWriteableScript { get; }
        string PublisherName { get; }
        VMS.TPS.Common.Model.Types.ApplicationScriptType ScriptType { get; }
        System.Collections.Generic.IReadOnlyList<System.DateTime> StatusDate { get; }
        VMS.TPS.Common.Model.Types.UserIdentity StatusUserIdentity { get; }
    }
}
