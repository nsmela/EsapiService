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
    public class AsyncTreatmentUnitOperatingLimits : AsyncSerializableObject, ITreatmentUnitOperatingLimits, IEsapiWrapper<VMS.TPS.Common.Model.API.TreatmentUnitOperatingLimits>
    {
        internal new readonly VMS.TPS.Common.Model.API.TreatmentUnitOperatingLimits _inner;

        // Store the inner ESAPI object reference
        // internal so other wrappers can access it
        // new to override any inherited _inner fields
        internal new readonly IEsapiService _service;

        public AsyncTreatmentUnitOperatingLimits(VMS.TPS.Common.Model.API.TreatmentUnitOperatingLimits inner, IEsapiService service) : base(inner, service)
        {
            if (inner is null) throw new ArgumentNullException(nameof(inner));
            if (service is null) throw new ArgumentNullException(nameof(service));

            _inner = inner;
            _service = service;
        }


        public async Task<ITreatmentUnitOperatingLimit> GetCollimatorAngleAsync()
        {
            return await _service.PostAsync(context => {
                var innerResult = _inner.CollimatorAngle is null ? null : new AsyncTreatmentUnitOperatingLimit(_inner.CollimatorAngle, _service);
                return innerResult;
            });
        }

        public async Task<ITreatmentUnitOperatingLimit> GetGantryAngleAsync()
        {
            return await _service.PostAsync(context => {
                var innerResult = _inner.GantryAngle is null ? null : new AsyncTreatmentUnitOperatingLimit(_inner.GantryAngle, _service);
                return innerResult;
            });
        }

        public async Task<ITreatmentUnitOperatingLimit> GetMUAsync()
        {
            return await _service.PostAsync(context => {
                var innerResult = _inner.MU is null ? null : new AsyncTreatmentUnitOperatingLimit(_inner.MU, _service);
                return innerResult;
            });
        }

        public async Task<ITreatmentUnitOperatingLimit> GetPatientSupportAngleAsync()
        {
            return await _service.PostAsync(context => {
                var innerResult = _inner.PatientSupportAngle is null ? null : new AsyncTreatmentUnitOperatingLimit(_inner.PatientSupportAngle, _service);
                return innerResult;
            });
        }

        public Task RunAsync(Action<VMS.TPS.Common.Model.API.TreatmentUnitOperatingLimits> action) => _service.PostAsync((context) => action(_inner));
        public Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.TreatmentUnitOperatingLimits, T> func) => _service.PostAsync<T>((context) => func(_inner));

        public static implicit operator VMS.TPS.Common.Model.API.TreatmentUnitOperatingLimits(AsyncTreatmentUnitOperatingLimits wrapper) => wrapper._inner;

        // Internal Explicit Implementation to expose _inner safely for covariance
        VMS.TPS.Common.Model.API.TreatmentUnitOperatingLimits IEsapiWrapper<VMS.TPS.Common.Model.API.TreatmentUnitOperatingLimits>.Inner => _inner;

        // Explicit or Implicit implementation of Service
        // Since _service is private, we expose it via the interface
        IEsapiService IEsapiWrapper<VMS.TPS.Common.Model.API.TreatmentUnitOperatingLimits>.Service => _service;
    }
}
