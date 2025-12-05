namespace EsapiService.Wrappers
{
    public class AsyncStructureCode : IStructureCode
    {
        internal readonly VMS.TPS.Common.Model.API.StructureCode _inner;

        // Store the inner ESAPI object reference
        // internal so other wrappers can access it
        // new to override any inherited _inner fields
        internal new readonly IEsapiService _service;

        public AsyncStructureCode(VMS.TPS.Common.Model.API.StructureCode inner, IEsapiService service) : base(inner, service)
        {
            _inner = inner;
            _service = service;

            Code = inner.Code;
            CodeMeaning = inner.CodeMeaning;
            CodingScheme = inner.CodingScheme;
            DisplayName = inner.DisplayName;
            IsEncompassStructureCode = inner.IsEncompassStructureCode;
        }

        public StructureCodeInfo ToStructureCodeInfo() => _inner.ToStructureCodeInfo();
        public bool Equals(IStructureCode other) => _inner.Equals(other);
        public string Code { get; }
        public string CodeMeaning { get; }
        public string CodingScheme { get; }
        public string DisplayName { get; }
        public bool IsEncompassStructureCode { get; }

        public Task RunAsync(Action<VMS.TPS.Common.Model.API.StructureCode> action) => _service.RunAsync(() => action(_inner));
        public Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.StructureCode, T> func) => _service.RunAsync(() => func(_inner));
    }
}
    }
}
