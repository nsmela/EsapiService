using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Media;
using VMS.TPS.Common.Model.API;
using VMS.TPS.Common.Model.Types;
using Esapi.Services;

namespace Esapi.Interfaces
{
    public interface IIonBeam : IBeam
    {
        // --- Simple Properties --- //
        double AirGap { get; }
        ProtonBeamLineStatus BeamLineStatus { get; }
        double DistalTargetMargin { get; }
        Task SetDistalTargetMarginAsync(double value);
        VRect<double> LateralMargins { get; }
        Task SetLateralMarginsAsync(VRect<double> value);
        IEnumerable<LateralSpreadingDevice> LateralSpreadingDevices { get; }
        double NominalRange { get; }
        double NominalSOBPWidth { get; }
        string OptionId { get; }
        string PatientSupportId { get; }
        PatientSupportType PatientSupportType { get; }
        double ProximalTargetMargin { get; }
        Task SetProximalTargetMarginAsync(double value);
        IEnumerable<RangeModulator> RangeModulators { get; }
        IEnumerable<RangeShifter> RangeShifters { get; }
        IonBeamScanMode ScanMode { get; }
        string SnoutId { get; }
        double SnoutPosition { get; }
        double VirtualSADX { get; }
        double VirtualSADY { get; }

        // --- Accessors --- //
        Task<IIonControlPointCollection> GetIonControlPointsAsync(); // read complex property
        Task<IStructure> GetTargetStructureAsync(); // read complex property

        // --- Methods --- //
        Task<ProtonDeliveryTimeStatus> GetDeliveryTimeStatusByRoomIdAsync(string roomId); // simple method
        Task<double> GetProtonDeliveryTimeByRoomIdAsNumberAsync(string roomId); // simple method

        // --- RunAsync --- //
        /// <summary>
        /// Runs a function against the raw ESAPI VMS.TPS.Common.Model.API.IonBeam object safely on the ESAPI thread.
        /// </summary>
        Task RunAsync(Action<VMS.TPS.Common.Model.API.IonBeam> action);

        /// <summary>
        /// Runs a function against the raw ESAPI VMS.TPS.Common.Model.API.IonBeam object safely on the ESAPI thread.
        /// </summary>
        Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.IonBeam, T> func);

        /* --- Skipped Members (Not generated) ---
           - ApplyParameters: Shadows base member in wrapped base class
           - GetEditableParameters: Shadows base member in wrapped base class
        */
    }
}
