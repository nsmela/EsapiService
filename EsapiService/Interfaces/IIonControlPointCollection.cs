using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Media;
using VMS.TPS.Common.Model.API;
using VMS.TPS.Common.Model.Types;
using Esapi.Services;

namespace Esapi.Interfaces
{
    public interface IIonControlPointCollection : ISerializableObject
    {
        Task<System.Collections.Generic.IReadOnlyList<IIonControlPoint>> GetEnumeratorAsync();
        Task WriteXmlAsync(System.Xml.XmlWriter writer);
        Task<IIonControlPoint> Getthis[]Async();
        int Count { get; }

        /// <summary>
        /// Runs a function against the raw ESAPI VMS.TPS.Common.Model.API.IonControlPointCollection object safely on the ESAPI thread.
        /// </summary>
        Task RunAsync(Action<VMS.TPS.Common.Model.API.IonControlPointCollection> action);

        /// <summary>
        /// Runs a function against the raw ESAPI VMS.TPS.Common.Model.API.IonControlPointCollection object safely on the ESAPI thread.
        /// </summary>
        Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.IonControlPointCollection, T> func);
    }
}
