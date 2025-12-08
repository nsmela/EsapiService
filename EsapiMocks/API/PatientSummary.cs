using System;
using System.Collections.Generic;
using System.Linq;
using VMS.TPS.Common.Model.Types;

namespace VMS.TPS.Common.Model.API
{
    public class PatientSummary : SerializableObject
    {
        public PatientSummary()
        {
        }

        public DateTime? CreationDateTime { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string FirstName { get; set; }
        public string Id { get; set; }
        public string Id2 { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string Sex { get; set; }
        public string SSN { get; set; }
    }
}
