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
    public class AsyncDVHData : AsyncSerializableObject, IDVHData
    {
        internal new readonly VMS.TPS.Common.Model.API.DVHData _inner;

        // Store the inner ESAPI object reference
        // internal so other wrappers can access it
        // new to override any inherited _inner fields
        internal new readonly IEsapiService _service;

        public AsyncDVHData(VMS.TPS.Common.Model.API.DVHData inner, IEsapiService service) : base(inner, service)
        {
            _inner = inner;
            _service = service;

            Coverage = inner.Coverage;
            SamplingCoverage = inner.SamplingCoverage;
            StdDev = inner.StdDev;
            Volume = inner.Volume;
        }


        public double Coverage { get; }

        public double SamplingCoverage { get; }

        public double StdDev { get; }

        public double Volume { get; }

        public Task RunAsync(Action<VMS.TPS.Common.Model.API.DVHData> action) => _service.PostAsync((context) => action(_inner));
        public Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.DVHData, T> func) => _service.PostAsync<T>((context) => func(_inner));
    }
}
