    using System.Threading.Tasks;
namespace EsapiService.Wrappers
{
    using System.Linq;
    using System.Collections.Generic;
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
        }

        public void SetAllLeafPositions(float[,] leafPositions) => _inner.SetAllLeafPositions(leafPositions);
        public void SetJawPositions(VRect<double> positions) => _inner.SetJawPositions(positions);
        public IReadOnlyList<IControlPointParameters> ControlPoints => _inner.ControlPoints?.Select(x => new AsyncControlPointParameters(x, _service)).ToList();
        public GantryDirection GantryDirection { get; }
        public VVector Isocenter => _inner.Isocenter;
        public async Task SetIsocenterAsync(VVector value) => _service.RunAsync(() => _inner.Isocenter = value);
        public double WeightFactor => _inner.WeightFactor;
        public async Task SetWeightFactorAsync(double value) => _service.RunAsync(() => _inner.WeightFactor = value);

        public Task RunAsync(Action<VMS.TPS.Common.Model.API.BeamParameters> action) => _service.RunAsync(() => action(_inner));
        public Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.BeamParameters, T> func) => _service.RunAsync(() => func(_inner));
    }
}
    }
}
