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
    public class AsyncCalculateBrachy3DDoseResult : AsyncSerializableObject, ICalculateBrachy3DDoseResult
    {
        internal new readonly VMS.TPS.Common.Model.API.CalculateBrachy3DDoseResult _inner;

        // Store the inner ESAPI object reference
        // internal so other wrappers can access it
        // new to override any inherited _inner fields
        internal new readonly IEsapiService _service;

        public AsyncCalculateBrachy3DDoseResult(VMS.TPS.Common.Model.API.CalculateBrachy3DDoseResult inner, IEsapiService service) : base(inner, service)
        {
            _inner = inner;
            _service = service;

            RoundedDwellTimeAdjustRatio = inner.RoundedDwellTimeAdjustRatio;
            Success = inner.Success;
            Errors = inner.Errors.ToList();
        }


        public IReadOnlyList<string> Errors { get; }


        public double RoundedDwellTimeAdjustRatio { get; }

        public bool Success { get; }

        public Task RunAsync(Action<VMS.TPS.Common.Model.API.CalculateBrachy3DDoseResult> action) => _service.PostAsync((context) => action(_inner));
        public Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.CalculateBrachy3DDoseResult, T> func) => _service.PostAsync<T>((context) => func(_inner));

        public static implicit operator VMS.TPS.Common.Model.API.CalculateBrachy3DDoseResult(AsyncCalculateBrachy3DDoseResult wrapper) => wrapper._inner;
    }
}
