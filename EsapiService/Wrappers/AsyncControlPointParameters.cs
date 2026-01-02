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
        }


        public double CollimatorAngle =>
            _inner.CollimatorAngle;


        public int Index =>
            _inner.Index;


        public VRect<double> JawPositions
        {
            get => _inner.JawPositions;
            set => _inner.JawPositions = value;
        }


        public float[,] LeafPositions
        {
            get => _inner.LeafPositions;
            set => _inner.LeafPositions = value;
        }


        public double PatientSupportAngle =>
            _inner.PatientSupportAngle;


        public double TableTopLateralPosition =>
            _inner.TableTopLateralPosition;


        public double TableTopLongitudinalPosition =>
            _inner.TableTopLongitudinalPosition;


        public double TableTopVerticalPosition =>
            _inner.TableTopVerticalPosition;


        public double GantryAngle
        {
            get => _inner.GantryAngle;
            set => _inner.GantryAngle = value;
        }


        public double MetersetWeight
        {
            get => _inner.MetersetWeight;
            set => _inner.MetersetWeight = value;
        }


        public Task RunAsync(Action<VMS.TPS.Common.Model.API.ControlPointParameters> action) => _service.PostAsync((context) => action(_inner));
        public Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.ControlPointParameters, T> func) => _service.PostAsync<T>((context) => func(_inner));

        // --- Validates --- //
        /// <summary>
        /// Verifies is the wrapped ESAPI object isn't null.
        /// </summary>
        public bool IsValid() => !IsNotValid();

        /// <summary>
        /// Verifies is the wrapped ESAPI object is null.
        /// </summary>
        public bool IsNotValid() => _inner is null;

        public static implicit operator VMS.TPS.Common.Model.API.ControlPointParameters(AsyncControlPointParameters wrapper) => wrapper._inner;

        // Internal Explicit Implementation to expose _inner safely for covariance
        VMS.TPS.Common.Model.API.ControlPointParameters IEsapiWrapper<VMS.TPS.Common.Model.API.ControlPointParameters>.Inner => _inner;

        // Explicit or Implicit implementation of Service
        // Since _service is private, we expose it via the interface
        IEsapiService IEsapiWrapper<VMS.TPS.Common.Model.API.ControlPointParameters>.Service => _service;
    }
}
