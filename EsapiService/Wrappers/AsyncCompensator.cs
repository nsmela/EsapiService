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
    public class AsyncCompensator : AsyncApiDataObject, ICompensator, IEsapiWrapper<VMS.TPS.Common.Model.API.Compensator>
    {
        internal new readonly VMS.TPS.Common.Model.API.Compensator _inner;

        // Store the inner ESAPI object reference
        // internal so other wrappers can access it
        // new to override any inherited _inner fields
        internal new readonly IEsapiService _service;

        public AsyncCompensator(VMS.TPS.Common.Model.API.Compensator inner, IEsapiService service) : base(inner, service)
        {
            if (inner is null) throw new ArgumentNullException(nameof(inner));
            if (service is null) throw new ArgumentNullException(nameof(service));

            _inner = inner;
            _service = service;

        }


        public async Task<IAddOnMaterial> GetMaterialAsync()
        {
            return await _service.PostAsync(context => {
                var innerResult = _inner.Material is null ? null : new AsyncAddOnMaterial(_inner.Material, _service);
                return innerResult;
            });
        }

        public async Task<ISlot> GetSlotAsync()
        {
            return await _service.PostAsync(context => {
                var innerResult = _inner.Slot is null ? null : new AsyncSlot(_inner.Slot, _service);
                return innerResult;
            });
        }

        public async Task<ITray> GetTrayAsync()
        {
            return await _service.PostAsync(context => {
                var innerResult = _inner.Tray is null ? null : new AsyncTray(_inner.Tray, _service);
                return innerResult;
            });
        }

        public Task RunAsync(Action<VMS.TPS.Common.Model.API.Compensator> action) => _service.PostAsync((context) => action(_inner));
        public Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.Compensator, T> func) => _service.PostAsync<T>((context) => func(_inner));

        // updates simple properties that might have changed
        public new void Refresh()
        {
            base.Refresh();
        }

        public static implicit operator VMS.TPS.Common.Model.API.Compensator(AsyncCompensator wrapper) => wrapper._inner;

        // Internal Explicit Implementation to expose _inner safely for covariance
        VMS.TPS.Common.Model.API.Compensator IEsapiWrapper<VMS.TPS.Common.Model.API.Compensator>.Inner => _inner;

        // Explicit or Implicit implementation of Service
        // Since _service is private, we expose it via the interface
        IEsapiService IEsapiWrapper<VMS.TPS.Common.Model.API.Compensator>.Service => _service;
    }
}
