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
    public class AsyncBeamParameters : IBeamParameters, IEsapiWrapper<VMS.TPS.Common.Model.API.BeamParameters>
    {
        internal readonly VMS.TPS.Common.Model.API.BeamParameters _inner;

        // Store the inner ESAPI object reference
        // internal so other wrappers can access it
        // new to override any inherited _inner fields
        internal readonly IEsapiService _service;

        public AsyncBeamParameters(VMS.TPS.Common.Model.API.BeamParameters inner, IEsapiService service)
        {
            if (inner is null) throw new ArgumentNullException(nameof(inner));
            if (service is null) throw new ArgumentNullException(nameof(service));

            _inner = inner;
            _service = service;
        }


        // Simple Void Method
        public Task SetAllLeafPositionsAsync(float[,] leafPositions) 
        {
            _service.PostAsync(context => _inner.SetAllLeafPositions(leafPositions));
            return Task.CompletedTask;
        }

        // Simple Void Method
        public Task SetJawPositionsAsync(VRect<double> positions) 
        {
            _service.PostAsync(context => _inner.SetJawPositions(positions));
            return Task.CompletedTask;
        }

        public async Task<IReadOnlyList<IControlPointParameters>> GetControlPointsAsync()
        {
            return await _service.PostAsync(context => 
                _inner.ControlPoints?.Select(x => new AsyncControlPointParameters(x, _service)).ToList());
        }


        public GantryDirection GantryDirection =>
            _inner.GantryDirection;


        public VVector Isocenter
        {
            get => _inner.Isocenter;
            set => _inner.Isocenter = value;
        }


        public double WeightFactor
        {
            get => _inner.WeightFactor;
            set => _inner.WeightFactor = value;
        }


        public Task RunAsync(Action<VMS.TPS.Common.Model.API.BeamParameters> action) => _service.PostAsync((context) => action(_inner));
        public Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.BeamParameters, T> func) => _service.PostAsync<T>((context) => func(_inner));

        // --- Validates --- //
        /// <summary>
        /// Verifies is the wrapped ESAPI object isn't null.
        /// </summary>
        public bool IsValid() => !IsNotValid();

        /// <summary>
        /// Verifies is the wrapped ESAPI object is null.
        /// </summary>
        public bool IsNotValid() => _inner is null;

        public static implicit operator VMS.TPS.Common.Model.API.BeamParameters(AsyncBeamParameters wrapper) => wrapper._inner;

        // Internal Explicit Implementation to expose _inner safely for covariance
        VMS.TPS.Common.Model.API.BeamParameters IEsapiWrapper<VMS.TPS.Common.Model.API.BeamParameters>.Inner => _inner;

        // Explicit or Implicit implementation of Service
        // Since _service is private, we expose it via the interface
        IEsapiService IEsapiWrapper<VMS.TPS.Common.Model.API.BeamParameters>.Service => _service;
    }
}
