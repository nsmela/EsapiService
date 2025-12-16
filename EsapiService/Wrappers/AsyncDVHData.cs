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
            if (inner == null) throw new ArgumentNullException(nameof(inner));
            if (service == null) throw new ArgumentNullException(nameof(service));

            _inner = inner;
            _service = service;

            Coverage = inner.Coverage;
            CurveData = inner.CurveData;
            MaxDose = inner.MaxDose;
            MaxDosePosition = inner.MaxDosePosition;
            MeanDose = inner.MeanDose;
            MedianDose = inner.MedianDose;
            MinDose = inner.MinDose;
            MinDosePosition = inner.MinDosePosition;
            SamplingCoverage = inner.SamplingCoverage;
            StdDev = inner.StdDev;
            Volume = inner.Volume;
        }

        public double Coverage { get; }

        public DVHPoint[] CurveData { get; }

        public DoseValue MaxDose { get; }

        public VVector MaxDosePosition { get; }

        public DoseValue MeanDose { get; }

        public DoseValue MedianDose { get; }

        public DoseValue MinDose { get; }

        public VVector MinDosePosition { get; }

        public double SamplingCoverage { get; }

        public double StdDev { get; }

        public double Volume { get; }

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
