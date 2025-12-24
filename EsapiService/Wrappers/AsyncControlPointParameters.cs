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
            if (inner is null) throw new ArgumentNullException(nameof(inner));
            if (service is null) throw new ArgumentNullException(nameof(service));

            _inner = inner;
            _service = service;

            CollimatorAngle = inner.CollimatorAngle;
            Index = inner.Index;
            JawPositions = inner.JawPositions;
            LeafPositions = inner.LeafPositions;
            PatientSupportAngle = inner.PatientSupportAngle;
            TableTopLateralPosition = inner.TableTopLateralPosition;
            TableTopLongitudinalPosition = inner.TableTopLongitudinalPosition;
            TableTopVerticalPosition = inner.TableTopVerticalPosition;
            GantryAngle = inner.GantryAngle;
            MetersetWeight = inner.MetersetWeight;
        }


        public double CollimatorAngle { get; private set; }


        public int Index { get; private set; }


        public VRect<double> JawPositions { get; private set; }
        public async Task SetJawPositionsAsync(VRect<double> value)
        {
            JawPositions = await _service.PostAsync(context => 
            {
                _inner.JawPositions = value;
                return _inner.JawPositions;
            });
        }


        public float[,] LeafPositions { get; private set; }
        public async Task SetLeafPositionsAsync(float[,] value)
        {
            LeafPositions = await _service.PostAsync(context => 
            {
                _inner.LeafPositions = value;
                return _inner.LeafPositions;
            });
        }


        public double PatientSupportAngle { get; private set; }


        public double TableTopLateralPosition { get; private set; }


        public double TableTopLongitudinalPosition { get; private set; }


        public double TableTopVerticalPosition { get; private set; }


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

        // updates simple properties that might have changed
        public void Refresh()
        {

            CollimatorAngle = _inner.CollimatorAngle;
            Index = _inner.Index;
            JawPositions = _inner.JawPositions;
            LeafPositions = _inner.LeafPositions;
            PatientSupportAngle = _inner.PatientSupportAngle;
            TableTopLateralPosition = _inner.TableTopLateralPosition;
            TableTopLongitudinalPosition = _inner.TableTopLongitudinalPosition;
            TableTopVerticalPosition = _inner.TableTopVerticalPosition;
            GantryAngle = _inner.GantryAngle;
            MetersetWeight = _inner.MetersetWeight;
        }

        public static implicit operator VMS.TPS.Common.Model.API.ControlPointParameters(AsyncControlPointParameters wrapper) => wrapper._inner;

        // Internal Explicit Implementation to expose _inner safely for covariance
        VMS.TPS.Common.Model.API.ControlPointParameters IEsapiWrapper<VMS.TPS.Common.Model.API.ControlPointParameters>.Inner => _inner;

        // Explicit or Implicit implementation of Service
        // Since _service is private, we expose it via the interface
        IEsapiService IEsapiWrapper<VMS.TPS.Common.Model.API.ControlPointParameters>.Service => _service;
    }
}
