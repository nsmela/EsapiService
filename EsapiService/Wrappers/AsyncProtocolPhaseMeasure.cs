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
    public class AsyncProtocolPhaseMeasure : AsyncSerializableObject, IProtocolPhaseMeasure, IEsapiWrapper<VMS.TPS.Common.Model.API.ProtocolPhaseMeasure>
    {
        internal new readonly VMS.TPS.Common.Model.API.ProtocolPhaseMeasure _inner;

        // Store the inner ESAPI object reference
        // internal so other wrappers can access it
        // new to override any inherited _inner fields
        internal new readonly IEsapiService _service;

public AsyncProtocolPhaseMeasure(VMS.TPS.Common.Model.API.ProtocolPhaseMeasure inner, IEsapiService service) : base(inner, service)
        {
            _inner = inner;
            _service = service;

            TargetValue = inner.TargetValue;
            ActualValue = inner.ActualValue;
            TargetIsMet = inner.TargetIsMet;
            StructureId = inner.StructureId;
            TypeText = inner.TypeText;
        }

        public double TargetValue { get; }

        public double ActualValue { get; }

        public bool? TargetIsMet { get; }

        public string StructureId { get; }

        public string TypeText { get; }

        public Task RunAsync(Action<VMS.TPS.Common.Model.API.ProtocolPhaseMeasure> action) => _service.PostAsync((context) => action(_inner));
        public Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.ProtocolPhaseMeasure, T> func) => _service.PostAsync<T>((context) => func(_inner));

        public static implicit operator VMS.TPS.Common.Model.API.ProtocolPhaseMeasure(AsyncProtocolPhaseMeasure wrapper) => wrapper._inner;
        // Internal Explicit Implementation to expose _inner safely
        VMS.TPS.Common.Model.API.ProtocolPhaseMeasure IEsapiWrapper<VMS.TPS.Common.Model.API.ProtocolPhaseMeasure>.Inner => _inner;
    }
}
