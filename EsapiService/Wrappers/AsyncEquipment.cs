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
    public sealed class AsyncEquipment : IEquipment, IEsapiWrapper<VMS.TPS.Common.Model.API.Equipment>
    {
        internal readonly VMS.TPS.Common.Model.API.Equipment _inner;

        // Store the inner ESAPI object reference
        // internal so other wrappers can access it
        // new to override any inherited _inner fields
        internal readonly IEsapiService _service;

public AsyncEquipment(VMS.TPS.Common.Model.API.Equipment inner, IEsapiService service)
        {
            if (inner == null) throw new ArgumentNullException(nameof(inner));
            if (service == null) throw new ArgumentNullException(nameof(service));

            _inner = inner;
            _service = service;

        }

        public async Task<IReadOnlyList<IBrachyTreatmentUnit>> GetBrachyTreatmentUnitsAsync()
        {
            return await _service.PostAsync(context => 
                _inner.GetBrachyTreatmentUnits()?.Select(x => new AsyncBrachyTreatmentUnit(x, _service)).ToList());
        }


        public async Task<IReadOnlyList<IExternalBeamTreatmentUnit>> GetExternalBeamTreatmentUnitsAsync()
        {
            return await _service.PostAsync(context => 
                _inner.GetExternalBeamTreatmentUnits()?.Select(x => new AsyncExternalBeamTreatmentUnit(x, _service)).ToList());
        }


        public Task RunAsync(Action<VMS.TPS.Common.Model.API.Equipment> action) => _service.PostAsync((context) => action(_inner));
        public Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.Equipment, T> func) => _service.PostAsync<T>((context) => func(_inner));

        public static implicit operator VMS.TPS.Common.Model.API.Equipment(AsyncEquipment wrapper) => wrapper;

        // Internal Explicit Implementation to expose _inner safely for covariance
        VMS.TPS.Common.Model.API.Equipment IEsapiWrapper<VMS.TPS.Common.Model.API.Equipment>.Inner => _inner;
    }
}
