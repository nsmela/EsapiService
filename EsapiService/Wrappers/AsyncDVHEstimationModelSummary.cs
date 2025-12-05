namespace EsapiService.Wrappers
{
    public class AsyncDVHEstimationModelSummary : IDVHEstimationModelSummary
    {
        internal readonly VMS.TPS.Common.Model.API.DVHEstimationModelSummary _inner;

        // Store the inner ESAPI object reference
        // internal so other wrappers can access it
        // new to override any inherited _inner fields
        internal new readonly IEsapiService _service;

        public AsyncDVHEstimationModelSummary(VMS.TPS.Common.Model.API.DVHEstimationModelSummary inner, IEsapiService service) : base(inner, service)
        {
            _inner = inner;
            _service = service;

            Description = inner.Description;
            IsPublished = inner.IsPublished;
            IsTrained = inner.IsTrained;
            ModelDataVersion = inner.ModelDataVersion;
            ModelParticleType = inner.ModelParticleType;
            ModelUID = inner.ModelUID;
            Name = inner.Name;
            Revision = inner.Revision;
            TreatmentSite = inner.TreatmentSite;
        }

        public void WriteXml(System.Xml.XmlWriter writer) => _inner.WriteXml(writer);
        public string Description { get; }
        public bool IsPublished { get; }
        public bool IsTrained { get; }
        public string ModelDataVersion { get; }
        public VMS.TPS.Common.Model.Types.ParticleType ModelParticleType { get; }
        public System.Guid ModelUID { get; }
        public string Name { get; }
        public int Revision { get; }
        public string TreatmentSite { get; }
    }
}
