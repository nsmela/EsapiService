using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VMS.TPS.Common.Model.API;
using VMS.TPS.Common.Model.Types;
using Esapi.Interfaces;
using Esapi.Services;

namespace Esapi.Wrappers
{
    public class AsyncBrachyPlanSetup : AsyncPlanSetup, IBrachyPlanSetup, IEsapiWrapper<VMS.TPS.Common.Model.API.BrachyPlanSetup>
    {
        internal new readonly VMS.TPS.Common.Model.API.BrachyPlanSetup _inner;

        // Store the inner ESAPI object reference
        // internal so other wrappers can access it
        // new to override any inherited _inner fields
        internal new readonly IEsapiService _service;

public AsyncBrachyPlanSetup(VMS.TPS.Common.Model.API.BrachyPlanSetup inner, IEsapiService service) : base(inner, service)
        {
            if (inner == null) throw new ArgumentNullException(nameof(inner));
            if (service == null) throw new ArgumentNullException(nameof(service));

            _inner = inner;
            _service = service;

            ApplicationSetupType = inner.ApplicationSetupType;
            BrachyTreatmentTechnique = inner.BrachyTreatmentTechnique;
            Catheters = inner.Catheters;
            NumberOfPdrPulses = inner.NumberOfPdrPulses;
            PdrPulseInterval = inner.PdrPulseInterval;
            ReferenceLines = inner.ReferenceLines;
            SeedCollections = inner.SeedCollections;
            SolidApplicators = inner.SolidApplicators;
            TreatmentDateTime = inner.TreatmentDateTime;
        }

        public async Task<ICatheter> AddCatheterAsync(string catheterId, IBrachyTreatmentUnit treatmentUnit, System.Text.StringBuilder outputDiagnostics, bool appendChannelNumToId, int channelNum)
        {
            return await _service.PostAsync(context => 
                _inner.AddCatheter(catheterId, ((AsyncBrachyTreatmentUnit)treatmentUnit)._inner, outputDiagnostics, appendChannelNumToId, channelNum) is var result && result is null ? null : new AsyncCatheter(result, _service));
        }


        // Simple Void Method
        public Task AddLocationToExistingReferencePointAsync(VVector location, IReferencePoint referencePoint) =>
            _service.PostAsync(context => _inner.AddLocationToExistingReferencePoint(location, ((AsyncReferencePoint)referencePoint)._inner));

        // Simple Method
        public Task<DoseProfile> CalculateAccurateTG43DoseProfileAsync(VVector start, VVector stop, double[] preallocatedBuffer) => 
            _service.PostAsync(context => _inner.CalculateAccurateTG43DoseProfile(start, stop, preallocatedBuffer));

        public async Task<(ChangeBrachyTreatmentUnitResult result, List<string> messages)> ChangeTreatmentUnitAsync(IBrachyTreatmentUnit treatmentUnit, bool keepDoseIntact)
        {
            var postResult = await _service.PostAsync(context => {
                List<string> messages_temp = default(List<string>);
                var result = _inner.ChangeTreatmentUnit(((AsyncBrachyTreatmentUnit)treatmentUnit)._inner, keepDoseIntact, out messages_temp);
                return (result, messages_temp);
            });
            return (postResult.Item1,
                    postResult.Item2);
        }


        public async Task<ICalculateBrachy3DDoseResult> CalculateTG43DoseAsync()
        {
            return await _service.PostAsync(context => 
                _inner.CalculateTG43Dose() is var result && result is null ? null : new AsyncCalculateBrachy3DDoseResult(result, _service));
        }


        public string ApplicationSetupType { get; }

        public BrachyTreatmentTechniqueType BrachyTreatmentTechnique { get; private set; }
        public async Task SetBrachyTreatmentTechniqueAsync(BrachyTreatmentTechniqueType value)
        {
            BrachyTreatmentTechnique = await _service.PostAsync(context => 
            {
                _inner.BrachyTreatmentTechnique = value;
                return _inner.BrachyTreatmentTechnique;
            });
        }

        public IEnumerable<Catheter> Catheters { get; }

        public int? NumberOfPdrPulses { get; }

        public double? PdrPulseInterval { get; }

        public IEnumerable<Structure> ReferenceLines { get; }

        public IEnumerable<SeedCollection> SeedCollections { get; }

        public IEnumerable<BrachySolidApplicator> SolidApplicators { get; }

        public DateTime? TreatmentDateTime { get; private set; }
        public async Task SetTreatmentDateTimeAsync(DateTime? value)
        {
            TreatmentDateTime = await _service.PostAsync(context => 
            {
                _inner.TreatmentDateTime = value;
                return _inner.TreatmentDateTime;
            });
        }

        public Task RunAsync(Action<VMS.TPS.Common.Model.API.BrachyPlanSetup> action) => _service.PostAsync((context) => action(_inner));
        public Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.BrachyPlanSetup, T> func) => _service.PostAsync<T>((context) => func(_inner));

        public static implicit operator VMS.TPS.Common.Model.API.BrachyPlanSetup(AsyncBrachyPlanSetup wrapper) => wrapper._inner;

        // Internal Explicit Implementation to expose _inner safely for covariance
        VMS.TPS.Common.Model.API.BrachyPlanSetup IEsapiWrapper<VMS.TPS.Common.Model.API.BrachyPlanSetup>.Inner => _inner;

        /* --- Skipped Members (Not generated) ---
           - AddReferencePoint: Shadows base member in wrapped base class
        */
    }
}
