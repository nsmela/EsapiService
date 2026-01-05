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
    public partial class AsyncRadioactiveSource : AsyncApiDataObject, IRadioactiveSource, IEsapiWrapper<VMS.TPS.Common.Model.API.RadioactiveSource>
    {
        internal new readonly VMS.TPS.Common.Model.API.RadioactiveSource _inner;

        // Store the inner ESAPI object reference
        // internal so other wrappers can access it
        // new to override any inherited _inner fields
        internal new readonly IEsapiService _service;

        public AsyncRadioactiveSource(VMS.TPS.Common.Model.API.RadioactiveSource inner, IEsapiService service) : base(inner, service)
        {
            if (inner is null) throw new ArgumentNullException(nameof(inner));
            if (service is null) throw new ArgumentNullException(nameof(service));

            _inner = inner;
            _service = service;
        }


        public DateTime? CalibrationDate =>
            _inner.CalibrationDate;


        public bool NominalActivity =>
            _inner.NominalActivity;


        public async Task<IRadioactiveSourceModel> GetRadioactiveSourceModelAsync()
        {
            return await _service.PostAsync(context => {
                var innerResult = _inner.RadioactiveSourceModel is null ? null : new AsyncRadioactiveSourceModel(_inner.RadioactiveSourceModel, _service);
                return innerResult;
            });
        }

        public string SerialNumber =>
            _inner.SerialNumber;


        public double Strength =>
            _inner.Strength;


        public Task RunAsync(Action<VMS.TPS.Common.Model.API.RadioactiveSource> action) => _service.PostAsync((context) => action(_inner));
        public Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.RadioactiveSource, T> func) => _service.PostAsync<T>((context) => func(_inner));

        // --- Validates --- //
        /// <summary>
        /// Verifies is the wrapped ESAPI object isn't null.
        /// </summary>
        public new bool IsValid() => !IsNotValid();

        /// <summary>
        /// Verifies is the wrapped ESAPI object is null.
        /// </summary>
        public new bool IsNotValid() => _inner is null;

        public static implicit operator VMS.TPS.Common.Model.API.RadioactiveSource(AsyncRadioactiveSource wrapper) => wrapper._inner;

        // Internal Explicit Implementation to expose _inner safely for covariance
        VMS.TPS.Common.Model.API.RadioactiveSource IEsapiWrapper<VMS.TPS.Common.Model.API.RadioactiveSource>.Inner => _inner;

        // Explicit or Implicit implementation of Service
        // Since _service is private, we expose it via the interface
        IEsapiService IEsapiWrapper<VMS.TPS.Common.Model.API.RadioactiveSource>.Service => _service;
    }
}
