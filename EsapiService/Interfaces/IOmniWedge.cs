namespace VMS.TPS.Common.Model.API
{
    public interface IOmniWedge : IWedge
    {
        void WriteXml(System.Xml.XmlWriter writer);
    }
}
