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
    public class AsyncBeamParameters : IBeamParameters
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

            GantryDirection = inner.GantryDirection;
            Isocenter = inner.Isocenter;
            WeightFactor = inner.WeightFactor;
        }


        public Task SetAllLeafPositionsAsync(float[,] leafPositions) => _service.PostAsync(context => _inner.SetAllLeafPositions(leafPositions));

        public Task SetJawPositionsAsync(VRect<double> positions) => _service.PostAsync(context => _inner.SetJawPositions(positions));

        public async Task<IReadOnlyList<IControlPointParameters>> GetControlPointsAsync()
        {
            return await _service.PostAsync(context => 
                _inner.ControlPoints?.Select(x => new AsyncControlPointParameters(x, _service)).ToList());
        }


        public GantryDirection GantryDirection { get; }

        public VVector Isocenter { get; private set; }
        public async Task SetIsocenterAsync(VVector value)
        {
            Isocenter = await _service.PostAsync(context => 
            {
                _inner.Isocenter = value;
                return _inner.Isocenter;
            });
        }

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
    }
}
