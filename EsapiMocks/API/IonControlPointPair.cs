using System;
using System.Collections.Generic;
using System.Linq;
using VMS.TPS.Common.Model.Types;

namespace VMS.TPS.Common.Model.API
{
    public class IonControlPointPair
    {
        public IonControlPointPair()
        {
        }

        public void ResizeFinalSpotList(int count) { }
        public void ResizeRawSpotList(int count) { }
        public IonControlPointParameters EndControlPoint { get; set; }
        public IonSpotParametersCollection FinalSpotList { get; set; }
        public double NominalBeamEnergy { get; set; }
        public IonSpotParametersCollection RawSpotList { get; set; }
        public IonControlPointParameters StartControlPoint { get; set; }
        public int StartIndex { get; set; }
    }
}
