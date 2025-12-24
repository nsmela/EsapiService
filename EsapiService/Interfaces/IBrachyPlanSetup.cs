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
        BrachyTreatmentTechniqueType BrachyTreatmentTechnique { get; } // simple property
        Task SetBrachyTreatmentTechniqueAsync(BrachyTreatmentTechniqueType value);
        int? NumberOfPdrPulses { get; } // simple property
        double? PdrPulseInterval { get; } // simple property
        DateTime? TreatmentDateTime { get; } // simple property
        Task SetTreatmentDateTimeAsync(DateTime? value);

        // --- Collections --- //
        Task<IReadOnlyList<ICatheter>> GetCathetersAsync(); // collection proeprty context
        Task<IReadOnlyList<IStructure>> GetReferenceLinesAsync(); // collection proeprty context
        Task<IReadOnlyList<ISeedCollection>> GetSeedCollectionsAsync(); // collection proeprty context
        Task<IReadOnlyList<IBrachySolidApplicator>> GetSolidApplicatorsAsync(); // collection proeprty context

        // --- Methods --- //
        Task<ICatheter> AddCatheterAsync(string catheterId, IBrachyTreatmentUnit treatmentUnit, System.Text.StringBuilder outputDiagnostics, bool appendChannelNumToId, int channelNum); // complex method
        Task AddLocationToExistingReferencePointAsync(VVector location, IReferencePoint referencePoint); // void method
        Task<DoseProfile> CalculateAccurateTG43DoseProfileAsync(VVector start, VVector stop, double[] preallocatedBuffer); // simple method
        Task<(ChangeBrachyTreatmentUnitResult result, List<string> messages)> ChangeTreatmentUnitAsync(IBrachyTreatmentUnit treatmentUnit, bool keepDoseIntact); // out/ref parameter method
        Task<ICalculateBrachy3DDoseResult> CalculateTG43DoseAsync(); // complex method

        // --- RunAsync --- //
        /// <summary>
        /// Runs a function against the raw ESAPI VMS.TPS.Common.Model.API.BrachyPlanSetup object safely on the ESAPI thread.
        /// </summary>
        Task RunAsync(Action<VMS.TPS.Common.Model.API.BrachyPlanSetup> action);

        /// <summary>
        /// Runs a function against the raw ESAPI VMS.TPS.Common.Model.API.BrachyPlanSetup object safely on the ESAPI thread.
        /// </summary>
        Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.BrachyPlanSetup, T> func);

        /// <summary>
        /// Updated the properties from the raw Esapi VMS.TPS.Common.Model.API.BrachyPlanSetup object
        /// </summary>
        new void Refresh();

        /* --- Skipped Members (Not generated) ---
           - AddReferencePoint: Shadows base member in wrapped base class
        */
    }
}
