using System;
using System.Collections.Generic;
using System.Linq;
using VMS.TPS.Common.Model.Types;

namespace VMS.TPS.Common.Model.API
{
    public class IonBeam : Beam
    {
        public IonBeam()
        {
        }

        public void ApplyParameters(BeamParameters beamParams) { }
        public IonBeamParameters GetEditableParameters() => default;
        public double GetProtonDeliveryTimeByRoomIdAsNumber(string roomId) => default;
        public double AirGap { get; set; }
        public double DistalTargetMargin { get; set; }
        public VRect<double> LateralMargins { get; set; }
        public IEnumerable<LateralSpreadingDevice> LateralSpreadingDevices { get; set; }
        public double NominalRange { get; set; }
        public double NominalSOBPWidth { get; set; }
        public string OptionId { get; set; }
        public string PatientSupportId { get; set; }
        public IonControlPointCollection IonControlPoints { get; set; }
        public double ProximalTargetMargin { get; set; }
        public IEnumerable<RangeModulator> RangeModulators { get; set; }
        public IEnumerable<RangeShifter> RangeShifters { get; set; }
        public string SnoutId { get; set; }
        public double SnoutPosition { get; set; }
        public Structure TargetStructure { get; set; }
        public double VirtualSADX { get; set; }
        public double VirtualSADY { get; set; }
    }
}
