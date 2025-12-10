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
    public class AsyncExternalBeamTreatmentUnit : AsyncApiDataObject, IExternalBeamTreatmentUnit
    {
        internal new readonly VMS.TPS.Common.Model.API.ExternalBeamTreatmentUnit _inner;

        // Store the inner ESAPI object reference
        // internal so other wrappers can access it
        // new to override any inherited _inner fields
        internal new readonly IEsapiService _service;

        public AsyncExternalBeamTreatmentUnit(VMS.TPS.Common.Model.API.ExternalBeamTreatmentUnit inner, IEsapiService service) : base(inner, service)
        {
            _inner = inner;
            _service = service;

            MachineDepartmentName = inner.MachineDepartmentName;
            MachineModel = inner.MachineModel;
            MachineModelName = inner.MachineModelName;
            MachineScaleDisplayName = inner.MachineScaleDisplayName;
            SourceAxisDistance = inner.SourceAxisDistance;
        }


        public string MachineDepartmentName { get; }

        public string MachineModel { get; }

        public string MachineModelName { get; }

        public string MachineScaleDisplayName { get; }

        public async Task<ITreatmentUnitOperatingLimits> GetOperatingLimitsAsync()
        {
            return await _service.PostAsync(context => 
                _inner.OperatingLimits is null ? null : new AsyncTreatmentUnitOperatingLimits(_inner.OperatingLimits, _service));
        }

        public double SourceAxisDistance { get; }

        public Task RunAsync(Action<VMS.TPS.Common.Model.API.ExternalBeamTreatmentUnit> action) => _service.PostAsync((context) => action(_inner));
        public Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.ExternalBeamTreatmentUnit, T> func) => _service.PostAsync<T>((context) => func(_inner));
    }
}
