using System.Threading.Tasks;
using VMS.TPS.Common.Model.API;
using VMS.TPS.Common.Model.Types;
using Esapi.Interfaces;

namespace Esapi.Wrappers
{
    public class AsyncActiveStructureCodeDictionaries : IActiveStructureCodeDictionaries
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
            return await _service.RunAsync(() => 
                _inner.Fma is null ? null : new AsyncStructureCodeDictionary(_inner.Fma, _service));
        }

        public async Task<IStructureCodeDictionary> GetRadLexAsync()
        {
            return await _service.RunAsync(() => 
                _inner.RadLex is null ? null : new AsyncStructureCodeDictionary(_inner.RadLex, _service));
        }

        public async Task<IStructureCodeDictionary> GetSrtAsync()
        {
            return await _service.RunAsync(() => 
                _inner.Srt is null ? null : new AsyncStructureCodeDictionary(_inner.Srt, _service));
        }

        public async Task<IStructureCodeDictionary> GetVmsStructCodeAsync()
        {
            return await _service.RunAsync(() => 
                _inner.VmsStructCode is null ? null : new AsyncStructureCodeDictionary(_inner.VmsStructCode, _service));
        }

        public Task RunAsync(Action<VMS.TPS.Common.Model.API.ActiveStructureCodeDictionaries> action) => _service.RunAsync(() => action(_inner));
        public Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.ActiveStructureCodeDictionaries, T> func) => _service.RunAsync(() => func(_inner));
    }
}
