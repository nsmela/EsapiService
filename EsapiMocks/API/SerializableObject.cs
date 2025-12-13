using System;
using System.Collections.Generic;
using System.Linq;
using VMS.TPS.Common.Model.Types;

namespace VMS.TPS.Common.Model.API
{
    public class SerializableObject
    {
        public SerializableObject()
        {
        }

        public System.Xml.Schema.XmlSchema GetSchema() => default;
        public void ReadXml(System.Xml.XmlReader reader) { }
        public void WriteXml(System.Xml.XmlWriter writer) { }
    }
}
