namespace VMS.TPS.Common.Model.API
{
    public interface ITechnique : IApiDataObject
    {
        void WriteXml(System.Xml.XmlWriter writer);
        bool IsArc { get; }
        bool IsModulatedScanning { get; }
        bool IsProton { get; }
        bool IsScanning { get; }
        bool IsStatic { get; }
    }
}
