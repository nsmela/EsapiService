namespace EsapiService.Wrappers
{
    public class AsyncStructure : IStructure
    {
        internal readonly VMS.TPS.Common.Model.API.Structure _inner;

        // Store the inner ESAPI object reference
        // internal so other wrappers can access it
        // new to override any inherited _inner fields
        internal readonly IEsapiService _service;

        public AsyncStructure(VMS.TPS.Common.Model.API.Structure inner, IEsapiService service)
        {
            _inner = inner;
            _service = service;

            Id = inner.Id;
        }

        public string Id { get; }
    }
}
