namespace VMS.TPS.Common.Model.API
{
    public interface ITreatmentPhase : IApiDataObject
    {
        void WriteXml(System.Xml.XmlWriter writer);
        string OtherInfo { get; }
        int PhaseGapNumberOfDays { get; }
        System.Collections.Generic.IReadOnlyList<IRTPrescription> Prescriptions { get; }
        string TimeGapType { get; }
    }
}
