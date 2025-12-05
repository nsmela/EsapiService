namespace VMS.TPS.Common.Model.API
{
    public interface IBeamDose : IDose
    {
        void WriteXml(System.Xml.XmlWriter writer);
        VMS.TPS.Common.Model.Types.DoseValue GetAbsoluteBeamDoseValue(VMS.TPS.Common.Model.Types.DoseValue relative);
    }
}
