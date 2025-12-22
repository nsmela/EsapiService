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
            if (inner is null) throw new ArgumentNullException(nameof(inner));
            if (service is null) throw new ArgumentNullException(nameof(service));

            _inner = inner;
            _service = service;

            ApplicationSetupType = inner.ApplicationSetupType;
            NumberOfPdrPulses = inner.NumberOfPdrPulses;
            PdrPulseInterval = inner.PdrPulseInterval;
            TreatmentDateTime = inner.TreatmentDateTime;
            TreatmentTechnique = inner.TreatmentTechnique;
        }


        // Simple Method
        public Task<DoseProfile> CalculateAccurateTG43DoseProfileAsync(VVector start, VVector stop, double[] preallocatedBuffer) => 
            _service.PostAsync(context => _inner.CalculateAccurateTG43DoseProfile(start, stop, preallocatedBuffer));

        public string ApplicationSetupType { get; }

        public async Task<IReadOnlyList<ICatheter>> GetCathetersAsync()
        {
            return await _service.PostAsync(context => 
                _inner.Catheters?.Select(x => new AsyncCatheter(x, _service)).ToList());
        }


        public int? NumberOfPdrPulses { get; }

        public double? PdrPulseInterval { get; }

        public async Task<IReadOnlyList<ISeedCollection>> GetSeedCollectionsAsync()
        {
            return await _service.PostAsync(context => 
                _inner.SeedCollections?.Select(x => new AsyncSeedCollection(x, _service)).ToList());
        }


        public async Task<IReadOnlyList<IBrachySolidApplicator>> GetSolidApplicatorsAsync()
        {
            return await _service.PostAsync(context => 
                _inner.SolidApplicators?.Select(x => new AsyncBrachySolidApplicator(x, _service)).ToList());
        }


        public DateTime? TreatmentDateTime { get; }

        public string TreatmentTechnique { get; }

        public Task RunAsync(Action<VMS.TPS.Common.Model.API.BrachyPlanSetup> action) => _service.PostAsync((context) => action(_inner));
        public Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.BrachyPlanSetup, T> func) => _service.PostAsync<T>((context) => func(_inner));

        public static implicit operator VMS.TPS.Common.Model.API.BrachyPlanSetup(AsyncBrachyPlanSetup wrapper) => wrapper._inner;

        // Internal Explicit Implementation to expose _inner safely for covariance
        VMS.TPS.Common.Model.API.BrachyPlanSetup IEsapiWrapper<VMS.TPS.Common.Model.API.BrachyPlanSetup>.Inner => _inner;

        // Explicit or Implicit implementation of Service
        // Since _service is private, we expose it via the interface
        IEsapiService IEsapiWrapper<VMS.TPS.Common.Model.API.BrachyPlanSetup>.Service => _service;
    }
}
