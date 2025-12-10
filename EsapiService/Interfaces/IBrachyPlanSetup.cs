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
        int? NumberOfPdrPulses { get; }
        double? PdrPulseInterval { get; }
        DateTime? TreatmentDateTime { get; }
        Task SetTreatmentDateTimeAsync(DateTime? value);

        // --- Collections --- //
        Task<IReadOnlyList<ICatheter>> GetCathetersAsync(); // collection proeprty context
        Task<IReadOnlyList<IStructure>> GetReferenceLinesAsync(); // collection proeprty context
        Task<IReadOnlyList<ISeedCollection>> GetSeedCollectionsAsync(); // collection proeprty context
        Task<IReadOnlyList<IBrachySolidApplicator>> GetSolidApplicatorsAsync(); // collection proeprty context

        // --- Methods --- //
        Task<ICatheter> AddCatheterAsync(string catheterId, IBrachyTreatmentUnit treatmentUnit, System.Text.StringBuilder outputDiagnostics, bool appendChannelNumToId, int channelNum); // complex method
        Task<IReferencePoint> AddReferencePointAsync(bool target, string id); // complex method
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
    }
}
