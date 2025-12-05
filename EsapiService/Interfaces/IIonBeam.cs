namespace VMS.TPS.Common.Model.API
{
    public interface IIonBeam : IBeam
    {
        void WriteXml(System.Xml.XmlWriter writer);
        void ApplyParameters(VMS.TPS.Common.Model.API.BeamParameters beamParams);
        VMS.TPS.Common.Model.Types.ProtonDeliveryTimeStatus GetDeliveryTimeStatusByRoomId(string roomId);
        IIonBeamParameters GetEditableParameters();
        double GetProtonDeliveryTimeByRoomIdAsNumber(string roomId);
        double AirGap { get; }
        VMS.TPS.Common.Model.Types.ProtonBeamLineStatus BeamLineStatus { get; }
        double DistalTargetMargin { get; }
        System.Threading.Tasks.Task SetDistalTargetMarginAsync(double value);
        System.Collections.Generic.IReadOnlyList<double> LateralMargins { get; }
        System.Collections.Generic.IReadOnlyList<ILateralSpreadingDevice> LateralSpreadingDevices { get; }
        double NominalRange { get; }
        double NominalSOBPWidth { get; }
        string OptionId { get; }
        string PatientSupportId { get; }
        VMS.TPS.Common.Model.Types.PatientSupportType PatientSupportType { get; }
        IIonControlPointCollection IonControlPoints { get; }
        double ProximalTargetMargin { get; }
        System.Threading.Tasks.Task SetProximalTargetMarginAsync(double value);
        System.Collections.Generic.IReadOnlyList<IRangeModulator> RangeModulators { get; }
        System.Collections.Generic.IReadOnlyList<IRangeShifter> RangeShifters { get; }
        VMS.TPS.Common.Model.Types.IonBeamScanMode ScanMode { get; }
        string SnoutId { get; }
        double SnoutPosition { get; }
        IStructure TargetStructure { get; }
        double VirtualSADX { get; }
        double VirtualSADY { get; }
    }
}
