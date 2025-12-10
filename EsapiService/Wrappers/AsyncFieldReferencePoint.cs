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
    public class AsyncFieldReferencePoint : AsyncApiDataObject, IFieldReferencePoint
    {
        internal new readonly VMS.TPS.Common.Model.API.FieldReferencePoint _inner;

        // Store the inner ESAPI object reference
        // internal so other wrappers can access it
        // new to override any inherited _inner fields
        internal new readonly IEsapiService _service;

        public AsyncFieldReferencePoint(VMS.TPS.Common.Model.API.FieldReferencePoint inner, IEsapiService service) : base(inner, service)
        {
            _inner = inner;
            _service = service;

            EffectiveDepth = inner.EffectiveDepth;
            IsFieldDoseNominal = inner.IsFieldDoseNominal;
            IsPrimaryReferencePoint = inner.IsPrimaryReferencePoint;
            SSD = inner.SSD;
        }


        public double EffectiveDepth { get; }

        public bool IsFieldDoseNominal { get; }

        public bool IsPrimaryReferencePoint { get; }

        public async Task<IReferencePoint> GetReferencePointAsync()
        {
            return await _service.PostAsync(context => 
                _inner.ReferencePoint is null ? null : new AsyncReferencePoint(_inner.ReferencePoint, _service));
        }

        public double SSD { get; }

        public Task RunAsync(Action<VMS.TPS.Common.Model.API.FieldReferencePoint> action) => _service.PostAsync((context) => action(_inner));
        public Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.FieldReferencePoint, T> func) => _service.PostAsync<T>((context) => func(_inner));

        public static implicit operator VMS.TPS.Common.Model.API.FieldReferencePoint(AsyncFieldReferencePoint wrapper) => wrapper._inner;
    }
}
