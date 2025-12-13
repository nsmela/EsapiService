using System;
using System.Collections.Generic;
using System.Linq;
using VMS.TPS.Common.Model.Types;

namespace VMS.TPS.Common.Model.API
{
    public class ApiDataObject : SerializableObject
    {
        public ApiDataObject()
        {
        }

        public string Id { get; set; }
        public string Name { get; set; }
        public string Comment { get; set; }
        public string HistoryUserName { get; set; }
        public string HistoryUserDisplayName { get; set; }
        public DateTime HistoryDateTime { get; set; }
    }
}
