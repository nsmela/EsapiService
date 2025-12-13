using System;
using System.Collections.Generic;
using System.Linq;
using VMS.TPS.Common.Model.Types;

namespace VMS.TPS.Common.Model.API
{
    public class User : SerializableObject
    {
        public User()
        {
        }

        public string Id { get; set; }
        public bool IsServiceUser { get; set; }
        public string Language { get; set; }
        public string Name { get; set; }
    }
}
