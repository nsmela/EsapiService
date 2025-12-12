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
            _inner = inner;
            _service = service;

            ControlPoints = inner.ControlPoints;
            WeightFactor = inner.WeightFactor;
        }

        // Simple Void Method
        public Task SetAllLeafPositionsAsync(float[,] leafPositions) => _service.PostAsync(context => _inner.SetAllLeafPositions(leafPositions));

        // Simple Void Method
        public Task SetJawPositionsAsync(VRect<double> positions) => _service.PostAsync(context => _inner.SetJawPositions(positions));

        public IEnumerable<ControlPointParameters> ControlPoints { get; }

        public double WeightFactor { get; private set; }
        public async Task SetWeightFactorAsync(double value)
        {
            WeightFactor = await _service.PostAsync(context => 
            {
                _inner.WeightFactor = value;
                return _inner.WeightFactor;
            });
        }

        public Task RunAsync(Action<VMS.TPS.Common.Model.API.BeamParameters> action) => _service.PostAsync((context) => action(_inner));
        public Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.BeamParameters, T> func) => _service.PostAsync<T>((context) => func(_inner));

        public static implicit operator VMS.TPS.Common.Model.API.BeamParameters(AsyncBeamParameters wrapper) => wrapper;

        // Internal Explicit Implementation to expose _inner safely for covariance
        VMS.TPS.Common.Model.API.BeamParameters IEsapiWrapper<VMS.TPS.Common.Model.API.BeamParameters>.Inner => _inner;
    }
}
