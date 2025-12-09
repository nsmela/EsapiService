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
    public class AsyncBrachyFieldReferencePoint : AsyncApiDataObject, IBrachyFieldReferencePoint
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


        public DoseValue FieldDose { get; }

        public bool IsFieldDoseNominal { get; }

        public bool IsPrimaryReferencePoint { get; }

        public async Task<IReferencePoint> GetReferencePointAsync()
        {
            return await _service.PostAsync(context => 
                _inner.ReferencePoint is null ? null : new AsyncReferencePoint(_inner.ReferencePoint, _service));
        }

        public VVector RefPointLocation { get; }

        public Task RunAsync(Action<VMS.TPS.Common.Model.API.BrachyFieldReferencePoint> action) => _service.PostAsync((context) => action(_inner));
        public Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.BrachyFieldReferencePoint, T> func) => _service.PostAsync<T>((context) => func(_inner));
    }
}
