namespace VMS.TPS.Common.Model.API
{
    public interface ITreatmentUnitOperatingLimits : ISerializableObject
    {
        void WriteXml(System.Xml.XmlWriter writer);
        ITreatmentUnitOperatingLimit CollimatorAngle { get; }
        ITreatmentUnitOperatingLimit GantryAngle { get; }
        ITreatmentUnitOperatingLimit MU { get; }
        ITreatmentUnitOperatingLimit PatientSupportAngle { get; }
    }
}
