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
    public partial class AsyncBlock : AsyncApiDataObject, IBlock, IEsapiWrapper<VMS.TPS.Common.Model.API.Block>
    {
        internal new readonly VMS.TPS.Common.Model.API.Block _inner;

        // Store the inner ESAPI object reference
        // internal so other wrappers can access it
        // new to override any inherited _inner fields
        internal new readonly IEsapiService _service;

        public AsyncBlock(VMS.TPS.Common.Model.API.Block inner, IEsapiService service) : base(inner, service)
        {
            if (inner is null) throw new ArgumentNullException(nameof(inner));
            if (service is null) throw new ArgumentNullException(nameof(service));

            _inner = inner;
            _service = service;
        }


        public async Task<IAddOnMaterial> GetAddOnMaterialAsync()
        {
            return await _service.PostAsync(context => {
                var innerResult = _inner.AddOnMaterial is null ? null : new AsyncAddOnMaterial(_inner.AddOnMaterial, _service);
                return innerResult;
            });
        }

        public bool IsDiverging =>
            _inner.IsDiverging;


        public System.Windows.Point[][] Outline
        {
            get => _inner.Outline;
            set => _inner.Outline = value;
        }


        public double TransmissionFactor =>
            _inner.TransmissionFactor;


        public async Task<ITray> GetTrayAsync()
        {
            return await _service.PostAsync(context => {
                var innerResult = _inner.Tray is null ? null : new AsyncTray(_inner.Tray, _service);
                return innerResult;
            });
        }

        public double TrayTransmissionFactor =>
            _inner.TrayTransmissionFactor;


        public BlockType Type =>
            _inner.Type;


        public Task RunAsync(Action<VMS.TPS.Common.Model.API.Block> action) => _service.PostAsync((context) => action(_inner));
        public Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.Block, T> func) => _service.PostAsync<T>((context) => func(_inner));

        // --- Validates --- //
        /// <summary>
        /// Verifies is the wrapped ESAPI object isn't null.
        /// </summary>
        public new bool IsValid() => !IsNotValid();

        /// <summary>
        /// Verifies is the wrapped ESAPI object is null.
        /// </summary>
        public new bool IsNotValid() => _inner is null;

        public static implicit operator VMS.TPS.Common.Model.API.Block(AsyncBlock wrapper) => wrapper._inner;

        // Internal Explicit Implementation to expose _inner safely for covariance
        VMS.TPS.Common.Model.API.Block IEsapiWrapper<VMS.TPS.Common.Model.API.Block>.Inner => _inner;

        // Explicit or Implicit implementation of Service
        // Since _service is private, we expose it via the interface
        IEsapiService IEsapiWrapper<VMS.TPS.Common.Model.API.Block>.Service => _service;
    }
}
