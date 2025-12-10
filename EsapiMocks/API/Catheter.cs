using System;
using System.Collections.Generic;
using System.Linq;
using VMS.TPS.Common.Model.Types;

namespace VMS.TPS.Common.Model.API
{
    public class Catheter : ApiDataObject
    {
        public Catheter()
        {
            BrachyFieldReferencePoints = new List<BrachyFieldReferencePoint>();
            SourcePositions = new List<SourcePosition>();
        }

        public double GetSourcePosCenterDistanceFromTip(SourcePosition sourcePosition) => default;
        public double GetTotalDwellTime() => default;
        public void LinkRefLine(Structure refLine) { }
        public void LinkRefPoint(ReferencePoint refPoint) { }
        public bool SetId(string id, out string message)
        {
            message = default;
            return default;
        }

        public void UnlinkRefLine(Structure refLine) { }
        public void UnlinkRefPoint(ReferencePoint refPoint) { }
        public double ApplicatorLength { get; set; }
        public IEnumerable<BrachyFieldReferencePoint> BrachyFieldReferencePoints { get; set; }
        public int BrachySolidApplicatorPartID { get; set; }
        public int ChannelNumber { get; set; }
        public System.Windows.Media.Color Color { get; set; }
        public double DeadSpaceLength { get; set; }
        public double FirstSourcePosition { get; set; }
        public int GroupNumber { get; set; }
        public double LastSourcePosition { get; set; }
        public IEnumerable<SourcePosition> SourcePositions { get; set; }
        public double StepSize { get; set; }
        public BrachyTreatmentUnit TreatmentUnit { get; set; }
    }
}
