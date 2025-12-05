namespace VMS.TPS.Common.Model.API
{
    public interface ISlot : IApiDataObject
    {
        void WriteXml(System.Xml.XmlWriter writer);
        int Number { get; }
    }
}
