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
    public interface IFieldReferencePoint : IApiDataObject
    {
        // --- Simple Properties --- //
        double EffectiveDepth { get; } // simple property
        DoseValue FieldDose { get; } // simple property
        bool IsFieldDoseNominal { get; } // simple property
        bool IsPrimaryReferencePoint { get; } // simple property
        VVector RefPointLocation { get; } // simple property
        double SSD { get; } // simple property

        // --- Accessors --- //
        Task<IReferencePoint> GetReferencePointAsync(); // read complex property

        // --- RunAsync --- //
        /// <summary>
        /// Runs a function against the raw ESAPI VMS.TPS.Common.Model.API.FieldReferencePoint object safely on the ESAPI thread.
        /// </summary>
        Task RunAsync(Action<VMS.TPS.Common.Model.API.FieldReferencePoint> action);

        /// <summary>
        /// Runs a function against the raw ESAPI VMS.TPS.Common.Model.API.FieldReferencePoint object safely on the ESAPI thread.
        /// </summary>
        Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.FieldReferencePoint, T> func);
    }
}
