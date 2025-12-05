namespace VMS.TPS.Common.Model.API
{
    public interface IExternalBeamTreatmentUnit : IApiDataObject
    {
        void WriteXml(System.Xml.XmlWriter writer);
        string MachineDepartmentName { get; }
        string MachineModel { get; }
        string MachineModelName { get; }
        string MachineScaleDisplayName { get; }
        ITreatmentUnitOperatingLimits OperatingLimits { get; }
        double SourceAxisDistance { get; }
    }
}
