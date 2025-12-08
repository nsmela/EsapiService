using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Media;
using VMS.TPS.Common.Model.API;
using VMS.TPS.Common.Model.Types;
using Esapi.Services;
using Esapi.Interfaces;

namespace Esapi.Interfaces
{
    public interface IProtocolPhasePrescription : ISerializableObject
    {
        // --- Simple Properties --- //
        DoseValue TargetTotalDose { get; }
        DoseValue TargetFractionDose { get; }
        DoseValue ActualTotalDose { get; }
        bool? TargetIsMet { get; }
        PrescriptionModifier PrescModifier { get; }
        double PrescParameter { get; }
        PrescriptionType PrescType { get; }
        string StructureId { get; }

        // --- RunAsync --- //
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
