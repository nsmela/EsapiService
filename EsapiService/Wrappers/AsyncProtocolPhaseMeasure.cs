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
            if (inner is null) throw new ArgumentNullException(nameof(inner));
            if (service is null) throw new ArgumentNullException(nameof(service));

            _inner = inner;
            _service = service;

            TargetValue = inner.TargetValue;
            ActualValue = inner.ActualValue;
            TargetIsMet = inner.TargetIsMet;
            Modifier = inner.Modifier;
            StructureId = inner.StructureId;
            Type = inner.Type;
            TypeText = inner.TypeText;
        }


        public double TargetValue { get; private set; }

        public double ActualValue { get; private set; }

        public bool? TargetIsMet { get; private set; }

        public MeasureModifier Modifier { get; private set; }

        public string StructureId { get; private set; }

        public MeasureType Type { get; private set; }

        public string TypeText { get; private set; }

        public Task RunAsync(Action<VMS.TPS.Common.Model.API.ProtocolPhaseMeasure> action) => _service.PostAsync((context) => action(_inner));
        public Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.ProtocolPhaseMeasure, T> func) => _service.PostAsync<T>((context) => func(_inner));

        // updates simple properties that might have changed
        public new void Refresh()
        {
            base.Refresh();

            TargetValue = _inner.TargetValue;
            ActualValue = _inner.ActualValue;
            TargetIsMet = _inner.TargetIsMet;
            Modifier = _inner.Modifier;
            StructureId = _inner.StructureId;
            Type = _inner.Type;
            TypeText = _inner.TypeText;
        }

        public static implicit operator VMS.TPS.Common.Model.API.ProtocolPhaseMeasure(AsyncProtocolPhaseMeasure wrapper) => wrapper._inner;

        // Internal Explicit Implementation to expose _inner safely for covariance
        VMS.TPS.Common.Model.API.ProtocolPhaseMeasure IEsapiWrapper<VMS.TPS.Common.Model.API.ProtocolPhaseMeasure>.Inner => _inner;

        // Explicit or Implicit implementation of Service
        // Since _service is private, we expose it via the interface
        IEsapiService IEsapiWrapper<VMS.TPS.Common.Model.API.ProtocolPhaseMeasure>.Service => _service;
    }
}
