    using System.Threading.Tasks;
namespace EsapiService.Wrappers
{
    using System.Linq;
    using System.Collections.Generic;
    public class AsyncBrachyPlanSetup : IBrachyPlanSetup
    {
        internal readonly VMS.TPS.Common.Model.API.BrachyPlanSetup _inner;

        // Store the inner ESAPI object reference
        // internal so other wrappers can access it
        // new to override any inherited _inner fields
        internal new readonly IEsapiService _service;

        public AsyncBrachyPlanSetup(VMS.TPS.Common.Model.API.BrachyPlanSetup inner, IEsapiService service) : base(inner, service)
        {
            _inner = inner;
            _service = service;

            ApplicationSetupType = inner.ApplicationSetupType;
            TreatmentTechnique = inner.TreatmentTechnique;
        }

        public ICatheter AddCatheter(string catheterId, IBrachyTreatmentUnit treatmentUnit, Text.StringBuilder outputDiagnostics, bool appendChannelNumToId, int channelNum) => _inner.AddCatheter(catheterId, treatmentUnit, outputDiagnostics, appendChannelNumToId, channelNum) is var result && result is null ? null : new AsyncCatheter(result, _service);
        public void AddLocationToExistingReferencePoint(VVector location, IReferencePoint referencePoint) => _inner.AddLocationToExistingReferencePoint(location, referencePoint);
        public IReferencePoint AddReferencePoint(bool target, string id) => _inner.AddReferencePoint(target, id) is var result && result is null ? null : new AsyncReferencePoint(result, _service);
        public DoseProfile CalculateAccurateTG43DoseProfile(VVector start, VVector stop, double[] preallocatedBuffer) => _inner.CalculateAccurateTG43DoseProfile(start, stop, preallocatedBuffer);
        public async System.Threading.Tasks.Task<(ChangeBrachyTreatmentUnitResult Result, List<string> messages)> ChangeTreatmentUnitAsync(IBrachyTreatmentUnit treatmentUnit, bool keepDoseIntact)
        {
            List<string> messages_temp;
            var result = await _service.RunAsync(() => _inner.ChangeTreatmentUnit(treatmentUnit._inner, keepDoseIntact, out messages_temp));
            return (result, messages_temp);
        }
        public ICalculateBrachy3DDoseResult CalculateTG43Dose() => _inner.CalculateTG43Dose() is var result && result is null ? null : new AsyncCalculateBrachy3DDoseResult(result, _service);
        public string ApplicationSetupType { get; }
        public BrachyTreatmentTechniqueType BrachyTreatmentTechnique => _inner.BrachyTreatmentTechnique;
        public async Task SetBrachyTreatmentTechniqueAsync(BrachyTreatmentTechniqueType value) => _service.RunAsync(() => _inner.BrachyTreatmentTechnique = value);
        public IReadOnlyList<ICatheter> Catheters => _inner.Catheters?.Select(x => new AsyncCatheter(x, _service)).ToList();
        public IReadOnlyList<int> NumberOfPdrPulses => _inner.NumberOfPdrPulses?.ToList();
        public IReadOnlyList<double> PdrPulseInterval => _inner.PdrPulseInterval?.ToList();
        public IReadOnlyList<IStructure> ReferenceLines => _inner.ReferenceLines?.Select(x => new AsyncStructure(x, _service)).ToList();
        public IReadOnlyList<ISeedCollection> SeedCollections => _inner.SeedCollections?.Select(x => new AsyncSeedCollection(x, _service)).ToList();
        public IReadOnlyList<IBrachySolidApplicator> SolidApplicators => _inner.SolidApplicators?.Select(x => new AsyncBrachySolidApplicator(x, _service)).ToList();
        public string TreatmentTechnique { get; }
        public IReadOnlyList<DateTime> TreatmentDateTime => _inner.TreatmentDateTime?.ToList();

        public Task RunAsync(Action<VMS.TPS.Common.Model.API.BrachyPlanSetup> action) => _service.RunAsync(() => action(_inner));
        public Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.BrachyPlanSetup, T> func) => _service.RunAsync(() => func(_inner));
    }
}
    }
}
