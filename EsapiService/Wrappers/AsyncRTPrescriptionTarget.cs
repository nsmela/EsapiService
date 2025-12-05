namespace EsapiService.Wrappers
{
    using System.Linq;
    using System.Collections.Generic;
    public class AsyncRTPrescriptionTarget : IRTPrescriptionTarget
    {
        internal readonly VMS.TPS.Common.Model.API.RTPrescriptionTarget _inner;

        // Store the inner ESAPI object reference
        // internal so other wrappers can access it
        // new to override any inherited _inner fields
        internal new readonly IEsapiService _service;

        public AsyncRTPrescriptionTarget(VMS.TPS.Common.Model.API.RTPrescriptionTarget inner, IEsapiService service) : base(inner, service)
        {
            _inner = inner;
            _service = service;

            DosePerFraction = inner.DosePerFraction;
            NumberOfFractions = inner.NumberOfFractions;
            TargetId = inner.TargetId;
            Type = inner.Type;
            Value = inner.Value;
        }

        public void WriteXml(System.Xml.XmlWriter writer) => _inner.WriteXml(writer);
        public System.Collections.Generic.IReadOnlyList<IRTPrescriptionConstraint> Constraints => _inner.Constraints?.Select(x => new AsyncRTPrescriptionConstraint(x, _service)).ToList();
        public VMS.TPS.Common.Model.Types.DoseValue DosePerFraction { get; }
        public int NumberOfFractions { get; }
        public string TargetId { get; }
        public VMS.TPS.Common.Model.Types.RTPrescriptionTargetType Type { get; }
        public double Value { get; }

        public Task RunAsync(Action<VMS.TPS.Common.Model.API.RTPrescriptionTarget> action) => _service.RunAsync(() => action(_inner));
        public Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.RTPrescriptionTarget, T> func) => _service.RunAsync(() => func(_inner));
    }
}
    }
}
