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
        string ApplicationSetupType { get; }
        BrachyTreatmentTechniqueType BrachyTreatmentTechnique { get; }
        Task SetBrachyTreatmentTechniqueAsync(BrachyTreatmentTechniqueType value);
        IEnumerable<Catheter> Catheters { get; }
        int? NumberOfPdrPulses { get; }
        double? PdrPulseInterval { get; }
        IEnumerable<Structure> ReferenceLines { get; }
        IEnumerable<SeedCollection> SeedCollections { get; }
        IEnumerable<BrachySolidApplicator> SolidApplicators { get; }
        DateTime? TreatmentDateTime { get; }
        Task SetTreatmentDateTimeAsync(DateTime? value);

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

        /* --- Skipped Members (Not generated) ---
           - AddReferencePoint: Shadows base member in wrapped base class
        */
    }
}
