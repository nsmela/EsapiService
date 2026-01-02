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
        double AirGap { get; } // simple property
        ProtonBeamLineStatus BeamLineStatus { get; } // simple property
        double DistalTargetMargin { get; set; } // simple property
        VRect<double> LateralMargins { get; set; } // simple property
        double NominalRange { get; } // simple property
        double NominalSOBPWidth { get; } // simple property
        string OptionId { get; } // simple property
        string PatientSupportId { get; } // simple property
        PatientSupportType PatientSupportType { get; } // simple property
        double ProximalTargetMargin { get; set; } // simple property
        IonBeamScanMode ScanMode { get; } // simple property
        string SnoutId { get; } // simple property
        double SnoutPosition { get; } // simple property
        double VirtualSADX { get; } // simple property
        double VirtualSADY { get; } // simple property

        // --- Accessors --- //
        Task<IIonControlPointCollection> GetIonControlPointsAsync(); // read complex property
        Task<IStructure> GetTargetStructureAsync(); // read complex property

        // --- Collections --- //
        Task<IReadOnlyList<ILateralSpreadingDevice>> GetLateralSpreadingDevicesAsync(); // collection property context
        Task<IReadOnlyList<IRangeModulator>> GetRangeModulatorsAsync(); // collection property context
        Task<IReadOnlyList<IRangeShifter>> GetRangeShiftersAsync(); // collection property context

        // --- Methods --- //
        new Task ApplyParametersAsync(IBeamParameters beamParams); // void method
        Task<ProtonDeliveryTimeStatus> GetDeliveryTimeStatusByRoomIdAsync(string roomId); // simple method
        new Task<IIonBeamParameters> GetEditableParametersAsync(); // complex method
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

        // --- Validates --- //
        /// <summary>
        /// Verifies is the wrapped ESAPI object isn't null.
        /// </summary>
        new bool IsValid();

        /// <summary>
        /// Verifies is the wrapped ESAPI object is null.
        /// </summary>
        new bool IsNotValid();
    }
}
