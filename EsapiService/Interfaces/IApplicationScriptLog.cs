namespace VMS.TPS.Common.Model.API
{
    public interface IApplicationScriptLog : IApiDataObject
    {
        void WriteXml(System.Xml.XmlWriter writer);
        string CourseId { get; }
        string PatientId { get; }
        string PlanSetupId { get; }
        string PlanUID { get; }
        IApplicationScript Script { get; }
        string ScriptFullName { get; }
        string StructureSetId { get; }
        string StructureSetUID { get; }
    }
}
