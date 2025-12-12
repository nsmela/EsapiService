using System;
using System.Collections.Generic;
using System.Linq;
using VMS.TPS.Common.Model.Types;

namespace VMS.TPS.Common.Model.API
{
    public class SeedCollection : ApiDataObject
    {
        public SeedCollection()
        {
        }

        public IEnumerable<BrachyFieldReferencePoint> BrachyFieldReferencePoints { get; set; }
        public System.Windows.Media.Color Color { get; set; }
        public IEnumerable<SourcePosition> SourcePositions { get; set; }
    }
}
