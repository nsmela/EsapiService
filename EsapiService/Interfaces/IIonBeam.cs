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
        double DistalTargetMargin { get; } // simple property
        VRect<double> LateralMargins { get; } // simple property
        double NominalRange { get; } // simple property
        double NominalSOBPWidth { get; } // simple property
        string OptionId { get; } // simple property
        string PatientSupportId { get; } // simple property
        PatientSupportType PatientSupportType { get; } // simple property
        double ProximalTargetMargin { get; } // simple property
        IonBeamScanMode ScanMode { get; } // simple property
        string SnoutId { get; } // simple property
        double VirtualSADX { get; } // simple property
        double VirtualSADY { get; } // simple property

        // --- Accessors --- //
        Task<IIonControlPointCollection> GetIonControlPointsAsync(); // read complex property
        Task<IStructure> GetTargetStructureAsync(); // read complex property

        // --- Collections --- //
        Task<IReadOnlyList<ILateralSpreadingDevice>> GetLateralSpreadingDevicesAsync(); // collection proeprty context
        Task<IReadOnlyList<IRangeModulator>> GetRangeModulatorsAsync(); // collection proeprty context
        Task<IReadOnlyList<IRangeShifter>> GetRangeShiftersAsync(); // collection proeprty context

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
           - GetEditableParameters: Shadows base member in wrapped base class
        */
    }
}
