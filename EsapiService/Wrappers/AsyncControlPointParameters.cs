    using System.Threading.Tasks;
namespace EsapiService.Wrappers
{
    public class AsyncControlPointParameters : IControlPointParameters
    {
        internal readonly VMS.TPS.Common.Model.API.ControlPointParameters _inner;

        // Store the inner ESAPI object reference
        // internal so other wrappers can access it
        // new to override any inherited _inner fields
        internal readonly IEsapiService _service;

        public AsyncControlPointParameters(VMS.TPS.Common.Model.API.ControlPointParameters inner, IEsapiService service)
        {
            _inner = inner;
            _service = service;

            CollimatorAngle = inner.CollimatorAngle;
            Index = inner.Index;
            PatientSupportAngle = inner.PatientSupportAngle;
            TableTopLateralPosition = inner.TableTopLateralPosition;
            TableTopLongitudinalPosition = inner.TableTopLongitudinalPosition;
            TableTopVerticalPosition = inner.TableTopVerticalPosition;
        }

        public double CollimatorAngle { get; }
        public int Index { get; }
        public System.Collections.Generic.IReadOnlyList<double> JawPositions => _inner.JawPositions?.ToList();
        public float[,] LeafPositions => _inner.LeafPositions;
        public async Task SetLeafPositionsAsync(float[,] value) => _service.RunAsync(() => _inner.LeafPositions = value);
        public double PatientSupportAngle { get; }
        public double TableTopLateralPosition { get; }
        public double TableTopLongitudinalPosition { get; }
        public double TableTopVerticalPosition { get; }
        public double GantryAngle => _inner.GantryAngle;
        public async Task SetGantryAngleAsync(double value) => _service.RunAsync(() => _inner.GantryAngle = value);
        public double MetersetWeight => _inner.MetersetWeight;
        public async Task SetMetersetWeightAsync(double value) => _service.RunAsync(() => _inner.MetersetWeight = value);

        public Task RunAsync(Action<VMS.TPS.Common.Model.API.ControlPointParameters> action) => _service.RunAsync(() => action(_inner));
        public Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.ControlPointParameters, T> func) => _service.RunAsync(() => func(_inner));
    }
}
    }
}
