namespace VMS.TPS.Common.Model.API
{
    public interface IDynamicWedge : IWedge
    {
        void WriteXml(System.Xml.XmlWriter writer);
    }
}
