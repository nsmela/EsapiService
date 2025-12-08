using System;
using System.Collections.Generic;
using System.Linq;
using VMS.TPS.Common.Model.Types;

namespace VMS.TPS.Common.Model.API
{
    public class ControlPointCollection : SerializableObject
    {
        public ControlPointCollection()
        {
        }

        // -- Collection Simulation --
        public List<ControlPoint> Items { get; set; } = new List<ControlPoint>();

        public IEnumerator<ControlPoint> GetEnumerator() => Items.GetEnumerator();
        public ControlPoint this[int index] { get => Items[index]; set => Items[index] = value; }
        public int Count => Items.Count;
    }
}
