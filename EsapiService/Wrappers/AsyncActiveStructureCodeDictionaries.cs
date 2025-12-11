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
    public class AsyncActiveStructureCodeDictionaries : IActiveStructureCodeDictionaries, IEsapiWrapper<VMS.TPS.Common.Model.API.ActiveStructureCodeDictionaries>
    {
        internal readonly VMS.TPS.Common.Model.API.ActiveStructureCodeDictionaries _inner;

        // Store the inner ESAPI object reference
        // internal so other wrappers can access it
        // new to override any inherited _inner fields
        internal readonly IEsapiService _service;

public AsyncActiveStructureCodeDictionaries(VMS.TPS.Common.Model.API.ActiveStructureCodeDictionaries inner, IEsapiService service)
        {
            _inner = inner;
            _service = service;

        }

        public async Task<IStructureCodeDictionary> GetFmaAsync()
        {
            return await _service.PostAsync(context => 
                _inner.Fma is null ? null : new AsyncStructureCodeDictionary(_inner.Fma, _service));
        }

        public async Task<IStructureCodeDictionary> GetRadLexAsync()
        {
            return await _service.PostAsync(context => 
                _inner.RadLex is null ? null : new AsyncStructureCodeDictionary(_inner.RadLex, _service));
        }

        public async Task<IStructureCodeDictionary> GetSrtAsync()
        {
            return await _service.PostAsync(context => 
                _inner.Srt is null ? null : new AsyncStructureCodeDictionary(_inner.Srt, _service));
        }

        public async Task<IStructureCodeDictionary> GetVmsStructCodeAsync()
        {
            return await _service.PostAsync(context => 
                _inner.VmsStructCode is null ? null : new AsyncStructureCodeDictionary(_inner.VmsStructCode, _service));
        }

        public Task RunAsync(Action<VMS.TPS.Common.Model.API.ActiveStructureCodeDictionaries> action) => _service.PostAsync((context) => action(_inner));
        public Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.ActiveStructureCodeDictionaries, T> func) => _service.PostAsync<T>((context) => func(_inner));

        public static implicit operator VMS.TPS.Common.Model.API.ActiveStructureCodeDictionaries(AsyncActiveStructureCodeDictionaries wrapper) => wrapper._inner;
        // Internal Explicit Implementation to expose _inner safely
        VMS.TPS.Common.Model.API.ActiveStructureCodeDictionaries IEsapiWrapper<VMS.TPS.Common.Model.API.ActiveStructureCodeDictionaries>.Inner => _inner;
    }
}
