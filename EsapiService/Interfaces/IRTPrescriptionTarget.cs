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
    public interface IRTPrescriptionTarget : IApiDataObject
    {
        Task WriteXmlAsync(System.Xml.XmlWriter writer);
        System.Collections.Generic.IReadOnlyList<IRTPrescriptionConstraint> Constraints { get; }
        VMS.TPS.Common.Model.Types.DoseValue DosePerFraction { get; }
        int NumberOfFractions { get; }
        string TargetId { get; }
        VMS.TPS.Common.Model.Types.RTPrescriptionTargetType Type { get; }
        double Value { get; }

        /// <summary>
        /// Runs a function against the raw ESAPI VMS.TPS.Common.Model.API.RTPrescriptionTarget object safely on the ESAPI thread.
        /// </summary>
        Task RunAsync(Action<VMS.TPS.Common.Model.API.RTPrescriptionTarget> action);

        /// <summary>
        /// Runs a function against the raw ESAPI VMS.TPS.Common.Model.API.RTPrescriptionTarget object safely on the ESAPI thread.
        /// </summary>
        Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.RTPrescriptionTarget, T> func);
    }
}
