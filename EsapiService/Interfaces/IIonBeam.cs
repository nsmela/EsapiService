using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Media;
using VMS.TPS.Common.Model.API;
using VMS.TPS.Common.Model.Types;
using Esapi.Services;
using Esapi.Interfaces;

namespace Esapi.Interfaces
{
    public interface IIonBeam : IBeam
    {
        // --- Simple Properties --- //
        double AirGap { get; }
        ProtonBeamLineStatus BeamLineStatus { get; }
        double DistalTargetMargin { get; }
        Task SetDistalTargetMarginAsync(double value);
        double NominalRange { get; }
        double NominalSOBPWidth { get; }
        string OptionId { get; }
        string PatientSupportId { get; }
        PatientSupportType PatientSupportType { get; }
        double ProximalTargetMargin { get; }
        Task SetProximalTargetMarginAsync(double value);
        IonBeamScanMode ScanMode { get; }
        string SnoutId { get; }
        double SnoutPosition { get; }
        double VirtualSADX { get; }
        double VirtualSADY { get; }

        // --- Accessors --- //
        Task<IIonControlPointCollection> GetIonControlPointsAsync();
        Task<IStructure> GetTargetStructureAsync();

        // --- Collections --- //
        IReadOnlyList<double> LateralMargins { get; }
        Task<IReadOnlyList<ILateralSpreadingDevice>> GetLateralSpreadingDevicesAsync();
        Task<IReadOnlyList<IRangeModulator>> GetRangeModulatorsAsync();
        Task<IReadOnlyList<IRangeShifter>> GetRangeShiftersAsync();

        // --- Methods --- //
        Task ApplyParametersAsync(IBeamParameters beamParams);
        Task<ProtonDeliveryTimeStatus> GetDeliveryTimeStatusByRoomIdAsync(string roomId);
        Task<IIonBeamParameters> GetEditableParametersAsync();
        Task<double> GetProtonDeliveryTimeByRoomIdAsNumberAsync(string roomId);

        // --- RunAsync --- //
        /// <summary>
        /// Runs a function against the raw ESAPI VMS.TPS.Common.Model.API.IonBeam object safely on the ESAPI thread.
        /// </summary>
        Task RunAsync(Action<VMS.TPS.Common.Model.API.IonBeam> action);

        /// <summary>
        /// Runs a function against the raw ESAPI VMS.TPS.Common.Model.API.IonBeam object safely on the ESAPI thread.
        /// </summary>
        Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.IonBeam, T> func);
    }
}
