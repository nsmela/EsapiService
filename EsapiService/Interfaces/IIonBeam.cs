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
        double DistalTargetMargin { get; }
        Task SetDistalTargetMarginAsync(double value);
        double NominalRange { get; }
        double NominalSOBPWidth { get; }
        string OptionId { get; }
        string PatientSupportId { get; }
        double ProximalTargetMargin { get; }
        Task SetProximalTargetMarginAsync(double value);
        string SnoutId { get; }
        double SnoutPosition { get; }
        double VirtualSADX { get; }
        double VirtualSADY { get; }

        // --- Accessors --- //
        Task<IIonControlPointCollection> GetIonControlPointsAsync(); // read complex property
        Task<IStructure> GetTargetStructureAsync(); // read complex property

        // --- Collections --- //
        IReadOnlyList<double> LateralMargins { get; } // simple collection property
        Task<IReadOnlyList<ILateralSpreadingDevice>> GetLateralSpreadingDevicesAsync(); // collection proeprty context
        Task<IReadOnlyList<IRangeModulator>> GetRangeModulatorsAsync(); // collection proeprty context
        Task<IReadOnlyList<IRangeShifter>> GetRangeShiftersAsync(); // collection proeprty context

        // --- Methods --- //
        Task ApplyParametersAsync(IBeamParameters beamParams); // void method
        Task<IIonBeamParameters> GetEditableParametersAsync(); // complex method
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
    }
}
