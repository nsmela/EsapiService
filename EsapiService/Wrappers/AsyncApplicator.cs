namespace EsapiService.Wrappers
{
    public class AsyncApplicator : IApplicator
    {
        internal readonly VMS.TPS.Common.Model.API.Applicator _inner;

        // Store the inner ESAPI object reference
        // internal so other wrappers can access it
        // new to override any inherited _inner fields
        internal new readonly IEsapiService _service;

        public AsyncApplicator(VMS.TPS.Common.Model.API.Applicator inner, IEsapiService service) : base(inner, service)
        {
            _inner = inner;
            _service = service;

            ApplicatorLengthInMM = inner.ApplicatorLengthInMM;
            DiameterInMM = inner.DiameterInMM;
            FieldSizeX = inner.FieldSizeX;
            FieldSizeY = inner.FieldSizeY;
            IsStereotactic = inner.IsStereotactic;
        }

        public void WriteXml(System.Xml.XmlWriter writer) => _inner.WriteXml(writer);
        public double ApplicatorLengthInMM { get; }
        public double DiameterInMM { get; }
        public double FieldSizeX { get; }
        public double FieldSizeY { get; }
        public bool IsStereotactic { get; }

        public Task RunAsync(Action<VMS.TPS.Common.Model.API.Applicator> action) => _service.RunAsync(() => action(_inner));
        public Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.Applicator, T> func) => _service.RunAsync(() => func(_inner));
    }
}
    }
}
