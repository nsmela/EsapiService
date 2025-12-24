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
    public class AsyncDVHData : AsyncSerializableObject, IDVHData, IEsapiWrapper<VMS.TPS.Common.Model.API.DVHData>
    {
        internal new readonly VMS.TPS.Common.Model.API.DVHData _inner;

        // Store the inner ESAPI object reference
        // internal so other wrappers can access it
        // new to override any inherited _inner fields
        internal new readonly IEsapiService _service;

        public AsyncDVHData(VMS.TPS.Common.Model.API.DVHData inner, IEsapiService service) : base(inner, service)
        {
            if (inner is null) throw new ArgumentNullException(nameof(inner));
            if (service is null) throw new ArgumentNullException(nameof(service));

            _inner = inner;
            _service = service;
        }


        public double Coverage =>
            _inner.Coverage;


        public DVHPoint[] CurveData =>
            _inner.CurveData;


        public DoseValue MaxDose =>
            _inner.MaxDose;


        public VVector MaxDosePosition =>
            _inner.MaxDosePosition;


        public DoseValue MeanDose =>
            _inner.MeanDose;


        public DoseValue MedianDose =>
            _inner.MedianDose;


        public DoseValue MinDose =>
            _inner.MinDose;


        public VVector MinDosePosition =>
            _inner.MinDosePosition;


        public double SamplingCoverage =>
            _inner.SamplingCoverage;


        public double StdDev =>
            _inner.StdDev;


        public double Volume =>
            _inner.Volume;


        public Task RunAsync(Action<VMS.TPS.Common.Model.API.DVHData> action) => _service.PostAsync((context) => action(_inner));
        public Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.DVHData, T> func) => _service.PostAsync<T>((context) => func(_inner));

        public static implicit operator VMS.TPS.Common.Model.API.DVHData(AsyncDVHData wrapper) => wrapper._inner;

        // Internal Explicit Implementation to expose _inner safely for covariance
        VMS.TPS.Common.Model.API.DVHData IEsapiWrapper<VMS.TPS.Common.Model.API.DVHData>.Inner => _inner;

        // Explicit or Implicit implementation of Service
        // Since _service is private, we expose it via the interface
        IEsapiService IEsapiWrapper<VMS.TPS.Common.Model.API.DVHData>.Service => _service;
    }
}
