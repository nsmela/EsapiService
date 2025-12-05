namespace VMS.TPS.Common.Model.API
{
    public interface ITreatmentSession : IApiDataObject
    {
        void WriteXml(System.Xml.XmlWriter writer);
        long SessionNumber { get; }
        System.Collections.Generic.IReadOnlyList<IPlanTreatmentSession> SessionPlans { get; }
    }
}
