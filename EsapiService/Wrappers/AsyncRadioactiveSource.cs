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
            _inner = inner;
            _service = service;

            CalibrationDate = inner.CalibrationDate;
            NominalActivity = inner.NominalActivity;
            SerialNumber = inner.SerialNumber;
            Strength = inner.Strength;
        }

        public DateTime? CalibrationDate { get; }

        public bool NominalActivity { get; }

        public async Task<IRadioactiveSourceModel> GetRadioactiveSourceModelAsync()
        {
            return await _service.PostAsync(context => 
                _inner.RadioactiveSourceModel is null ? null : new AsyncRadioactiveSourceModel(_inner.RadioactiveSourceModel, _service));
        }

        public string SerialNumber { get; }

        public double Strength { get; }

        public Task RunAsync(Action<VMS.TPS.Common.Model.API.RadioactiveSource> action) => _service.PostAsync((context) => action(_inner));
        public Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.RadioactiveSource, T> func) => _service.PostAsync<T>((context) => func(_inner));

        public static implicit operator VMS.TPS.Common.Model.API.RadioactiveSource(AsyncRadioactiveSource wrapper) => wrapper._inner;
        // Internal Explicit Implementation to expose _inner safely
        VMS.TPS.Common.Model.API.RadioactiveSource IEsapiWrapper<VMS.TPS.Common.Model.API.RadioactiveSource>.Inner => _inner;
    }
}
