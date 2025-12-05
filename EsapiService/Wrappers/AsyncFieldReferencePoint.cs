namespace EsapiService.Wrappers
{
    public class AsyncFieldReferencePoint : IFieldReferencePoint
    {
        internal readonly VMS.TPS.Common.Model.API.FieldReferencePoint _inner;

        // Store the inner ESAPI object reference
        // internal so other wrappers can access it
        // new to override any inherited _inner fields
        internal new readonly IEsapiService _service;

        public AsyncFieldReferencePoint(VMS.TPS.Common.Model.API.FieldReferencePoint inner, IEsapiService service) : base(inner, service)
        {
            _inner = inner;
            _service = service;

            EffectiveDepth = inner.EffectiveDepth;
            FieldDose = inner.FieldDose;
            IsFieldDoseNominal = inner.IsFieldDoseNominal;
            IsPrimaryReferencePoint = inner.IsPrimaryReferencePoint;
            RefPointLocation = inner.RefPointLocation;
            SSD = inner.SSD;
        }

        public void WriteXml(System.Xml.XmlWriter writer) => _inner.WriteXml(writer);
        public double EffectiveDepth { get; }
        public VMS.TPS.Common.Model.Types.DoseValue FieldDose { get; }
        public bool IsFieldDoseNominal { get; }
        public bool IsPrimaryReferencePoint { get; }
        public IReferencePoint ReferencePoint => _inner.ReferencePoint is null ? null : new AsyncReferencePoint(_inner.ReferencePoint, _service);

        public VMS.TPS.Common.Model.Types.VVector RefPointLocation { get; }
        public double SSD { get; }

        public Task RunAsync(Action<VMS.TPS.Common.Model.API.FieldReferencePoint> action) => _service.RunAsync(() => action(_inner));
        public Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.FieldReferencePoint, T> func) => _service.RunAsync(() => func(_inner));
    }
}
    }
}
