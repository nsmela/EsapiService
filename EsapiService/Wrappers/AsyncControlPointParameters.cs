using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;
using VMS.TPS.Common.Model.API;
using VMS.TPS.Common.Model.Types;
using Esapi.Interfaces;
using Esapi.Services;

namespace Esapi.Wrappers
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
            LeafPositions = inner.LeafPositions;
            PatientSupportAngle = inner.PatientSupportAngle;
            TableTopLateralPosition = inner.TableTopLateralPosition;
            TableTopLongitudinalPosition = inner.TableTopLongitudinalPosition;
            TableTopVerticalPosition = inner.TableTopVerticalPosition;
            GantryAngle = inner.GantryAngle;
            MetersetWeight = inner.MetersetWeight;
        }


        public double CollimatorAngle { get; }

        public int Index { get; }

        public async Task<IReadOnlyList<double>> GetJawPositionsAsync()
        {
            return await _service.RunAsync(() => _inner.JawPositions?.ToList());
        }


        public float[,] LeafPositions { get; private set; }
        public async Task SetLeafPositionsAsync(float[,] value)
        {
            LeafPositions = await _service.RunAsync(() =>
            {
                _inner.LeafPositions = value;
                return _inner.LeafPositions;
            });
        }

        public double PatientSupportAngle { get; }

        public double TableTopLateralPosition { get; }

        public double TableTopLongitudinalPosition { get; }

        public double TableTopVerticalPosition { get; }

        public double GantryAngle { get; private set; }
        public async Task SetGantryAngleAsync(double value)
        {
            GantryAngle = await _service.RunAsync(() =>
            {
                _inner.GantryAngle = value;
                return _inner.GantryAngle;
            });
        }

        public double MetersetWeight { get; private set; }
        public async Task SetMetersetWeightAsync(double value)
        {
            MetersetWeight = await _service.RunAsync(() =>
            {
                _inner.MetersetWeight = value;
                return _inner.MetersetWeight;
            });
        }

        public Task RunAsync(Action<VMS.TPS.Common.Model.API.ControlPointParameters> action) => _service.RunAsync(() => action(_inner));
        public Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.ControlPointParameters, T> func) => _service.RunAsync(() => func(_inner));
    }
}
