namespace EsapiService.Wrappers
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

        public IStructureCodeDictionary Fma => _inner.Fma is null ? null : new AsyncStructureCodeDictionary(_inner.Fma, _service);

        public IStructureCodeDictionary RadLex => _inner.RadLex is null ? null : new AsyncStructureCodeDictionary(_inner.RadLex, _service);

        public IStructureCodeDictionary Srt => _inner.Srt is null ? null : new AsyncStructureCodeDictionary(_inner.Srt, _service);

        public IStructureCodeDictionary VmsStructCode => _inner.VmsStructCode is null ? null : new AsyncStructureCodeDictionary(_inner.VmsStructCode, _service);

    }
}
