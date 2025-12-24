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
            if (inner is null) throw new ArgumentNullException(nameof(inner));
            if (service is null) throw new ArgumentNullException(nameof(service));

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

        public string Code { get; private set; }


        public string CodeMeaning { get; private set; }


        public string CodingScheme { get; private set; }


        public string DisplayName { get; private set; }


        public bool IsEncompassStructureCode { get; private set; }


        public Task RunAsync(Action<VMS.TPS.Common.Model.API.StructureCode> action) => _service.PostAsync((context) => action(_inner));
        public Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.StructureCode, T> func) => _service.PostAsync<T>((context) => func(_inner));

        // updates simple properties that might have changed
        public new void Refresh()
        {
            base.Refresh();

            Code = _inner.Code;
            CodeMeaning = _inner.CodeMeaning;
            CodingScheme = _inner.CodingScheme;
            DisplayName = _inner.DisplayName;
            IsEncompassStructureCode = _inner.IsEncompassStructureCode;
        }

        public static implicit operator VMS.TPS.Common.Model.API.StructureCode(AsyncStructureCode wrapper) => wrapper._inner;

        // Internal Explicit Implementation to expose _inner safely for covariance
        VMS.TPS.Common.Model.API.StructureCode IEsapiWrapper<VMS.TPS.Common.Model.API.StructureCode>.Inner => _inner;

        // Explicit or Implicit implementation of Service
        // Since _service is private, we expose it via the interface
        IEsapiService IEsapiWrapper<VMS.TPS.Common.Model.API.StructureCode>.Service => _service;

        /* --- Skipped Members (Not generated) ---
           - Equals: Explicitly ignored by name
           - op_Equality: Static members are not supported
           - op_Inequality: Static members are not supported
        */
    }
}
