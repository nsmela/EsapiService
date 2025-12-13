using System;
using System.Collections.Generic;
using System.Linq;
using VMS.TPS.Common.Model.Types;

namespace VMS.TPS.Common.Model.API
{
    public class Registration : ApiDataObject
    {
        public Registration()
        {
        }

        public VVector InverseTransformPoint(VVector pt) => default;
        public VVector TransformPoint(VVector pt) => default;
        public DateTime? CreationDateTime { get; set; }
        public string RegisteredFOR { get; set; }
        public string SourceFOR { get; set; }
        public RegistrationApprovalStatus Status { get; set; }
        public DateTime? StatusDateTime { get; set; }
        public string StatusUserDisplayName { get; set; }
        public string StatusUserName { get; set; }
        public double[,] TransformationMatrix { get; set; }
        public string UID { get; set; }
    }
}
