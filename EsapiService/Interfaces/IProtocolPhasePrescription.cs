namespace VMS.TPS.Common.Model.API
{
    public interface IProtocolPhasePrescription : ISerializableObject
    {
        void WriteXml(System.Xml.XmlWriter writer);
        VMS.TPS.Common.Model.Types.DoseValue TargetTotalDose { get; }
        VMS.TPS.Common.Model.Types.DoseValue TargetFractionDose { get; }
        VMS.TPS.Common.Model.Types.DoseValue ActualTotalDose { get; }
        System.Collections.Generic.IReadOnlyList<bool> TargetIsMet { get; }
        VMS.TPS.Common.Model.Types.PrescriptionModifier PrescModifier { get; }
        double PrescParameter { get; }
        VMS.TPS.Common.Model.Types.PrescriptionType PrescType { get; }
        string StructureId { get; }
    }
}
