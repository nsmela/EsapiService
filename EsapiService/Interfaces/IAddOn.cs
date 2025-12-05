namespace VMS.TPS.Common.Model.API
{
    public interface IAddOn : IApiDataObject
    {
        void WriteXml(System.Xml.XmlWriter writer);
        System.Collections.Generic.IReadOnlyList<System.DateTime> CreationDateTime { get; }
    }
}
