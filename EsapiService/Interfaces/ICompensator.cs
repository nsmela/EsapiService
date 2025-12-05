namespace VMS.TPS.Common.Model.API
{
    public interface ICompensator : IApiDataObject
    {
        void WriteXml(System.Xml.XmlWriter writer);
        IAddOnMaterial Material { get; }
        ISlot Slot { get; }
        ITray Tray { get; }
    }
}
