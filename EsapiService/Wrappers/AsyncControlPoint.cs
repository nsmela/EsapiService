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
    public class AsyncControlPoint : AsyncSerializableObject, IControlPoint, IEsapiWrapper<VMS.TPS.Common.Model.API.ControlPoint>
    {
        internal new readonly VMS.TPS.Common.Model.API.ControlPoint _inner;

        // Store the inner ESAPI object reference
        // internal so other wrappers can access it
        // new to override any inherited _inner fields
        internal new readonly IEsapiService _service;

public AsyncControlPoint(VMS.TPS.Common.Model.API.ControlPoint inner, IEsapiService service) : base(inner, service)
        {
            _inner = inner;
            _service = service;

            CollimatorAngle = inner.CollimatorAngle;
            GantryAngle = inner.GantryAngle;
            Index = inner.Index;
            JawPositions = inner.JawPositions;
            LeafPositions = inner.LeafPositions;
            MetersetWeight = inner.MetersetWeight;
            PatientSupportAngle = inner.PatientSupportAngle;
            TableTopLateralPosition = inner.TableTopLateralPosition;
            TableTopLongitudinalPosition = inner.TableTopLongitudinalPosition;
            TableTopVerticalPosition = inner.TableTopVerticalPosition;
        }

        public async Task<IBeam> GetBeamAsync()
        {
            return await _service.PostAsync(context => 
                _inner.Beam is null ? null : new AsyncBeam(_inner.Beam, _service));
        }

        public double CollimatorAngle { get; }

        public double GantryAngle { get; }

        public int Index { get; }

        public VRect<double> JawPositions { get; }

        public float[,] LeafPositions { get; }

        public double MetersetWeight { get; }

        public double PatientSupportAngle { get; }

        public double TableTopLateralPosition { get; }

        public double TableTopLongitudinalPosition { get; }

        public double TableTopVerticalPosition { get; }

        public Task RunAsync(Action<VMS.TPS.Common.Model.API.ControlPoint> action) => _service.PostAsync((context) => action(_inner));
        public Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.ControlPoint, T> func) => _service.PostAsync<T>((context) => func(_inner));

        public static implicit operator VMS.TPS.Common.Model.API.ControlPoint(AsyncControlPoint wrapper) => wrapper;

        // Internal Explicit Implementation to expose _inner safely for covariance
        VMS.TPS.Common.Model.API.ControlPoint IEsapiWrapper<VMS.TPS.Common.Model.API.ControlPoint>.Inner => _inner;
    }
}
