using System;
using System.Collections.Generic;
using System.Linq;
using VMS.TPS.Common.Model.Types;

namespace VMS.TPS.Common.Model.API
{
    public class Slot : ApiDataObject
    {
        public Slot()
        {
        }

        public int Number { get; set; }
    }
}
