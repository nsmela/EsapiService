using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;
using VMS.TPS.Common.Model.API;
using VMS.TPS.Common.Model.Types;

namespace EsapiService.Wrappers
{
    public class AsyncIonBeam : IIonBeam
    {
        internal readonly VMS.TPS.Common.Model.API.IonBeam _inner;

        // Store the inner ESAPI object reference
        // internal so other wrappers can access it
        // new to override any inherited _inner fields
        internal new readonly IEsapiService _service;

        public AsyncIonBeam(VMS.TPS.Common.Model.API.IonBeam inner, IEsapiService service) : base(inner, service)
        {
            _inner = inner;
            _service = service;

            AirGap = inner.AirGap;
            BeamLineStatus = inner.BeamLineStatus;
            DistalTargetMargin = inner.DistalTargetMargin;
            NominalRange = inner.NominalRange;
            NominalSOBPWidth = inner.NominalSOBPWidth;
            OptionId = inner.OptionId;
            PatientSupportId = inner.PatientSupportId;
            PatientSupportType = inner.PatientSupportType;
            ProximalTargetMargin = inner.ProximalTargetMargin;
            ScanMode = inner.ScanMode;
            SnoutId = inner.SnoutId;
            SnoutPosition = inner.SnoutPosition;
            VirtualSADX = inner.VirtualSADX;
            VirtualSADY = inner.VirtualSADY;
        }

        public Task ApplyParametersAsync(IBeamParameters beamParams) => _service.RunAsync(() => _inner.ApplyParameters(beamParams));
        public Task<ProtonDeliveryTimeStatus> GetDeliveryTimeStatusByRoomIdAsync(string roomId) => _service.RunAsync(() => _inner.GetDeliveryTimeStatusByRoomId(roomId));
        public async Task<IIonBeamParameters> GetEditableParametersAsync()
        {
            return await _service.RunAsync(() => 
                _inner.GetEditableParameters() is var result && result is null ? null : new AsyncIonBeamParameters(result, _service));
        }

        public Task<double> GetProtonDeliveryTimeByRoomIdAsNumberAsync(string roomId) => _service.RunAsync(() => _inner.GetProtonDeliveryTimeByRoomIdAsNumber(roomId));
        public double AirGap { get; }
        public ProtonBeamLineStatus BeamLineStatus { get; }
        public double DistalTargetMargin { get; private set; }
        public async Task SetDistalTargetMarginAsync(double value)
        {
            DistalTargetMargin = await _service.RunAsync(() =>
            {
                _inner.DistalTargetMargin = value;
                return _inner.DistalTargetMargin;
            });
        }
        public async Task<IReadOnlyList<double>> GetLateralMarginsAsync()
        {
            return await _service.RunAsync(() => _inner.LateralMargins?.ToList());
        }

        public async Task<IReadOnlyList<ILateralSpreadingDevice>> GetLateralSpreadingDevicesAsync()
        {
            return await _service.RunAsync(() => 
                _inner.LateralSpreadingDevices?.Select(x => new AsyncLateralSpreadingDevice(x, _service)).ToList());
        }

        public double NominalRange { get; }
        public double NominalSOBPWidth { get; }
        public string OptionId { get; }
        public string PatientSupportId { get; }
        public PatientSupportType PatientSupportType { get; }
        public async Task<IIonControlPointCollection> GetIonControlPointsAsync()
        {
            return await _service.RunAsync(() => 
                _inner.IonControlPoints is null ? null : new AsyncIonControlPointCollection(_inner.IonControlPoints, _service));
        }
        public double ProximalTargetMargin { get; private set; }
        public async Task SetProximalTargetMarginAsync(double value)
        {
            ProximalTargetMargin = await _service.RunAsync(() =>
            {
                _inner.ProximalTargetMargin = value;
                return _inner.ProximalTargetMargin;
            });
        }
        public async Task<IReadOnlyList<IRangeModulator>> GetRangeModulatorsAsync()
        {
            return await _service.RunAsync(() => 
                _inner.RangeModulators?.Select(x => new AsyncRangeModulator(x, _service)).ToList());
        }

        public async Task<IReadOnlyList<IRangeShifter>> GetRangeShiftersAsync()
        {
            return await _service.RunAsync(() => 
                _inner.RangeShifters?.Select(x => new AsyncRangeShifter(x, _service)).ToList());
        }

        public IonBeamScanMode ScanMode { get; }
        public string SnoutId { get; }
        public double SnoutPosition { get; }
        public async Task<IStructure> GetTargetStructureAsync()
        {
            return await _service.RunAsync(() => 
                _inner.TargetStructure is null ? null : new AsyncStructure(_inner.TargetStructure, _service));
        }
        public double VirtualSADX { get; }
        public double VirtualSADY { get; }

        public Task RunAsync(Action<VMS.TPS.Common.Model.API.IonBeam> action) => _service.RunAsync(() => action(_inner));
        public Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.IonBeam, T> func) => _service.RunAsync(() => func(_inner));
    }
}
    }
}
