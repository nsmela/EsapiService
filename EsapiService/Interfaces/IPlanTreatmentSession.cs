namespace VMS.TPS.Common.Model.API
{
    public interface IPlanTreatmentSession : IApiDataObject
    {
        void WriteXml(System.Xml.XmlWriter writer);
        IPlanSetup PlanSetup { get; }
        VMS.TPS.Common.Model.Types.TreatmentSessionStatus Status { get; }
        ITreatmentSession TreatmentSession { get; }
    }
}
