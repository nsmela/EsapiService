namespace EsapiService.Wrappers
{
    public class AsyncBrachyFieldReferencePoint : IBrachyFieldReferencePoint
    {
        internal readonly VMS.TPS.Common.Model.API.BrachyFieldReferencePoint _inner;

        // Store the inner ESAPI object reference
        // internal so other wrappers can access it
        // new to override any inherited _inner fields
        internal new readonly IEsapiService _service;

        public AsyncBrachyFieldReferencePoint(VMS.TPS.Common.Model.API.BrachyFieldReferencePoint inner, IEsapiService service) : base(inner, service)
        {
            _inner = inner;
            _service = service;

            FieldDose = inner.FieldDose;
            IsFieldDoseNominal = inner.IsFieldDoseNominal;
            IsPrimaryReferencePoint = inner.IsPrimaryReferencePoint;
            RefPointLocation = inner.RefPointLocation;
        }

        public void WriteXml(System.Xml.XmlWriter writer) => _inner.WriteXml(writer);
        public VMS.TPS.Common.Model.Types.DoseValue FieldDose { get; }
        public bool IsFieldDoseNominal { get; }
        public bool IsPrimaryReferencePoint { get; }
        public IReferencePoint ReferencePoint => _inner.ReferencePoint is null ? null : new AsyncReferencePoint(_inner.ReferencePoint, _service);

        public VMS.TPS.Common.Model.Types.VVector RefPointLocation { get; }

        public Task RunAsync(Action<VMS.TPS.Common.Model.API.BrachyFieldReferencePoint> action) => _service.RunAsync(() => action(_inner));
        public Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.BrachyFieldReferencePoint, T> func) => _service.RunAsync(() => func(_inner));
    }
}
    }
}
