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
    public class AsyncTechnique : AsyncApiDataObject, ITechnique
    {
        internal readonly VMS.TPS.Common.Model.API.Technique _inner;

        // Store the inner ESAPI object reference
        // internal so other wrappers can access it
        // new to override any inherited _inner fields
        internal new readonly IEsapiService _service;

        public AsyncTechnique(VMS.TPS.Common.Model.API.Technique inner, IEsapiService service) : base(inner, service)
        {
            _inner = inner;
            _service = service;

            IsArc = inner.IsArc;
            IsModulatedScanning = inner.IsModulatedScanning;
            IsProton = inner.IsProton;
            IsScanning = inner.IsScanning;
            IsStatic = inner.IsStatic;
        }


        public bool IsArc { get; }

        public bool IsModulatedScanning { get; }

        public bool IsProton { get; }

        public bool IsScanning { get; }

        public bool IsStatic { get; }

        public Task RunAsync(Action<VMS.TPS.Common.Model.API.Technique> action) => _service.PostAsync((context) => action(_inner));
        public Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.Technique, T> func) => _service.PostAsync<T>((context) => func(_inner));
    }
}
