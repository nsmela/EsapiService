using System;
using System.Collections.Generic;
using System.Linq;
using VMS.TPS.Common.Model.Types;

namespace VMS.TPS.Common.Model.API
{
    public class IonControlPointPairCollection
    {
        public IonControlPointPairCollection()
        {
        }

        // -- Collection Simulation --
        public List<IonControlPointPair> Items { get; set; } = new List<IonControlPointPair>();

        public IEnumerator<IonControlPointPair> GetEnumerator() => Items.GetEnumerator();
        public IonControlPointPair this[int index] { get => Items[index]; set => Items[index] = value; }
        public int Count => Items.Count;
    }
}
