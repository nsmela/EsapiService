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
        Task WriteXmlAsync(System.Xml.XmlWriter writer);
        Task ApplyParametersAsync(VMS.TPS.Common.Model.API.BeamParameters beamParams);
        Task<VMS.TPS.Common.Model.Types.ProtonDeliveryTimeStatus> GetDeliveryTimeStatusByRoomIdAsync(string roomId);
        Task<IIonBeamParameters> GetEditableParametersAsync();
        Task<double> GetProtonDeliveryTimeByRoomIdAsNumberAsync(string roomId);
        double AirGap { get; }
        VMS.TPS.Common.Model.Types.ProtonBeamLineStatus BeamLineStatus { get; }
        double DistalTargetMargin { get; }
        Task SetDistalTargetMarginAsync(double value);
        System.Collections.Generic.IReadOnlyList<double> LateralMargins { get; }
        System.Collections.Generic.IReadOnlyList<ILateralSpreadingDevice> LateralSpreadingDevices { get; }
        double NominalRange { get; }
        double NominalSOBPWidth { get; }
        string OptionId { get; }
        string PatientSupportId { get; }
        VMS.TPS.Common.Model.Types.PatientSupportType PatientSupportType { get; }
        Task<IIonControlPointCollection> GetIonControlPointsAsync();
        double ProximalTargetMargin { get; }
        Task SetProximalTargetMarginAsync(double value);
        System.Collections.Generic.IReadOnlyList<IRangeModulator> RangeModulators { get; }
        System.Collections.Generic.IReadOnlyList<IRangeShifter> RangeShifters { get; }
        VMS.TPS.Common.Model.Types.IonBeamScanMode ScanMode { get; }
        string SnoutId { get; }
        double SnoutPosition { get; }
        Task<IStructure> GetTargetStructureAsync();
        double VirtualSADX { get; }
        double VirtualSADY { get; }

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
