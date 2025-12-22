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
    public interface IBrachyPlanSetup : IPlanSetup
    {
        // --- Simple Properties --- //
        string ApplicationSetupType { get; } // simple property
        int? NumberOfPdrPulses { get; } // simple property
        double? PdrPulseInterval { get; } // simple property
        DateTime? TreatmentDateTime { get; } // simple property
        string TreatmentTechnique { get; } // simple property

        // --- Collections --- //
        Task<IReadOnlyList<ICatheter>> GetCathetersAsync(); // collection proeprty context
        Task<IReadOnlyList<ISeedCollection>> GetSeedCollectionsAsync(); // collection proeprty context
        Task<IReadOnlyList<IBrachySolidApplicator>> GetSolidApplicatorsAsync(); // collection proeprty context

        // --- Methods --- //
        Task<DoseProfile> CalculateAccurateTG43DoseProfileAsync(VVector start, VVector stop, double[] preallocatedBuffer); // simple method

        // --- RunAsync --- //
        /// <summary>
        /// Runs a function against the raw ESAPI VMS.TPS.Common.Model.API.BrachyPlanSetup object safely on the ESAPI thread.
        /// </summary>
        Task RunAsync(Action<VMS.TPS.Common.Model.API.BrachyPlanSetup> action);

        /// <summary>
        /// Runs a function against the raw ESAPI VMS.TPS.Common.Model.API.BrachyPlanSetup object safely on the ESAPI thread.
        /// </summary>
        Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.BrachyPlanSetup, T> func);
    }
}
