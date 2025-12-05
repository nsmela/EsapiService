namespace EsapiService.Wrappers
{
    public class AsyncCourse : ICourse
    {
        internal readonly VMS.TPS.Common.Model.API.Course _inner;

        // Store the inner ESAPI object reference
        // internal so other wrappers can access it
        // new to override any inherited _inner fields
        internal readonly IEsapiService _service;

        public AsyncCourse(VMS.TPS.Common.Model.API.Course inner, IEsapiService service)
        {
            _inner = inner;
            _service = service;

            Id = inner.Id;
        }

        public string Id { get; }
    }
}
