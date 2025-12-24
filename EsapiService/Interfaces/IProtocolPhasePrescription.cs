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
        // --- Simple Properties --- //
        DoseValue TargetTotalDose { get; } // simple property
        DoseValue TargetFractionDose { get; } // simple property
        DoseValue ActualTotalDose { get; } // simple property
        bool? TargetIsMet { get; } // simple property
        PrescriptionModifier PrescModifier { get; } // simple property
        double PrescParameter { get; } // simple property
        PrescriptionType PrescType { get; } // simple property
        string StructureId { get; } // simple property

        // --- RunAsync --- //
        /// <summary>
        /// Runs a function against the raw ESAPI VMS.TPS.Common.Model.API.ProtocolPhasePrescription object safely on the ESAPI thread.
        /// </summary>
        Task RunAsync(Action<VMS.TPS.Common.Model.API.ProtocolPhasePrescription> action);

        /// <summary>
        /// Runs a function against the raw ESAPI VMS.TPS.Common.Model.API.ProtocolPhasePrescription object safely on the ESAPI thread.
        /// </summary>
        Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.ProtocolPhasePrescription, T> func);

        /// <summary>
        /// Updated the properties from the raw Esapi VMS.TPS.Common.Model.API.ProtocolPhasePrescription object
        /// </summary>
        new void Refresh();
    }
}
