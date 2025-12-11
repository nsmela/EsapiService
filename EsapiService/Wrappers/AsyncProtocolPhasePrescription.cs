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
            _inner = inner;
            _service = service;

            TargetIsMet = inner.TargetIsMet;
            PrescParameter = inner.PrescParameter;
            StructureId = inner.StructureId;
        }

        public bool? TargetIsMet { get; }

        public double PrescParameter { get; }

        public string StructureId { get; }

        public Task RunAsync(Action<VMS.TPS.Common.Model.API.ProtocolPhasePrescription> action) => _service.PostAsync((context) => action(_inner));
        public Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.ProtocolPhasePrescription, T> func) => _service.PostAsync<T>((context) => func(_inner));

        public static implicit operator VMS.TPS.Common.Model.API.ProtocolPhasePrescription(AsyncProtocolPhasePrescription wrapper) => wrapper._inner;
        // Internal Explicit Implementation to expose _inner safely
        VMS.TPS.Common.Model.API.ProtocolPhasePrescription IEsapiWrapper<VMS.TPS.Common.Model.API.ProtocolPhasePrescription>.Inner => _inner;
    }
}
