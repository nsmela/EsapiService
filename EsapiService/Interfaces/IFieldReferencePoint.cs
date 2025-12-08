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
    public interface IFieldReferencePoint : IApiDataObject
    {
        // --- Simple Properties --- //
        double EffectiveDepth { get; }
        DoseValue FieldDose { get; }
        bool IsFieldDoseNominal { get; }
        bool IsPrimaryReferencePoint { get; }
        VVector RefPointLocation { get; }
        double SSD { get; }

        // --- Accessors --- //
        Task<IReferencePoint> GetReferencePointAsync();

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
