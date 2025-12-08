using System;
using System.Collections.Generic;
using System.Linq;
using VMS.TPS.Common.Model.Types;

namespace VMS.TPS.Common.Model.API
{
    public class IonSpotParametersCollection : SerializableObject
    {
        public IonSpotParametersCollection()
        {
        }

        // -- Collection Simulation --
        public List<IonSpotParameters> Items { get; set; } = new List<IonSpotParameters>();

        public IEnumerator<IonSpotParameters> GetEnumerator() => Items.GetEnumerator();
        public IonSpotParameters this[int index] { get => Items[index]; set => Items[index] = value; }
        public int Count => Items.Count;
    }
}
