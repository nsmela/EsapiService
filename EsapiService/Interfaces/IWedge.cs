namespace VMS.TPS.Common.Model.API
{
    public interface IWedge : IAddOn
    {
        void WriteXml(System.Xml.XmlWriter writer);
        double Direction { get; }
        double WedgeAngle { get; }
    }
}
