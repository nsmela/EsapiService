using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;
using VMS.TPS.Common.Model.API;
using VMS.TPS.Common.Model.Types;
using Esapi.Interfaces;

namespace Esapi.Wrappers
{
    public class AsyncRTPrescriptionTarget : IRTPrescriptionTarget
    {
        internal readonly VMS.TPS.Common.Model.API.RTPrescriptionTarget _inner;

        // Store the inner ESAPI object reference
        // internal so other wrappers can access it
        // new to override any inherited _inner fields
        internal new readonly IEsapiService _service;

        public AsyncRTPrescriptionTarget(VMS.TPS.Common.Model.API.RTPrescriptionTarget inner, IEsapiService service) : base(inner, service)
        {
            _inner = inner;
            _service = service;

            DosePerFraction = inner.DosePerFraction;
            NumberOfFractions = inner.NumberOfFractions;
            TargetId = inner.TargetId;
            Type = inner.Type;
            Value = inner.Value;
        }


        public async Task<IReadOnlyList<IRTPrescriptionConstraint>> GetConstraintsAsync()
        {
            return await _service.RunAsync(() => 
                _inner.Constraints?.Select(x => new AsyncRTPrescriptionConstraint(x, _service)).ToList());
        }


        public DoseValue DosePerFraction { get; }

        public int NumberOfFractions { get; }

        public string TargetId { get; }

        public RTPrescriptionTargetType Type { get; }

        public double Value { get; }

        public Task RunAsync(Action<VMS.TPS.Common.Model.API.RTPrescriptionTarget> action) => _service.RunAsync(() => action(_inner));
        public Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.RTPrescriptionTarget, T> func) => _service.RunAsync(() => func(_inner));
    }
}
