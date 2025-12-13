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
    public class AsyncStructureCode : AsyncSerializableObject, IStructureCode, IEsapiWrapper<VMS.TPS.Common.Model.API.StructureCode>
    {
        internal new readonly VMS.TPS.Common.Model.API.StructureCode _inner;

        // Store the inner ESAPI object reference
        // internal so other wrappers can access it
        // new to override any inherited _inner fields
        internal new readonly IEsapiService _service;

public AsyncStructureCode(VMS.TPS.Common.Model.API.StructureCode inner, IEsapiService service) : base(inner, service)
        {
            if (inner == null) throw new ArgumentNullException(nameof(inner));
            if (service == null) throw new ArgumentNullException(nameof(service));

            _inner = inner;
            _service = service;

            Code = inner.Code;
            CodeMeaning = inner.CodeMeaning;
            CodingScheme = inner.CodingScheme;
            DisplayName = inner.DisplayName;
            IsEncompassStructureCode = inner.IsEncompassStructureCode;
        }

        // Simple Method
        public Task<StructureCodeInfo> ToStructureCodeInfoAsync() => 
            _service.PostAsync(context => _inner.ToStructureCodeInfo());

        public string Code { get; }

        public string CodeMeaning { get; }

        public string CodingScheme { get; }

        public string DisplayName { get; }

        public bool IsEncompassStructureCode { get; }

        public Task RunAsync(Action<VMS.TPS.Common.Model.API.StructureCode> action) => _service.PostAsync((context) => action(_inner));
        public Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.StructureCode, T> func) => _service.PostAsync<T>((context) => func(_inner));

        public static implicit operator VMS.TPS.Common.Model.API.StructureCode(AsyncStructureCode wrapper) => wrapper._inner;

        // Internal Explicit Implementation to expose _inner safely for covariance
        VMS.TPS.Common.Model.API.StructureCode IEsapiWrapper<VMS.TPS.Common.Model.API.StructureCode>.Inner => _inner;

        /* --- Skipped Members (Not generated) ---
           - Equals: Explicitly ignored by name
        */
    }
}
