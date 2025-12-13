using System;
using System.Collections.Generic;
using System.Linq;
using VMS.TPS.Common.Model.Types;

namespace VMS.TPS.Common.Model.API
{
    public class BrachyFieldReferencePoint : ApiDataObject
    {
        public BrachyFieldReferencePoint()
        {
        }

        public DoseValue FieldDose { get; set; }
        public bool IsFieldDoseNominal { get; set; }
        public bool IsPrimaryReferencePoint { get; set; }
        public ReferencePoint ReferencePoint { get; set; }
        public VVector RefPointLocation { get; set; }
    }
}
