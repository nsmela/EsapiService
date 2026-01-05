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
    public partial class AsyncBrachyFieldReferencePoint : AsyncApiDataObject, IBrachyFieldReferencePoint, IEsapiWrapper<VMS.TPS.Common.Model.API.BrachyFieldReferencePoint>
    {
        internal new readonly VMS.TPS.Common.Model.API.BrachyFieldReferencePoint _inner;

        // Store the inner ESAPI object reference
        // internal so other wrappers can access it
        // new to override any inherited _inner fields
        internal new readonly IEsapiService _service;

        public AsyncBrachyFieldReferencePoint(VMS.TPS.Common.Model.API.BrachyFieldReferencePoint inner, IEsapiService service) : base(inner, service)
        {
            if (inner is null) throw new ArgumentNullException(nameof(inner));
            if (service is null) throw new ArgumentNullException(nameof(service));

            _inner = inner;
            _service = service;
        }


        public DoseValue FieldDose =>
            _inner.FieldDose;


        public bool IsFieldDoseNominal =>
            _inner.IsFieldDoseNominal;


        public bool IsPrimaryReferencePoint =>
            _inner.IsPrimaryReferencePoint;


        public async Task<IReferencePoint> GetReferencePointAsync()
        {
            return await _service.PostAsync(context => {
                var innerResult = _inner.ReferencePoint is null ? null : new AsyncReferencePoint(_inner.ReferencePoint, _service);
                return innerResult;
            });
        }

        public VVector RefPointLocation =>
            _inner.RefPointLocation;


        public Task RunAsync(Action<VMS.TPS.Common.Model.API.BrachyFieldReferencePoint> action) => _service.PostAsync((context) => action(_inner));
        public Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.BrachyFieldReferencePoint, T> func) => _service.PostAsync<T>((context) => func(_inner));

        // --- Validates --- //
        /// <summary>
        /// Verifies is the wrapped ESAPI object isn't null.
        /// </summary>
        public new bool IsValid() => !IsNotValid();

        /// <summary>
        /// Verifies is the wrapped ESAPI object is null.
        /// </summary>
        public new bool IsNotValid() => _inner is null;

        public static implicit operator VMS.TPS.Common.Model.API.BrachyFieldReferencePoint(AsyncBrachyFieldReferencePoint wrapper) => wrapper._inner;

        // Internal Explicit Implementation to expose _inner safely for covariance
        VMS.TPS.Common.Model.API.BrachyFieldReferencePoint IEsapiWrapper<VMS.TPS.Common.Model.API.BrachyFieldReferencePoint>.Inner => _inner;

        // Explicit or Implicit implementation of Service
        // Since _service is private, we expose it via the interface
        IEsapiService IEsapiWrapper<VMS.TPS.Common.Model.API.BrachyFieldReferencePoint>.Service => _service;
    }
}
