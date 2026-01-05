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
    public partial class AsyncProtocolPhasePrescription : AsyncSerializableObject, IProtocolPhasePrescription, IEsapiWrapper<VMS.TPS.Common.Model.API.ProtocolPhasePrescription>
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
        }


        public DoseValue TargetTotalDose =>
            _inner.TargetTotalDose;


        public DoseValue TargetFractionDose =>
            _inner.TargetFractionDose;


        public DoseValue ActualTotalDose =>
            _inner.ActualTotalDose;


        public bool? TargetIsMet =>
            _inner.TargetIsMet;


        public PrescriptionModifier PrescModifier =>
            _inner.PrescModifier;


        public double PrescParameter =>
            _inner.PrescParameter;


        public PrescriptionType PrescType =>
            _inner.PrescType;


        public string StructureId =>
            _inner.StructureId;


        public Task RunAsync(Action<VMS.TPS.Common.Model.API.ProtocolPhasePrescription> action) => _service.PostAsync((context) => action(_inner));
        public Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.ProtocolPhasePrescription, T> func) => _service.PostAsync<T>((context) => func(_inner));

        // --- Validates --- //
        /// <summary>
        /// Verifies is the wrapped ESAPI object isn't null.
        /// </summary>
        public new bool IsValid() => !IsNotValid();

        /// <summary>
        /// Verifies is the wrapped ESAPI object is null.
        /// </summary>
        public new bool IsNotValid() => _inner is null;

        public static implicit operator VMS.TPS.Common.Model.API.ProtocolPhasePrescription(AsyncProtocolPhasePrescription wrapper) => wrapper._inner;

        // Internal Explicit Implementation to expose _inner safely for covariance
        VMS.TPS.Common.Model.API.ProtocolPhasePrescription IEsapiWrapper<VMS.TPS.Common.Model.API.ProtocolPhasePrescription>.Inner => _inner;

        // Explicit or Implicit implementation of Service
        // Since _service is private, we expose it via the interface
        IEsapiService IEsapiWrapper<VMS.TPS.Common.Model.API.ProtocolPhasePrescription>.Service => _service;
    }
}
