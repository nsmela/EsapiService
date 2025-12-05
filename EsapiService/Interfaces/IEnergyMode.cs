namespace VMS.TPS.Common.Model.API
{
    public interface IEnergyMode : IApiDataObject
    {
        void WriteXml(System.Xml.XmlWriter writer);
        bool IsElectron { get; }
        bool IsPhoton { get; }
        bool IsProton { get; }
    }
}
