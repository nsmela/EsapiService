using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VMS.TPS.Common.Model.API;
using VMS.TPS.Common.Model.Types;
using Esapi.Interfaces;
using Esapi.Services;

namespace Esapi.Wrappers
{
    public class AsyncFieldReferencePoint : AsyncApiDataObject, IFieldReferencePoint, IEsapiWrapper<VMS.TPS.Common.Model.API.FieldReferencePoint>
    {
        internal new readonly VMS.TPS.Common.Model.API.FieldReferencePoint _inner;

        // Store the inner ESAPI object reference
        // internal so other wrappers can access it
        // new to override any inherited _inner fields
        internal new readonly IEsapiService _service;

        public AsyncFieldReferencePoint(VMS.TPS.Common.Model.API.FieldReferencePoint inner, IEsapiService service) : base(inner, service)
        {
            if (inner is null) throw new ArgumentNullException(nameof(inner));
            if (service is null) throw new ArgumentNullException(nameof(service));

            _inner = inner;
            _service = service;

            EffectiveDepth = inner.EffectiveDepth;
            FieldDose = inner.FieldDose;
            IsFieldDoseNominal = inner.IsFieldDoseNominal;
            IsPrimaryReferencePoint = inner.IsPrimaryReferencePoint;
            RefPointLocation = inner.RefPointLocation;
            SSD = inner.SSD;
        }


        public double EffectiveDepth { get; private set; }

        public DoseValue FieldDose { get; private set; }

        public bool IsFieldDoseNominal { get; private set; }

        public bool IsPrimaryReferencePoint { get; private set; }

        public async Task<IReferencePoint> GetReferencePointAsync()
        {
            return await _service.PostAsync(context => {
                var innerResult = _inner.ReferencePoint is null ? null : new AsyncReferencePoint(_inner.ReferencePoint, _service);
                return innerResult;
            });
        }

        public VVector RefPointLocation { get; private set; }

        public double SSD { get; private set; }

        public Task RunAsync(Action<VMS.TPS.Common.Model.API.FieldReferencePoint> action) => _service.PostAsync((context) => action(_inner));
        public Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.FieldReferencePoint, T> func) => _service.PostAsync<T>((context) => func(_inner));

        // updates simple properties that might have changed
        public new void Refresh()
        {
            base.Refresh();

            EffectiveDepth = _inner.EffectiveDepth;
            FieldDose = _inner.FieldDose;
            IsFieldDoseNominal = _inner.IsFieldDoseNominal;
            IsPrimaryReferencePoint = _inner.IsPrimaryReferencePoint;
            RefPointLocation = _inner.RefPointLocation;
            SSD = _inner.SSD;
        }

        public static implicit operator VMS.TPS.Common.Model.API.FieldReferencePoint(AsyncFieldReferencePoint wrapper) => wrapper._inner;

        // Internal Explicit Implementation to expose _inner safely for covariance
        VMS.TPS.Common.Model.API.FieldReferencePoint IEsapiWrapper<VMS.TPS.Common.Model.API.FieldReferencePoint>.Inner => _inner;

        // Explicit or Implicit implementation of Service
        // Since _service is private, we expose it via the interface
        IEsapiService IEsapiWrapper<VMS.TPS.Common.Model.API.FieldReferencePoint>.Service => _service;
    }
}
