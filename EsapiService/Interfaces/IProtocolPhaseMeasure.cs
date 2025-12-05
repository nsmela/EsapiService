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
    public interface IProtocolPhaseMeasure : ISerializableObject
    {
        Task WriteXmlAsync(System.Xml.XmlWriter writer);
        double TargetValue { get; }
        double ActualValue { get; }
        System.Collections.Generic.IReadOnlyList<bool> TargetIsMet { get; }
        VMS.TPS.Common.Model.Types.MeasureModifier Modifier { get; }
        string StructureId { get; }
        VMS.TPS.Common.Model.Types.MeasureType Type { get; }
        string TypeText { get; }

        /// <summary>
        /// Runs a function against the raw ESAPI VMS.TPS.Common.Model.API.ProtocolPhaseMeasure object safely on the ESAPI thread.
        /// </summary>
        Task RunAsync(Action<VMS.TPS.Common.Model.API.ProtocolPhaseMeasure> action);

        /// <summary>
        /// Runs a function against the raw ESAPI VMS.TPS.Common.Model.API.ProtocolPhaseMeasure object safely on the ESAPI thread.
        /// </summary>
        Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.ProtocolPhaseMeasure, T> func);
    }
}
