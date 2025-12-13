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
            if (inner == null) throw new ArgumentNullException(nameof(inner));
            if (service == null) throw new ArgumentNullException(nameof(service));

            _inner = inner;
            _service = service;

            AirGap = inner.AirGap;
            BeamLineStatus = inner.BeamLineStatus;
            DistalTargetMargin = inner.DistalTargetMargin;
            LateralMargins = inner.LateralMargins;
            LateralSpreadingDevices = inner.LateralSpreadingDevices;
            NominalRange = inner.NominalRange;
            NominalSOBPWidth = inner.NominalSOBPWidth;
            OptionId = inner.OptionId;
            PatientSupportId = inner.PatientSupportId;
            PatientSupportType = inner.PatientSupportType;
            ProximalTargetMargin = inner.ProximalTargetMargin;
            RangeModulators = inner.RangeModulators;
            RangeShifters = inner.RangeShifters;
            ScanMode = inner.ScanMode;
            SnoutId = inner.SnoutId;
            SnoutPosition = inner.SnoutPosition;
            VirtualSADX = inner.VirtualSADX;
            VirtualSADY = inner.VirtualSADY;
        }

        // Simple Method
        public Task<ProtonDeliveryTimeStatus> GetDeliveryTimeStatusByRoomIdAsync(string roomId) => 
            _service.PostAsync(context => _inner.GetDeliveryTimeStatusByRoomId(roomId));

        // Simple Method
        public Task<double> GetProtonDeliveryTimeByRoomIdAsNumberAsync(string roomId) => 
            _service.PostAsync(context => _inner.GetProtonDeliveryTimeByRoomIdAsNumber(roomId));

        public double AirGap { get; }

        public ProtonBeamLineStatus BeamLineStatus { get; }

        public double DistalTargetMargin { get; private set; }
        public async Task SetDistalTargetMarginAsync(double value)
        {
            DistalTargetMargin = await _service.PostAsync(context => 
            {
                _inner.DistalTargetMargin = value;
                return _inner.DistalTargetMargin;
            });
        }

        public VRect<double> LateralMargins { get; private set; }
        public async Task SetLateralMarginsAsync(VRect<double> value)
        {
            LateralMargins = await _service.PostAsync(context => 
            {
                _inner.LateralMargins = value;
                return _inner.LateralMargins;
            });
        }

        public IEnumerable<LateralSpreadingDevice> LateralSpreadingDevices { get; }

        public double NominalRange { get; }

        public double NominalSOBPWidth { get; }

        public string OptionId { get; }

        public string PatientSupportId { get; }

        public PatientSupportType PatientSupportType { get; }

        public async Task<IIonControlPointCollection> GetIonControlPointsAsync()
        {
            return await _service.PostAsync(context => 
                _inner.IonControlPoints is null ? null : new AsyncIonControlPointCollection(_inner.IonControlPoints, _service));
        }

        public double ProximalTargetMargin { get; private set; }
        public async Task SetProximalTargetMarginAsync(double value)
        {
            ProximalTargetMargin = await _service.PostAsync(context => 
            {
                _inner.ProximalTargetMargin = value;
                return _inner.ProximalTargetMargin;
            });
        }

        public IEnumerable<RangeModulator> RangeModulators { get; }

        public IEnumerable<RangeShifter> RangeShifters { get; }

        public IonBeamScanMode ScanMode { get; }

        public string SnoutId { get; }

        public double SnoutPosition { get; }

        public async Task<IStructure> GetTargetStructureAsync()
        {
            return await _service.PostAsync(context => 
                _inner.TargetStructure is null ? null : new AsyncStructure(_inner.TargetStructure, _service));
        }

        public double VirtualSADX { get; }

        public double VirtualSADY { get; }

        public Task RunAsync(Action<VMS.TPS.Common.Model.API.IonBeam> action) => _service.PostAsync((context) => action(_inner));
        public Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.IonBeam, T> func) => _service.PostAsync<T>((context) => func(_inner));

        public static implicit operator VMS.TPS.Common.Model.API.IonBeam(AsyncIonBeam wrapper) => wrapper._inner;

        // Internal Explicit Implementation to expose _inner safely for covariance
        VMS.TPS.Common.Model.API.IonBeam IEsapiWrapper<VMS.TPS.Common.Model.API.IonBeam>.Inner => _inner;

        /* --- Skipped Members (Not generated) ---
           - ApplyParameters: Shadows base member in wrapped base class
           - GetEditableParameters: Shadows base member in wrapped base class
        */
    }
}
