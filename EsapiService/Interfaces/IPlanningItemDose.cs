namespace VMS.TPS.Common.Model.API
{
    public interface IPlanningItemDose : IDose
    {
        void WriteXml(System.Xml.XmlWriter writer);
    }
}
