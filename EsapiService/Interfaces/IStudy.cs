namespace VMS.TPS.Common.Model.API
{
    public interface IStudy : IApiDataObject
    {
        void WriteXml(System.Xml.XmlWriter writer);
        System.Collections.Generic.IReadOnlyList<System.DateTime> CreationDateTime { get; }
        System.Collections.Generic.IReadOnlyList<IImage> Images3D { get; }
        System.Collections.Generic.IReadOnlyList<ISeries> Series { get; }
        string UID { get; }
    }
}
