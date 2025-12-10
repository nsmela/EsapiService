using System;
using System.Collections.Generic;
using System.Linq;
using VMS.TPS.Common.Model.Types;

namespace VMS.TPS.Common.Model.API
{
    public class ApplicationPackage : ApiDataObject
    {
        public ApplicationPackage()
        {
        }

        public string Description { get; set; }
        public DateTime? ExpirationDate { get; set; }
        public string PackageId { get; set; }
        public string PackageName { get; set; }
        public string PackageVersion { get; set; }
        public string PublisherData { get; set; }
        public string PublisherName { get; set; }
    }
}
