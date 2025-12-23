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
    public class AsyncRadioactiveSource : AsyncApiDataObject, IRadioactiveSource, IEsapiWrapper<VMS.TPS.Common.Model.API.RadioactiveSource>
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

            CalibrationDate = inner.CalibrationDate;
            NominalActivity = inner.NominalActivity;
            SerialNumber = inner.SerialNumber;
            Strength = inner.Strength;
        }


        public DateTime? CalibrationDate { get; private set; }

        public bool NominalActivity { get; private set; }

        public async Task<IRadioactiveSourceModel> GetRadioactiveSourceModelAsync()
        {
            return await _service.PostAsync(context => {
                var innerResult = _inner.RadioactiveSourceModel is null ? null : new AsyncRadioactiveSourceModel(_inner.RadioactiveSourceModel, _service);
                return innerResult;
            });
        }

        public string SerialNumber { get; private set; }

        public double Strength { get; private set; }

        public Task RunAsync(Action<VMS.TPS.Common.Model.API.RadioactiveSource> action) => _service.PostAsync((context) => action(_inner));
        public Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.RadioactiveSource, T> func) => _service.PostAsync<T>((context) => func(_inner));

        // updates simple properties that might have changed
        public new void Refresh()
        {
            base.Refresh();

            CalibrationDate = _inner.CalibrationDate;
            NominalActivity = _inner.NominalActivity;
            SerialNumber = _inner.SerialNumber;
            Strength = _inner.Strength;
        }

        public static implicit operator VMS.TPS.Common.Model.API.RadioactiveSource(AsyncRadioactiveSource wrapper) => wrapper._inner;

        // Internal Explicit Implementation to expose _inner safely for covariance
        VMS.TPS.Common.Model.API.RadioactiveSource IEsapiWrapper<VMS.TPS.Common.Model.API.RadioactiveSource>.Inner => _inner;

        // Explicit or Implicit implementation of Service
        // Since _service is private, we expose it via the interface
        IEsapiService IEsapiWrapper<VMS.TPS.Common.Model.API.RadioactiveSource>.Service => _service;
    }
}
