namespace VMS.TPS.Common.Model.API
{
    public interface IIonControlPoint : IControlPoint
    {
        void WriteXml(System.Xml.XmlWriter writer);
        IIonSpotCollection FinalSpotList { get; }
        System.Collections.Generic.IReadOnlyList<ILateralSpreadingDeviceSettings> LateralSpreadingDeviceSettings { get; }
        double NominalBeamEnergy { get; }
        int NumberOfPaintings { get; }
        System.Collections.Generic.IReadOnlyList<IRangeModulatorSettings> RangeModulatorSettings { get; }
        System.Collections.Generic.IReadOnlyList<IRangeShifterSettings> RangeShifterSettings { get; }
        IIonSpotCollection RawSpotList { get; }
        double ScanningSpotSizeX { get; }
        double ScanningSpotSizeY { get; }
        string ScanSpotTuneId { get; }
        double SnoutPosition { get; }
    }
}
