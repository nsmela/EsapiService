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
    public class AsyncControlPointParameters : IControlPointParameters, IEsapiWrapper<VMS.TPS.Common.Model.API.ControlPointParameters>
    {
        internal readonly VMS.TPS.Common.Model.API.ControlPointParameters _inner;

        // Store the inner ESAPI object reference
        // internal so other wrappers can access it
        // new to override any inherited _inner fields
        internal readonly IEsapiService _service;

public AsyncControlPointParameters(VMS.TPS.Common.Model.API.ControlPointParameters inner, IEsapiService service)
        {
            if (inner == null) throw new ArgumentNullException(nameof(inner));
            if (service == null) throw new ArgumentNullException(nameof(service));

            _inner = inner;
            _service = service;

            CollimatorAngle = inner.CollimatorAngle;
            Index = inner.Index;
            JawPositions = inner.JawPositions;
            PatientSupportAngle = inner.PatientSupportAngle;
            TableTopLateralPosition = inner.TableTopLateralPosition;
            TableTopLongitudinalPosition = inner.TableTopLongitudinalPosition;
            TableTopVerticalPosition = inner.TableTopVerticalPosition;
            GantryAngle = inner.GantryAngle;
            MetersetWeight = inner.MetersetWeight;
        }

        public double CollimatorAngle { get; }

        public int Index { get; }

        public VRect<double> JawPositions { get; private set; }
        public async Task SetJawPositionsAsync(VRect<double> value)
        {
            JawPositions = await _service.PostAsync(context => 
            {
                _inner.JawPositions = value;
                return _inner.JawPositions;
            });
        }

        public double PatientSupportAngle { get; }

        public double TableTopLateralPosition { get; }

        public double TableTopLongitudinalPosition { get; }

        public double TableTopVerticalPosition { get; }

        public double GantryAngle { get; private set; }
        public async Task SetGantryAngleAsync(double value)
        {
            GantryAngle = await _service.PostAsync(context => 
            {
                _inner.GantryAngle = value;
                return _inner.GantryAngle;
            });
        }

        public double MetersetWeight { get; private set; }
        public async Task SetMetersetWeightAsync(double value)
        {
            MetersetWeight = await _service.PostAsync(context => 
            {
                _inner.MetersetWeight = value;
                return _inner.MetersetWeight;
            });
        }

        public Task RunAsync(Action<VMS.TPS.Common.Model.API.ControlPointParameters> action) => _service.PostAsync((context) => action(_inner));
        public Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.ControlPointParameters, T> func) => _service.PostAsync<T>((context) => func(_inner));

        public static implicit operator VMS.TPS.Common.Model.API.ControlPointParameters(AsyncControlPointParameters wrapper) => wrapper;

        // Internal Explicit Implementation to expose _inner safely for covariance
        VMS.TPS.Common.Model.API.ControlPointParameters IEsapiWrapper<VMS.TPS.Common.Model.API.ControlPointParameters>.Inner => _inner;
    }
}
