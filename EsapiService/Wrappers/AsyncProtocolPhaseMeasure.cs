using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;
using VMS.TPS.Common.Model.API;
using VMS.TPS.Common.Model.Types;
using Esapi.Interfaces;
using Esapi.Services;

namespace Esapi.Wrappers
{
    public class AsyncProtocolPhaseMeasure : AsyncSerializableObject, IProtocolPhaseMeasure
    {
        internal readonly VMS.TPS.Common.Model.API.ProtocolPhaseMeasure _inner;

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
            Modifier = inner.Modifier;
            StructureId = inner.StructureId;
            Type = inner.Type;
            TypeText = inner.TypeText;
        }


        public double TargetValue { get; }

        public double ActualValue { get; }

        public bool? TargetIsMet { get; }

        public MeasureModifier Modifier { get; }

        public string StructureId { get; }

        public MeasureType Type { get; }

        public string TypeText { get; }

        public Task RunAsync(Action<VMS.TPS.Common.Model.API.ProtocolPhaseMeasure> action) => _service.RunAsync(() => action(_inner));
        public Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.ProtocolPhaseMeasure, T> func) => _service.RunAsync(() => func(_inner));
    }
}
