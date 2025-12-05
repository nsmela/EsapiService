namespace VMS.TPS.Common.Model.API
{
    public interface IApplicator : IAddOn
    {
        void WriteXml(System.Xml.XmlWriter writer);
        double ApplicatorLengthInMM { get; }
        double DiameterInMM { get; }
        double FieldSizeX { get; }
        double FieldSizeY { get; }
        bool IsStereotactic { get; }
    }
}
