using System;
using System.Collections.Generic;
using System.Linq;
using VMS.TPS.Common.Model.Types;

namespace VMS.TPS.Common.Model.API
{
    public class IonControlPointCollection : SerializableObject
    {
        public IonControlPointCollection()
        {
        }

        // -- Collection Simulation --
        public List<IonControlPoint> Items { get; set; } = new List<IonControlPoint>();

        public IEnumerator<IonControlPoint> GetEnumerator() => Items.GetEnumerator();
        public IonControlPoint this[int index] { get => Items[index]; set => Items[index] = value; }
        public int Count => Items.Count;
    }
}
