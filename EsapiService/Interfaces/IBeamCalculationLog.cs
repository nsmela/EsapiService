namespace VMS.TPS.Common.Model.API
{
    public interface IBeamCalculationLog : ISerializableObject
    {
        void WriteXml(System.Xml.XmlWriter writer);
        IBeam Beam { get; }
        string Category { get; }
        System.Collections.Generic.IReadOnlyList<string> MessageLines { get; }
    }
}
