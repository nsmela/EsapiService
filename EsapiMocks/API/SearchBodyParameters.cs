using System;
using System.Collections.Generic;
using System.Linq;
using VMS.TPS.Common.Model.Types;

namespace VMS.TPS.Common.Model.API
{
    public class SearchBodyParameters : SerializableObject
    {
        public SearchBodyParameters()
        {
        }

        public void LoadDefaults() { }
        public bool FillAllCavities { get; set; }
        public bool KeepLargestParts { get; set; }
        public int LowerHUThreshold { get; set; }
        public int MREdgeThresholdHigh { get; set; }
        public int MREdgeThresholdLow { get; set; }
        public int NumberOfLargestPartsToKeep { get; set; }
        public bool PreCloseOpenings { get; set; }
        public double PreCloseOpeningsRadius { get; set; }
        public bool PreDisconnect { get; set; }
        public double PreDisconnectRadius { get; set; }
        public bool Smoothing { get; set; }
        public int SmoothingLevel { get; set; }
    }
}
