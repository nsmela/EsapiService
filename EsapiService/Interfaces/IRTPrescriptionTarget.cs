namespace VMS.TPS.Common.Model.API
{
    public interface IRTPrescriptionTarget : IApiDataObject
    {
        void WriteXml(System.Xml.XmlWriter writer);
        System.Collections.Generic.IReadOnlyList<IRTPrescriptionConstraint> Constraints { get; }
        VMS.TPS.Common.Model.Types.DoseValue DosePerFraction { get; }
        int NumberOfFractions { get; }
        string TargetId { get; }
        VMS.TPS.Common.Model.Types.RTPrescriptionTargetType Type { get; }
        double Value { get; }
    }
}
