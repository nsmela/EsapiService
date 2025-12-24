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

            DistanceFromTargetBorderInMM = inner.DistanceFromTargetBorderInMM;
            EndDosePercentage = inner.EndDosePercentage;
            FallOff = inner.FallOff;
            IsAutomatic = inner.IsAutomatic;
            IsAutomaticSbrt = inner.IsAutomaticSbrt;
            IsAutomaticSrs = inner.IsAutomaticSrs;
            Priority = inner.Priority;
            StartDosePercentage = inner.StartDosePercentage;
        }


        public double DistanceFromTargetBorderInMM { get; private set; }


        public double EndDosePercentage { get; private set; }


        public double FallOff { get; private set; }


        public bool IsAutomatic { get; private set; }


        public bool IsAutomaticSbrt { get; private set; }


        public bool IsAutomaticSrs { get; private set; }


        public double Priority { get; private set; }


        public double StartDosePercentage { get; private set; }


        public Task RunAsync(Action<VMS.TPS.Common.Model.API.OptimizationNormalTissueParameter> action) => _service.PostAsync((context) => action(_inner));
        public Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.OptimizationNormalTissueParameter, T> func) => _service.PostAsync<T>((context) => func(_inner));

        // updates simple properties that might have changed
        public new void Refresh()
        {
            base.Refresh();

            DistanceFromTargetBorderInMM = _inner.DistanceFromTargetBorderInMM;
            EndDosePercentage = _inner.EndDosePercentage;
            FallOff = _inner.FallOff;
            IsAutomatic = _inner.IsAutomatic;
            IsAutomaticSbrt = _inner.IsAutomaticSbrt;
            IsAutomaticSrs = _inner.IsAutomaticSrs;
            Priority = _inner.Priority;
            StartDosePercentage = _inner.StartDosePercentage;
        }

        public static implicit operator VMS.TPS.Common.Model.API.OptimizationNormalTissueParameter(AsyncOptimizationNormalTissueParameter wrapper) => wrapper._inner;

        // Internal Explicit Implementation to expose _inner safely for covariance
        VMS.TPS.Common.Model.API.OptimizationNormalTissueParameter IEsapiWrapper<VMS.TPS.Common.Model.API.OptimizationNormalTissueParameter>.Inner => _inner;

        // Explicit or Implicit implementation of Service
        // Since _service is private, we expose it via the interface
        IEsapiService IEsapiWrapper<VMS.TPS.Common.Model.API.OptimizationNormalTissueParameter>.Service => _service;
    }
}
