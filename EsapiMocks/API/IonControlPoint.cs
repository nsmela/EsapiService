using System;
using System.Collections.Generic;
using System.Linq;
using VMS.TPS.Common.Model.Types;

namespace VMS.TPS.Common.Model.API
{
    public class IonControlPoint : ControlPoint
    {
        public IonControlPoint()
        {
            LateralSpreadingDeviceSettings = new List<LateralSpreadingDeviceSettings>();
            RangeModulatorSettings = new List<RangeModulatorSettings>();
            RangeShifterSettings = new List<RangeShifterSettings>();
        }

        public IonSpotCollection FinalSpotList { get; set; }
        public IEnumerable<LateralSpreadingDeviceSettings> LateralSpreadingDeviceSettings { get; set; }
        public double NominalBeamEnergy { get; set; }
        public int NumberOfPaintings { get; set; }
        public IEnumerable<RangeModulatorSettings> RangeModulatorSettings { get; set; }
        public IEnumerable<RangeShifterSettings> RangeShifterSettings { get; set; }
        public IonSpotCollection RawSpotList { get; set; }
        public double ScanningSpotSizeX { get; set; }
        public double ScanningSpotSizeY { get; set; }
        public string ScanSpotTuneId { get; set; }
        public double SnoutPosition { get; set; }
    }
}
