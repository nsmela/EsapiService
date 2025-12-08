using System;
using System.Collections.Generic;
using System.Linq;
using VMS.TPS.Common.Model.Types;

namespace VMS.TPS.Common.Model.API
{
    public class IonSpotCollection : SerializableObject
    {
        public IonSpotCollection()
        {
        }

        // -- Collection Simulation --
        public List<IonSpot> Items { get; set; } = new List<IonSpot>();

        public IEnumerator<IonSpot> GetEnumerator() => Items.GetEnumerator();
        public IonSpot this[int index] { get => Items[index]; set => Items[index] = value; }
        public int Count => Items.Count;
    }
}
