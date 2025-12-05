namespace VMS.TPS.Common.Model.API
{
    public interface IPlanSumComponent : IApiDataObject
    {
        void WriteXml(System.Xml.XmlWriter writer);
        string PlanSetupId { get; }
        VMS.TPS.Common.Model.Types.PlanSumOperation PlanSumOperation { get; }
        double PlanWeight { get; }
    }
}
