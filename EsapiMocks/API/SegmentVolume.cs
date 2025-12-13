using System;
using System.Collections.Generic;
using System.Linq;
using VMS.TPS.Common.Model.Types;

namespace VMS.TPS.Common.Model.API
{
    public class SegmentVolume : SerializableObject
    {
        public SegmentVolume()
        {
        }

        public SegmentVolume And(SegmentVolume other) => default;
        public SegmentVolume AsymmetricMargin(AxisAlignedMargins margins) => default;
        public SegmentVolume Margin(double marginInMM) => default;
        public SegmentVolume Not() => default;
        public SegmentVolume Or(SegmentVolume other) => default;
        public SegmentVolume Sub(SegmentVolume other) => default;
        public SegmentVolume Xor(SegmentVolume other) => default;
    }
}
