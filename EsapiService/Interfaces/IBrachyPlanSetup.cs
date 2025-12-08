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
    public interface IBrachyPlanSetup : IPlanSetup
    {
        // --- Simple Properties --- //
        string ApplicationSetupType { get; }
        BrachyTreatmentTechniqueType BrachyTreatmentTechnique { get; }
        Task SetBrachyTreatmentTechniqueAsync(BrachyTreatmentTechniqueType value);
        int? NumberOfPdrPulses { get; }
        double? PdrPulseInterval { get; }
        string TreatmentTechnique { get; }
        DateTime? TreatmentDateTime { get; }
        Task SetTreatmentDateTimeAsync(DateTime? value);

        // --- Collections --- //
        Task<IReadOnlyList<ICatheter>> GetCathetersAsync();
        Task<IReadOnlyList<IStructure>> GetReferenceLinesAsync();
        Task<IReadOnlyList<ISeedCollection>> GetSeedCollectionsAsync();
        Task<IReadOnlyList<IBrachySolidApplicator>> GetSolidApplicatorsAsync();

        // --- Methods --- //
        Task<ICatheter> AddCatheterAsync(string catheterId, IBrachyTreatmentUnit treatmentUnit, System.Text.StringBuilder outputDiagnostics, bool appendChannelNumToId, int channelNum);
        Task AddLocationToExistingReferencePointAsync(VVector location, IReferencePoint referencePoint);
        Task<IReferencePoint> AddReferencePointAsync(bool target, string id);
        Task<DoseProfile> CalculateAccurateTG43DoseProfileAsync(VVector start, VVector stop, double[] preallocatedBuffer);
        Task<(ChangeBrachyTreatmentUnitResult Result, List<string> messages)> ChangeTreatmentUnitAsync(IBrachyTreatmentUnit treatmentUnit, bool keepDoseIntact);
        Task<ICalculateBrachy3DDoseResult> CalculateTG43DoseAsync();

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
