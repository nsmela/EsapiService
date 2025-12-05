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
    public interface IRTPrescriptionTargetConstraints : ISerializableObject
    {
        Task WriteXmlAsync(System.Xml.XmlWriter writer);
        System.Collections.Generic.IReadOnlyList<IRTPrescriptionConstraint> Constraints { get; }
        string TargetId { get; }

        /// <summary>
        /// Runs a function against the raw ESAPI VMS.TPS.Common.Model.API.RTPrescriptionTargetConstraints object safely on the ESAPI thread.
        /// </summary>
        Task RunAsync(Action<VMS.TPS.Common.Model.API.RTPrescriptionTargetConstraints> action);

        /// <summary>
        /// Runs a function against the raw ESAPI VMS.TPS.Common.Model.API.RTPrescriptionTargetConstraints object safely on the ESAPI thread.
        /// </summary>
        Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.RTPrescriptionTargetConstraints, T> func);
    }
}
