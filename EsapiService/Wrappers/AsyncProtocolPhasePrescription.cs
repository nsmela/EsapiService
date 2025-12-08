using System.Threading.Tasks;
using VMS.TPS.Common.Model.API;
using VMS.TPS.Common.Model.Types;
using Esapi.Interfaces;

namespace Esapi.Wrappers
{
    public class AsyncProtocolPhasePrescription : IProtocolPhasePrescription
    {
        internal readonly VMS.TPS.Common.Model.API.ProtocolPhasePrescription _inner;

        // Store the inner ESAPI object reference
        // internal so other wrappers can access it
        // new to override any inherited _inner fields
        internal new readonly IEsapiService _service;

        public AsyncProtocolPhasePrescription(VMS.TPS.Common.Model.API.ProtocolPhasePrescription inner, IEsapiService service) : base(inner, service)
        {
            _inner = inner;
            _service = service;

            TargetTotalDose = inner.TargetTotalDose;
            TargetFractionDose = inner.TargetFractionDose;
            ActualTotalDose = inner.ActualTotalDose;
            PrescModifier = inner.PrescModifier;
            PrescParameter = inner.PrescParameter;
            PrescType = inner.PrescType;
            StructureId = inner.StructureId;
        }


        public DoseValue TargetTotalDose { get; }

        public DoseValue TargetFractionDose { get; }

        public DoseValue ActualTotalDose { get; }

        public async Task<IReadOnlyList<bool>> GetTargetIsMetAsync()
        {
            return await _service.RunAsync(() => _inner.TargetIsMet?.ToList());
        }


        public PrescriptionModifier PrescModifier { get; }

        public double PrescParameter { get; }

        public PrescriptionType PrescType { get; }

        public string StructureId { get; }

        public Task RunAsync(Action<VMS.TPS.Common.Model.API.ProtocolPhasePrescription> action) => _service.RunAsync(() => action(_inner));
        public Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.ProtocolPhasePrescription, T> func) => _service.RunAsync(() => func(_inner));
    }
}
