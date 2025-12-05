namespace VMS.TPS.Common.Model.API
{
    public interface IDVHEstimationModelSummary : ISerializableObject
    {
        void WriteXml(System.Xml.XmlWriter writer);
        string Description { get; }
        bool IsPublished { get; }
        bool IsTrained { get; }
        string ModelDataVersion { get; }
        VMS.TPS.Common.Model.Types.ParticleType ModelParticleType { get; }
        System.Guid ModelUID { get; }
        string Name { get; }
        int Revision { get; }
        string TreatmentSite { get; }
    }
}
