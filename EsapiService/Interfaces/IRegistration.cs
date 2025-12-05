namespace VMS.TPS.Common.Model.API
{
    public interface IRegistration : IApiDataObject
    {
        void WriteXml(System.Xml.XmlWriter writer);
        VMS.TPS.Common.Model.Types.VVector InverseTransformPoint(VMS.TPS.Common.Model.Types.VVector pt);
        VMS.TPS.Common.Model.Types.VVector TransformPoint(VMS.TPS.Common.Model.Types.VVector pt);
        System.Collections.Generic.IReadOnlyList<System.DateTime> CreationDateTime { get; }
        string RegisteredFOR { get; }
        string SourceFOR { get; }
        VMS.TPS.Common.Model.Types.RegistrationApprovalStatus Status { get; }
        System.Collections.Generic.IReadOnlyList<System.DateTime> StatusDateTime { get; }
        string StatusUserDisplayName { get; }
        string StatusUserName { get; }
        double[,] TransformationMatrix { get; }
        string UID { get; }
    }
}
