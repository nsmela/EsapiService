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
            _inner = inner;
            _service = service;

            ApplicationSetupType = inner.ApplicationSetupType;
            NumberOfPdrPulses = inner.NumberOfPdrPulses;
            PdrPulseInterval = inner.PdrPulseInterval;
            TreatmentDateTime = inner.TreatmentDateTime;
        }

        public async Task<ICatheter> AddCatheterAsync(string catheterId, IBrachyTreatmentUnit treatmentUnit, System.Text.StringBuilder outputDiagnostics, bool appendChannelNumToId, int channelNum)
        {
            return await _service.PostAsync(context => 
                _inner.AddCatheter(catheterId, ((AsyncBrachyTreatmentUnit)treatmentUnit)._inner, outputDiagnostics, appendChannelNumToId, channelNum) is var result && result is null ? null : new AsyncCatheter(result, _service));
        }


        public async Task<IReferencePoint> AddReferencePointAsync(bool target, string id)
        {
            return await _service.PostAsync(context => 
                _inner.AddReferencePoint(target, id) is var result && result is null ? null : new AsyncReferencePoint(result, _service));
        }


        public async Task<ICalculateBrachy3DDoseResult> CalculateTG43DoseAsync()
        {
            return await _service.PostAsync(context => 
                _inner.CalculateTG43Dose() is var result && result is null ? null : new AsyncCalculateBrachy3DDoseResult(result, _service));
        }


        public string ApplicationSetupType { get; }

        public async Task<IReadOnlyList<ICatheter>> GetCathetersAsync()
        {
            return await _service.PostAsync(context => 
                _inner.Catheters?.Select(x => new AsyncCatheter(x, _service)).ToList());
        }


        public int? NumberOfPdrPulses { get; }

        public double? PdrPulseInterval { get; }

        public async Task<IReadOnlyList<IStructure>> GetReferenceLinesAsync()
        {
            return await _service.PostAsync(context => 
                _inner.ReferenceLines?.Select(x => new AsyncStructure(x, _service)).ToList());
        }


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
        // Internal Explicit Implementation to expose _inner safely
        VMS.TPS.Common.Model.API.BrachyPlanSetup IEsapiWrapper<VMS.TPS.Common.Model.API.BrachyPlanSetup>.Inner => _inner;
    }
}
