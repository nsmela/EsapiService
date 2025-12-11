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
            _inner = inner;
            _service = service;

            AirGap = inner.AirGap;
            DistalTargetMargin = inner.DistalTargetMargin;
            NominalRange = inner.NominalRange;
            NominalSOBPWidth = inner.NominalSOBPWidth;
            OptionId = inner.OptionId;
            PatientSupportId = inner.PatientSupportId;
            ProximalTargetMargin = inner.ProximalTargetMargin;
            SnoutId = inner.SnoutId;
            SnoutPosition = inner.SnoutPosition;
            VirtualSADX = inner.VirtualSADX;
            VirtualSADY = inner.VirtualSADY;
            LateralMargins = inner.LateralMargins.ToList();
        }

        // Simple Void Method
        public Task ApplyParametersAsync(IBeamParameters beamParams) => _service.PostAsync(context => _inner.ApplyParameters(((AsyncBeamParameters)beamParams)._inner));

        public async Task<IIonBeamParameters> GetEditableParametersAsync()
        {
            return await _service.PostAsync(context => 
                _inner.GetEditableParameters() is var result && result is null ? null : new AsyncIonBeamParameters(result, _service));
        }


        // Simple Method
        public Task<double> GetProtonDeliveryTimeByRoomIdAsNumberAsync(string roomId) => _service.PostAsync(context => _inner.GetProtonDeliveryTimeByRoomIdAsNumber(roomId));

        public double AirGap { get; }

        public double DistalTargetMargin { get; private set; }
        public async Task SetDistalTargetMarginAsync(double value)
        {
            DistalTargetMargin = await _service.PostAsync(context => 
            {
                _inner.DistalTargetMargin = value;
                return _inner.DistalTargetMargin;
            });
        }

        // Simple Collection Property
        public IReadOnlyList<double> LateralMargins { get; }


        public async Task<IReadOnlyList<ILateralSpreadingDevice>> GetLateralSpreadingDevicesAsync()
        {
            return await _service.PostAsync(context => 
                _inner.LateralSpreadingDevices?.Select(x => new AsyncLateralSpreadingDevice(x, _service)).ToList());
        }


        public double NominalRange { get; }

        public double NominalSOBPWidth { get; }

        public string OptionId { get; }

        public string PatientSupportId { get; }

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
        // Internal Explicit Implementation to expose _inner safely
        VMS.TPS.Common.Model.API.IonBeam IEsapiWrapper<VMS.TPS.Common.Model.API.IonBeam>.Inner => _inner;
    }
}
