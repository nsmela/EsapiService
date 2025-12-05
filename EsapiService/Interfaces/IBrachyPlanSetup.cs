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
        Task WriteXmlAsync(System.Xml.XmlWriter writer);
        Task<ICatheter> AddCatheterAsync(string catheterId, VMS.TPS.Common.Model.API.BrachyTreatmentUnit treatmentUnit, System.Text.StringBuilder outputDiagnostics, bool appendChannelNumToId, int channelNum);
        Task AddLocationToExistingReferencePointAsync(VMS.TPS.Common.Model.Types.VVector location, VMS.TPS.Common.Model.API.ReferencePoint referencePoint);
        Task<IReferencePoint> AddReferencePointAsync(bool target, string id);
        Task<VMS.TPS.Common.Model.Types.DoseProfile> CalculateAccurateTG43DoseProfileAsync(VMS.TPS.Common.Model.Types.VVector start, VMS.TPS.Common.Model.Types.VVector stop, double[] preallocatedBuffer);
        Task<(VMS.TPS.Common.Model.Types.ChangeBrachyTreatmentUnitResult Result, System.Collections.Generic.List<string> messages)> ChangeTreatmentUnitAsync(IBrachyTreatmentUnit treatmentUnit, bool keepDoseIntact);
        Task<ICalculateBrachy3DDoseResult> CalculateTG43DoseAsync();
        string ApplicationSetupType { get; }
        VMS.TPS.Common.Model.Types.BrachyTreatmentTechniqueType BrachyTreatmentTechnique { get; }
        Task SetBrachyTreatmentTechniqueAsync(VMS.TPS.Common.Model.Types.BrachyTreatmentTechniqueType value);
        System.Collections.Generic.IReadOnlyList<ICatheter> Catheters { get; }
        System.Collections.Generic.IReadOnlyList<int> NumberOfPdrPulses { get; }
        System.Collections.Generic.IReadOnlyList<double> PdrPulseInterval { get; }
        System.Collections.Generic.IReadOnlyList<IStructure> ReferenceLines { get; }
        System.Collections.Generic.IReadOnlyList<ISeedCollection> SeedCollections { get; }
        System.Collections.Generic.IReadOnlyList<IBrachySolidApplicator> SolidApplicators { get; }
        string TreatmentTechnique { get; }
        System.Collections.Generic.IReadOnlyList<System.DateTime> TreatmentDateTime { get; }

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
