using System.Threading.Tasks;
using VMS.TPS.Common.Model.API;
using VMS.TPS.Common.Model.Types;
using Esapi.Interfaces;

namespace Esapi.Wrappers
{
    public class AsyncTreatmentUnitOperatingLimits : ITreatmentUnitOperatingLimits
    {
        internal readonly VMS.TPS.Common.Model.API.TreatmentUnitOperatingLimits _inner;

        // Store the inner ESAPI object reference
        // internal so other wrappers can access it
        // new to override any inherited _inner fields
        internal new readonly IEsapiService _service;

        public AsyncTreatmentUnitOperatingLimits(VMS.TPS.Common.Model.API.TreatmentUnitOperatingLimits inner, IEsapiService service) : base(inner, service)
        {
            _inner = inner;
            _service = service;

        }


        public async Task<ITreatmentUnitOperatingLimit> GetCollimatorAngleAsync()
        {
            return await _service.RunAsync(() => 
                _inner.CollimatorAngle is null ? null : new AsyncTreatmentUnitOperatingLimit(_inner.CollimatorAngle, _service));
        }

        public async Task<ITreatmentUnitOperatingLimit> GetGantryAngleAsync()
        {
            return await _service.RunAsync(() => 
                _inner.GantryAngle is null ? null : new AsyncTreatmentUnitOperatingLimit(_inner.GantryAngle, _service));
        }

        public async Task<ITreatmentUnitOperatingLimit> GetMUAsync()
        {
            return await _service.RunAsync(() => 
                _inner.MU is null ? null : new AsyncTreatmentUnitOperatingLimit(_inner.MU, _service));
        }

        public async Task<ITreatmentUnitOperatingLimit> GetPatientSupportAngleAsync()
        {
            return await _service.RunAsync(() => 
                _inner.PatientSupportAngle is null ? null : new AsyncTreatmentUnitOperatingLimit(_inner.PatientSupportAngle, _service));
        }

        public Task RunAsync(Action<VMS.TPS.Common.Model.API.TreatmentUnitOperatingLimits> action) => _service.RunAsync(() => action(_inner));
        public Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.TreatmentUnitOperatingLimits, T> func) => _service.RunAsync(() => func(_inner));
    }
}
