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
    public class AsyncApplicator : AsyncAddOn, IApplicator
    {
        internal new readonly VMS.TPS.Common.Model.API.Applicator _inner;

        // Store the inner ESAPI object reference
        // internal so other wrappers can access it
        // new to override any inherited _inner fields
        internal new readonly IEsapiService _service;

        public AsyncApplicator(VMS.TPS.Common.Model.API.Applicator inner, IEsapiService service) : base(inner, service)
        {
            _inner = inner;
            _service = service;

            ApplicatorLengthInMM = inner.ApplicatorLengthInMM;
            DiameterInMM = inner.DiameterInMM;
            FieldSizeX = inner.FieldSizeX;
            FieldSizeY = inner.FieldSizeY;
            IsStereotactic = inner.IsStereotactic;
        }


        public double ApplicatorLengthInMM { get; }

        public double DiameterInMM { get; }

        public double FieldSizeX { get; }

        public double FieldSizeY { get; }

        public bool IsStereotactic { get; }

        public Task RunAsync(Action<VMS.TPS.Common.Model.API.Applicator> action) => _service.PostAsync((context) => action(_inner));
        public Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.Applicator, T> func) => _service.PostAsync<T>((context) => func(_inner));
    }
}
