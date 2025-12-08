using System;
using System.Collections.Generic;
using System.Linq;
using VMS.TPS.Common.Model.Types;

namespace VMS.TPS.Common.Model.API
{
    public class Technique : ApiDataObject
    {
        public Technique()
        {
        }

        public bool IsArc { get; set; }
        public bool IsModulatedScanning { get; set; }
        public bool IsProton { get; set; }
        public bool IsScanning { get; set; }
        public bool IsStatic { get; set; }
    }
}
