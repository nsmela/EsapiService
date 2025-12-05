namespace VMS.TPS.Common.Model.API
{
    public interface IFieldReferencePoint : IApiDataObject
    {
        void WriteXml(System.Xml.XmlWriter writer);
        double EffectiveDepth { get; }
        VMS.TPS.Common.Model.Types.DoseValue FieldDose { get; }
        bool IsFieldDoseNominal { get; }
        bool IsPrimaryReferencePoint { get; }
        IReferencePoint ReferencePoint { get; }
        VMS.TPS.Common.Model.Types.VVector RefPointLocation { get; }
        double SSD { get; }
    }
}
