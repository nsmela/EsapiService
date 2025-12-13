using System;
using System.Collections.Generic;
using System.Linq;
using VMS.TPS.Common.Model.Types;

namespace VMS.TPS.Common.Model.API
{
    public class PatientSupportDevice : ApiDataObject
    {
        public PatientSupportDevice()
        {
        }

        public string PatientSupportAccessoryCode { get; set; }
        public string PatientSupportDeviceType { get; set; }
    }
}
