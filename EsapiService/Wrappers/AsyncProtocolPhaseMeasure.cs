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
        }


        public double TargetValue =>
            _inner.TargetValue;


        public double ActualValue =>
            _inner.ActualValue;


        public bool? TargetIsMet =>
            _inner.TargetIsMet;


        public MeasureModifier Modifier =>
            _inner.Modifier;


        public string StructureId =>
            _inner.StructureId;


        public MeasureType Type =>
            _inner.Type;


        public string TypeText =>
            _inner.TypeText;


        public Task RunAsync(Action<VMS.TPS.Common.Model.API.ProtocolPhaseMeasure> action) => _service.PostAsync((context) => action(_inner));
        public Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.ProtocolPhaseMeasure, T> func) => _service.PostAsync<T>((context) => func(_inner));

        // --- Validates --- //
        /// <summary>
        /// Verifies is the wrapped ESAPI object isn't null.
        /// </summary>
        public new bool IsValid() => !IsNotValid();

        /// <summary>
        /// Verifies is the wrapped ESAPI object is null.
        /// </summary>
        public new bool IsNotValid() => _inner is null;

        public static implicit operator VMS.TPS.Common.Model.API.ProtocolPhaseMeasure(AsyncProtocolPhaseMeasure wrapper) => wrapper._inner;

        // Internal Explicit Implementation to expose _inner safely for covariance
        VMS.TPS.Common.Model.API.ProtocolPhaseMeasure IEsapiWrapper<VMS.TPS.Common.Model.API.ProtocolPhaseMeasure>.Inner => _inner;

        // Explicit or Implicit implementation of Service
        // Since _service is private, we expose it via the interface
        IEsapiService IEsapiWrapper<VMS.TPS.Common.Model.API.ProtocolPhaseMeasure>.Service => _service;
    }
}
