namespace VMS.TPS.Common.Model.API
{
    public interface IBlock : IApiDataObject
    {
        void WriteXml(System.Xml.XmlWriter writer);
        IAddOnMaterial AddOnMaterial { get; }
        bool IsDiverging { get; }
        System.Windows.Point[][] Outline { get; }
        System.Threading.Tasks.Task SetOutlineAsync(System.Windows.Point[][] value);
        double TransmissionFactor { get; }
        ITray Tray { get; }
        double TrayTransmissionFactor { get; }
        VMS.TPS.Common.Model.Types.BlockType Type { get; }
    }
}
