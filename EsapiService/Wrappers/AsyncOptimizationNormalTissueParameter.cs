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
    public class AsyncOptimizationNormalTissueParameter : AsyncOptimizationParameter, IOptimizationNormalTissueParameter, IEsapiWrapper<VMS.TPS.Common.Model.API.OptimizationNormalTissueParameter>
    {
        internal new readonly VMS.TPS.Common.Model.API.OptimizationNormalTissueParameter _inner;

        // Store the inner ESAPI object reference
        // internal so other wrappers can access it
        // new to override any inherited _inner fields
        internal new readonly IEsapiService _service;

        public AsyncOptimizationNormalTissueParameter(VMS.TPS.Common.Model.API.OptimizationNormalTissueParameter inner, IEsapiService service) : base(inner, service)
        {
            if (inner is null) throw new ArgumentNullException(nameof(inner));
            if (service is null) throw new ArgumentNullException(nameof(service));

            _inner = inner;
            _service = service;
        }


        public double DistanceFromTargetBorderInMM =>
            _inner.DistanceFromTargetBorderInMM;


        public double EndDosePercentage =>
            _inner.EndDosePercentage;


        public double FallOff =>
            _inner.FallOff;


        public bool IsAutomatic =>
            _inner.IsAutomatic;


        public bool IsAutomaticSbrt =>
            _inner.IsAutomaticSbrt;


        public bool IsAutomaticSrs =>
            _inner.IsAutomaticSrs;


        public double Priority =>
            _inner.Priority;


        public double StartDosePercentage =>
            _inner.StartDosePercentage;


        public Task RunAsync(Action<VMS.TPS.Common.Model.API.OptimizationNormalTissueParameter> action) => _service.PostAsync((context) => action(_inner));
        public Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.OptimizationNormalTissueParameter, T> func) => _service.PostAsync<T>((context) => func(_inner));

        public static implicit operator VMS.TPS.Common.Model.API.OptimizationNormalTissueParameter(AsyncOptimizationNormalTissueParameter wrapper) => wrapper._inner;

        // Internal Explicit Implementation to expose _inner safely for covariance
        VMS.TPS.Common.Model.API.OptimizationNormalTissueParameter IEsapiWrapper<VMS.TPS.Common.Model.API.OptimizationNormalTissueParameter>.Inner => _inner;

        // Explicit or Implicit implementation of Service
        // Since _service is private, we expose it via the interface
        IEsapiService IEsapiWrapper<VMS.TPS.Common.Model.API.OptimizationNormalTissueParameter>.Service => _service;
    }
}
