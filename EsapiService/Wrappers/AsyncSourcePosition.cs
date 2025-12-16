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
    public class AsyncSourcePosition : AsyncApiDataObject, ISourcePosition, IEsapiWrapper<VMS.TPS.Common.Model.API.SourcePosition>
    {
        internal new readonly VMS.TPS.Common.Model.API.SourcePosition _inner;

        // Store the inner ESAPI object reference
        // internal so other wrappers can access it
        // new to override any inherited _inner fields
        internal new readonly IEsapiService _service;

public AsyncSourcePosition(VMS.TPS.Common.Model.API.SourcePosition inner, IEsapiService service) : base(inner, service)
        {
            if (inner == null) throw new ArgumentNullException(nameof(inner));
            if (service == null) throw new ArgumentNullException(nameof(service));

            _inner = inner;
            _service = service;

            DwellTime = inner.DwellTime;
            DwellTimeLock = inner.DwellTimeLock;
            NominalDwellTime = inner.NominalDwellTime;
            Transform = inner.Transform;
            Translation = inner.Translation;
        }

        public double DwellTime { get; }

        public bool? DwellTimeLock { get; private set; }
        public async Task SetDwellTimeLockAsync(bool? value)
        {
            DwellTimeLock = await _service.PostAsync(context => 
            {
                _inner.DwellTimeLock = value;
                return _inner.DwellTimeLock;
            });
        }

        public double NominalDwellTime { get; private set; }
        public async Task SetNominalDwellTimeAsync(double value)
        {
            NominalDwellTime = await _service.PostAsync(context => 
            {
                _inner.NominalDwellTime = value;
                return _inner.NominalDwellTime;
            });
        }

        public async Task<IRadioactiveSource> GetRadioactiveSourceAsync()
        {
            var result = await _service.PostAsync(context => 
                _inner.RadioactiveSource is null ? null : new AsyncRadioactiveSource(_inner.RadioactiveSource, _service));
            return result;
        }

        public double[,] Transform { get; }

        public VVector Translation { get; }

        public Task RunAsync(Action<VMS.TPS.Common.Model.API.SourcePosition> action) => _service.PostAsync((context) => action(_inner));
        public Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.SourcePosition, T> func) => _service.PostAsync<T>((context) => func(_inner));

        public static implicit operator VMS.TPS.Common.Model.API.SourcePosition(AsyncSourcePosition wrapper) => wrapper._inner;

        // Internal Explicit Implementation to expose _inner safely for covariance
        VMS.TPS.Common.Model.API.SourcePosition IEsapiWrapper<VMS.TPS.Common.Model.API.SourcePosition>.Inner => _inner;

        // Explicit or Implicit implementation of Service
        // Since _service is private, we expose it via the interface
        IEsapiService IEsapiWrapper<VMS.TPS.Common.Model.API.SourcePosition>.Service => _service;
    }
}
