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
    public class AsyncOptimizationObjective : AsyncSerializableObject, IOptimizationObjective, IEsapiWrapper<VMS.TPS.Common.Model.API.OptimizationObjective>
    {
        internal new readonly VMS.TPS.Common.Model.API.OptimizationObjective _inner;

        // Store the inner ESAPI object reference
        // internal so other wrappers can access it
        // new to override any inherited _inner fields
        internal new readonly IEsapiService _service;

public AsyncOptimizationObjective(VMS.TPS.Common.Model.API.OptimizationObjective inner, IEsapiService service) : base(inner, service)
        {
            if (inner is null) throw new ArgumentNullException(nameof(inner));
            if (service is null) throw new ArgumentNullException(nameof(service));

            _inner = inner;
            _service = service;

            Operator = inner.Operator;
            Priority = inner.Priority;
            StructureId = inner.StructureId;
        }

        public OptimizationObjectiveOperator Operator { get; }

        public double Priority { get; }

        public async Task<IStructure> GetStructureAsync()
        {
            return await _service.PostAsync(context => {
                var innerResult = _inner.Structure is null ? null : new AsyncStructure(_inner.Structure, _service);
                return innerResult;
            });
        }

        public string StructureId { get; }

        public Task RunAsync(Action<VMS.TPS.Common.Model.API.OptimizationObjective> action) => _service.PostAsync((context) => action(_inner));
        public Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.OptimizationObjective, T> func) => _service.PostAsync<T>((context) => func(_inner));

        public static implicit operator VMS.TPS.Common.Model.API.OptimizationObjective(AsyncOptimizationObjective wrapper) => wrapper._inner;

        // Internal Explicit Implementation to expose _inner safely for covariance
        VMS.TPS.Common.Model.API.OptimizationObjective IEsapiWrapper<VMS.TPS.Common.Model.API.OptimizationObjective>.Inner => _inner;

        // Explicit or Implicit implementation of Service
        // Since _service is private, we expose it via the interface
        IEsapiService IEsapiWrapper<VMS.TPS.Common.Model.API.OptimizationObjective>.Service => _service;
    }
}
