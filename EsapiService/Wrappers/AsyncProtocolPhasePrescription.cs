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
    public class AsyncProtocolPhasePrescription : AsyncSerializableObject, IProtocolPhasePrescription, IEsapiWrapper<VMS.TPS.Common.Model.API.ProtocolPhasePrescription>
    {
        internal new readonly VMS.TPS.Common.Model.API.ProtocolPhasePrescription _inner;

        // Store the inner ESAPI object reference
        // internal so other wrappers can access it
        // new to override any inherited _inner fields
        internal new readonly IEsapiService _service;

        public AsyncProtocolPhasePrescription(VMS.TPS.Common.Model.API.ProtocolPhasePrescription inner, IEsapiService service) : base(inner, service)
        {
            if (inner is null) throw new ArgumentNullException(nameof(inner));
            if (service is null) throw new ArgumentNullException(nameof(service));

            _inner = inner;
            _service = service;

            TargetTotalDose = inner.TargetTotalDose;
            TargetFractionDose = inner.TargetFractionDose;
            ActualTotalDose = inner.ActualTotalDose;
            TargetIsMet = inner.TargetIsMet;
            PrescModifier = inner.PrescModifier;
            PrescParameter = inner.PrescParameter;
            PrescType = inner.PrescType;
            StructureId = inner.StructureId;
        }


        public DoseValue TargetTotalDose { get; private set; }

        public DoseValue TargetFractionDose { get; private set; }

        public DoseValue ActualTotalDose { get; private set; }

        public bool? TargetIsMet { get; private set; }

        public PrescriptionModifier PrescModifier { get; private set; }

        public double PrescParameter { get; private set; }

        public PrescriptionType PrescType { get; private set; }

        public string StructureId { get; private set; }

        public Task RunAsync(Action<VMS.TPS.Common.Model.API.ProtocolPhasePrescription> action) => _service.PostAsync((context) => action(_inner));
        public Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.ProtocolPhasePrescription, T> func) => _service.PostAsync<T>((context) => func(_inner));

        // updates simple properties that might have changed
        public new void Refresh()
        {
            base.Refresh();

            TargetTotalDose = _inner.TargetTotalDose;
            TargetFractionDose = _inner.TargetFractionDose;
            ActualTotalDose = _inner.ActualTotalDose;
            TargetIsMet = _inner.TargetIsMet;
            PrescModifier = _inner.PrescModifier;
            PrescParameter = _inner.PrescParameter;
            PrescType = _inner.PrescType;
            StructureId = _inner.StructureId;
        }

        public static implicit operator VMS.TPS.Common.Model.API.ProtocolPhasePrescription(AsyncProtocolPhasePrescription wrapper) => wrapper._inner;

        // Internal Explicit Implementation to expose _inner safely for covariance
        VMS.TPS.Common.Model.API.ProtocolPhasePrescription IEsapiWrapper<VMS.TPS.Common.Model.API.ProtocolPhasePrescription>.Inner => _inner;

        // Explicit or Implicit implementation of Service
        // Since _service is private, we expose it via the interface
        IEsapiService IEsapiWrapper<VMS.TPS.Common.Model.API.ProtocolPhasePrescription>.Service => _service;
    }
}
