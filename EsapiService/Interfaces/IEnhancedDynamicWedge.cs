namespace VMS.TPS.Common.Model.API
{
    public interface IEnhancedDynamicWedge : IDynamicWedge
    {
        void WriteXml(System.Xml.XmlWriter writer);
    }
}
