    using System.Threading.Tasks;
namespace EsapiService.Wrappers
{
    using System.Linq;
    using System.Collections.Generic;
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
            NominalRange = inner.NominalRange;
            NominalSOBPWidth = inner.NominalSOBPWidth;
            OptionId = inner.OptionId;
            PatientSupportId = inner.PatientSupportId;
            PatientSupportType = inner.PatientSupportType;
            ScanMode = inner.ScanMode;
            SnoutId = inner.SnoutId;
            SnoutPosition = inner.SnoutPosition;
            VirtualSADX = inner.VirtualSADX;
            VirtualSADY = inner.VirtualSADY;
        }

        public void ApplyParameters(IBeamParameters beamParams) => _inner.ApplyParameters(beamParams);
        public ProtonDeliveryTimeStatus GetDeliveryTimeStatusByRoomId(string roomId) => _inner.GetDeliveryTimeStatusByRoomId(roomId);
        public IIonBeamParameters GetEditableParameters() => _inner.GetEditableParameters() is var result && result is null ? null : new AsyncIonBeamParameters(result, _service);
        public double GetProtonDeliveryTimeByRoomIdAsNumber(string roomId) => _inner.GetProtonDeliveryTimeByRoomIdAsNumber(roomId);
        public double AirGap { get; }
        public ProtonBeamLineStatus BeamLineStatus { get; }
        public double DistalTargetMargin => _inner.DistalTargetMargin;
        public async Task SetDistalTargetMarginAsync(double value) => _service.RunAsync(() => _inner.DistalTargetMargin = value);
        public IReadOnlyList<double> LateralMargins => _inner.LateralMargins?.ToList();
        public IReadOnlyList<ILateralSpreadingDevice> LateralSpreadingDevices => _inner.LateralSpreadingDevices?.Select(x => new AsyncLateralSpreadingDevice(x, _service)).ToList();
        public double NominalRange { get; }
        public double NominalSOBPWidth { get; }
        public string OptionId { get; }
        public string PatientSupportId { get; }
        public PatientSupportType PatientSupportType { get; }
        public IIonControlPointCollection IonControlPoints => _inner.IonControlPoints is null ? null : new AsyncIonControlPointCollection(_inner.IonControlPoints, _service);

        public double ProximalTargetMargin => _inner.ProximalTargetMargin;
        public async Task SetProximalTargetMarginAsync(double value) => _service.RunAsync(() => _inner.ProximalTargetMargin = value);
        public IReadOnlyList<IRangeModulator> RangeModulators => _inner.RangeModulators?.Select(x => new AsyncRangeModulator(x, _service)).ToList();
        public IReadOnlyList<IRangeShifter> RangeShifters => _inner.RangeShifters?.Select(x => new AsyncRangeShifter(x, _service)).ToList();
        public IonBeamScanMode ScanMode { get; }
        public string SnoutId { get; }
        public double SnoutPosition { get; }
        public IStructure TargetStructure => _inner.TargetStructure is null ? null : new AsyncStructure(_inner.TargetStructure, _service);

        public double VirtualSADX { get; }
        public double VirtualSADY { get; }

        public Task RunAsync(Action<VMS.TPS.Common.Model.API.IonBeam> action) => _service.RunAsync(() => action(_inner));
        public Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.IonBeam, T> func) => _service.RunAsync(() => func(_inner));
    }
}
    }
}
