using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;
using VMS.TPS.Common.Model.API;
using VMS.TPS.Common.Model.Types;
using Esapi.Interfaces;
using Esapi.Services;

namespace Esapi.Wrappers
{
    public class AsyncBrachyPlanSetup : AsyncPlanSetup, IBrachyPlanSetup
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
            BrachyTreatmentTechnique = inner.BrachyTreatmentTechnique;
            NumberOfPdrPulses = inner.NumberOfPdrPulses;
            PdrPulseInterval = inner.PdrPulseInterval;
            TreatmentTechnique = inner.TreatmentTechnique;
            TreatmentDateTime = inner.TreatmentDateTime;
        }


        public async Task<ICatheter> AddCatheterAsync(string catheterId, IBrachyTreatmentUnit treatmentUnit, System.Text.StringBuilder outputDiagnostics, bool appendChannelNumToId, int channelNum)
        {
            return await _service.RunAsync(() => 
                _inner.AddCatheter(catheterId, treatmentUnit, outputDiagnostics, appendChannelNumToId, channelNum) is var result && result is null ? null : new AsyncCatheter(result, _service));
        }


        public Task AddLocationToExistingReferencePointAsync(VVector location, IReferencePoint referencePoint) => _service.RunAsync(() => _inner.AddLocationToExistingReferencePoint(location, referencePoint));

        public async Task<IReferencePoint> AddReferencePointAsync(bool target, string id)
        {
            return await _service.RunAsync(() => 
                _inner.AddReferencePoint(target, id) is var result && result is null ? null : new AsyncReferencePoint(result, _service));
        }


        public Task<DoseProfile> CalculateAccurateTG43DoseProfileAsync(VVector start, VVector stop, double[] preallocatedBuffer) => _service.RunAsync(() => _inner.CalculateAccurateTG43DoseProfile(start, stop, preallocatedBuffer));

        public async Task<(ChangeBrachyTreatmentUnitResult Result, List<string> messages)> ChangeTreatmentUnitAsync(IBrachyTreatmentUnit treatmentUnit, bool keepDoseIntact)
        {
            List<string> messages_temp;
            var result = await _service.RunAsync(() => _inner.ChangeTreatmentUnit(treatmentUnit._inner, keepDoseIntact, out messages_temp));
            return (result, messages_temp);
        }

        public async Task<ICalculateBrachy3DDoseResult> CalculateTG43DoseAsync()
        {
            return await _service.RunAsync(() => 
                _inner.CalculateTG43Dose() is var result && result is null ? null : new AsyncCalculateBrachy3DDoseResult(result, _service));
        }


        public string ApplicationSetupType { get; }

        public BrachyTreatmentTechniqueType BrachyTreatmentTechnique { get; private set; }
        public async Task SetBrachyTreatmentTechniqueAsync(BrachyTreatmentTechniqueType value)
        {
            BrachyTreatmentTechnique = await _service.RunAsync(() =>
            {
                _inner.BrachyTreatmentTechnique = value;
                return _inner.BrachyTreatmentTechnique;
            });
        }

        public async Task<IReadOnlyList<ICatheter>> GetCathetersAsync()
        {
            return await _service.RunAsync(() => 
                _inner.Catheters?.Select(x => new AsyncCatheter(x, _service)).ToList());
        }


        public int? NumberOfPdrPulses { get; }

        public double? PdrPulseInterval { get; }

        public async Task<IReadOnlyList<IStructure>> GetReferenceLinesAsync()
        {
            return await _service.RunAsync(() => 
                _inner.ReferenceLines?.Select(x => new AsyncStructure(x, _service)).ToList());
        }


        public async Task<IReadOnlyList<ISeedCollection>> GetSeedCollectionsAsync()
        {
            return await _service.RunAsync(() => 
                _inner.SeedCollections?.Select(x => new AsyncSeedCollection(x, _service)).ToList());
        }


        public async Task<IReadOnlyList<IBrachySolidApplicator>> GetSolidApplicatorsAsync()
        {
            return await _service.RunAsync(() => 
                _inner.SolidApplicators?.Select(x => new AsyncBrachySolidApplicator(x, _service)).ToList());
        }


        public string TreatmentTechnique { get; }

        public DateTime? TreatmentDateTime { get; private set; }
        public async Task SetTreatmentDateTimeAsync(DateTime? value)
        {
            TreatmentDateTime = await _service.RunAsync(() =>
            {
                _inner.TreatmentDateTime = value;
                return _inner.TreatmentDateTime;
            });
        }

        public Task RunAsync(Action<VMS.TPS.Common.Model.API.BrachyPlanSetup> action) => _service.RunAsync(() => action(_inner));
        public Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.BrachyPlanSetup, T> func) => _service.RunAsync(() => func(_inner));
    }
}
