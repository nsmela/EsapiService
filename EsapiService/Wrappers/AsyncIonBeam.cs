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
    public class AsyncIonBeam : AsyncBeam, IIonBeam, IEsapiWrapper<VMS.TPS.Common.Model.API.IonBeam>
    {
        internal new readonly VMS.TPS.Common.Model.API.IonBeam _inner;

        // Store the inner ESAPI object reference
        // internal so other wrappers can access it
        // new to override any inherited _inner fields
        internal new readonly IEsapiService _service;

        public AsyncIonBeam(VMS.TPS.Common.Model.API.IonBeam inner, IEsapiService service) : base(inner, service)
        {
            if (inner is null) throw new ArgumentNullException(nameof(inner));
            if (service is null) throw new ArgumentNullException(nameof(service));

            _inner = inner;
            _service = service;

            AirGap = inner.AirGap;
            DistalTargetMargin = inner.DistalTargetMargin;
            LateralMargins = inner.LateralMargins;
            NominalRange = inner.NominalRange;
            NominalSOBPWidth = inner.NominalSOBPWidth;
            OptionId = inner.OptionId;
            PatientSupportId = inner.PatientSupportId;
            PatientSupportType = inner.PatientSupportType;
            ProximalTargetMargin = inner.ProximalTargetMargin;
            ScanMode = inner.ScanMode;
            SnoutId = inner.SnoutId;
            VirtualSADX = inner.VirtualSADX;
            VirtualSADY = inner.VirtualSADY;
        }


        public double AirGap { get; }

        public double DistalTargetMargin { get; }

        public VRect<double> LateralMargins { get; }

        public async Task<IReadOnlyList<ILateralSpreadingDevice>> GetLateralSpreadingDevicesAsync()
        {
            return await _service.PostAsync(context => 
                _inner.LateralSpreadingDevices?.Select(x => new AsyncLateralSpreadingDevice(x, _service)).ToList());
        }


        public double NominalRange { get; }

        public double NominalSOBPWidth { get; }

        public string OptionId { get; }

        public string PatientSupportId { get; }

        public PatientSupportType PatientSupportType { get; }

        public async Task<IIonControlPointCollection> GetIonControlPointsAsync()
        {
            return await _service.PostAsync(context => {
                var innerResult = _inner.IonControlPoints is null ? null : new AsyncIonControlPointCollection(_inner.IonControlPoints, _service);
                return innerResult;
            });
        }

        public double ProximalTargetMargin { get; }

        public async Task<IReadOnlyList<IRangeModulator>> GetRangeModulatorsAsync()
        {
            return await _service.PostAsync(context => 
                _inner.RangeModulators?.Select(x => new AsyncRangeModulator(x, _service)).ToList());
        }


        public async Task<IReadOnlyList<IRangeShifter>> GetRangeShiftersAsync()
        {
            return await _service.PostAsync(context => 
                _inner.RangeShifters?.Select(x => new AsyncRangeShifter(x, _service)).ToList());
        }


        public IonBeamScanMode ScanMode { get; }

        public string SnoutId { get; }

        public async Task<IStructure> GetTargetStructureAsync()
        {
            return await _service.PostAsync(context => {
                var innerResult = _inner.TargetStructure is null ? null : new AsyncStructure(_inner.TargetStructure, _service);
                return innerResult;
            });
        }

        public double VirtualSADX { get; }

        public double VirtualSADY { get; }

        public Task RunAsync(Action<VMS.TPS.Common.Model.API.IonBeam> action) => _service.PostAsync((context) => action(_inner));
        public Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.IonBeam, T> func) => _service.PostAsync<T>((context) => func(_inner));

        public static implicit operator VMS.TPS.Common.Model.API.IonBeam(AsyncIonBeam wrapper) => wrapper._inner;

        // Internal Explicit Implementation to expose _inner safely for covariance
        VMS.TPS.Common.Model.API.IonBeam IEsapiWrapper<VMS.TPS.Common.Model.API.IonBeam>.Inner => _inner;

        // Explicit or Implicit implementation of Service
        // Since _service is private, we expose it via the interface
        IEsapiService IEsapiWrapper<VMS.TPS.Common.Model.API.IonBeam>.Service => _service;

        /* --- Skipped Members (Not generated) ---
           - GetEditableParameters: Shadows base member in wrapped base class
        */
    }
}
