namespace VMS.TPS.Common.Model.API
{
    public interface IBeamUncertainty : IApiDataObject
    {
        void WriteXml(System.Xml.XmlWriter writer);
        IBeam Beam { get; }
        VMS.TPS.Common.Model.Types.BeamNumber BeamNumber { get; }
        IDose Dose { get; }
    }
}
