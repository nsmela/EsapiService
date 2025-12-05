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
    public interface IProtocolPhasePrescription : ISerializableObject
    {
        Task WriteXmlAsync(System.Xml.XmlWriter writer);
        VMS.TPS.Common.Model.Types.DoseValue TargetTotalDose { get; }
        VMS.TPS.Common.Model.Types.DoseValue TargetFractionDose { get; }
        VMS.TPS.Common.Model.Types.DoseValue ActualTotalDose { get; }
        System.Collections.Generic.IReadOnlyList<bool> TargetIsMet { get; }
        VMS.TPS.Common.Model.Types.PrescriptionModifier PrescModifier { get; }
        double PrescParameter { get; }
        VMS.TPS.Common.Model.Types.PrescriptionType PrescType { get; }
        string StructureId { get; }

        /// <summary>
        /// Runs a function against the raw ESAPI VMS.TPS.Common.Model.API.ProtocolPhasePrescription object safely on the ESAPI thread.
        /// </summary>
        Task RunAsync(Action<VMS.TPS.Common.Model.API.ProtocolPhasePrescription> action);

        /// <summary>
        /// Runs a function against the raw ESAPI VMS.TPS.Common.Model.API.ProtocolPhasePrescription object safely on the ESAPI thread.
        /// </summary>
        Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.ProtocolPhasePrescription, T> func);
    }
}
