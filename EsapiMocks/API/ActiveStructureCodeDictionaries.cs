using System;
using System.Collections.Generic;
using System.Linq;
using VMS.TPS.Common.Model.Types;

namespace VMS.TPS.Common.Model.API
{
    public class ActiveStructureCodeDictionaries
    {
        public ActiveStructureCodeDictionaries()
        {
        }

        public StructureCodeDictionary Fma { get; set; }
        public StructureCodeDictionary RadLex { get; set; }
        public StructureCodeDictionary Srt { get; set; }
        public StructureCodeDictionary VmsStructCode { get; set; }

        /* --- Skipped Members (Not generated) ---
           - .ctor: Explicitly ignored by name
        */
    }
}
