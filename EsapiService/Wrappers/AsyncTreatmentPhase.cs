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
    public class AsyncTreatmentPhase : AsyncApiDataObject, ITreatmentPhase
    {
        internal new readonly VMS.TPS.Common.Model.API.TreatmentPhase _inner;

        // Store the inner ESAPI object reference
        // internal so other wrappers can access it
        // new to override any inherited _inner fields
        internal new readonly IEsapiService _service;

        public AsyncTreatmentPhase(VMS.TPS.Common.Model.API.TreatmentPhase inner, IEsapiService service) : base(inner, service)
        {
            _inner = inner;
            _service = service;

            OtherInfo = inner.OtherInfo;
            PhaseGapNumberOfDays = inner.PhaseGapNumberOfDays;
            TimeGapType = inner.TimeGapType;
        }


        public string OtherInfo { get; }

        public int PhaseGapNumberOfDays { get; }

        public async Task<IReadOnlyList<IRTPrescription>> GetPrescriptionsAsync()
        {
            return await _service.PostAsync(context => 
                _inner.Prescriptions?.Select(x => new AsyncRTPrescription(x, _service)).ToList());
        }


        public string TimeGapType { get; }

        public Task RunAsync(Action<VMS.TPS.Common.Model.API.TreatmentPhase> action) => _service.PostAsync((context) => action(_inner));
        public Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.TreatmentPhase, T> func) => _service.PostAsync<T>((context) => func(_inner));
    }
}
