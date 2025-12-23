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


        public double Coverage { get; private set; }

        public DVHPoint[] CurveData { get; private set; }

        public DoseValue MaxDose { get; private set; }

        public VVector MaxDosePosition { get; private set; }

        public DoseValue MeanDose { get; private set; }

        public DoseValue MedianDose { get; private set; }

        public DoseValue MinDose { get; private set; }

        public VVector MinDosePosition { get; private set; }

        public double SamplingCoverage { get; private set; }

        public double StdDev { get; private set; }

        public double Volume { get; private set; }

        public Task RunAsync(Action<VMS.TPS.Common.Model.API.DVHData> action) => _service.PostAsync((context) => action(_inner));
        public Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.DVHData, T> func) => _service.PostAsync<T>((context) => func(_inner));

        // updates simple properties that might have changed
        public new void Refresh()
        {
            base.Refresh();

            Coverage = _inner.Coverage;
            CurveData = _inner.CurveData;
            MaxDose = _inner.MaxDose;
            MaxDosePosition = _inner.MaxDosePosition;
            MeanDose = _inner.MeanDose;
            MedianDose = _inner.MedianDose;
            MinDose = _inner.MinDose;
            MinDosePosition = _inner.MinDosePosition;
            SamplingCoverage = _inner.SamplingCoverage;
            StdDev = _inner.StdDev;
            Volume = _inner.Volume;
        }

        public static implicit operator VMS.TPS.Common.Model.API.DVHData(AsyncDVHData wrapper) => wrapper._inner;

        // Internal Explicit Implementation to expose _inner safely for covariance
        VMS.TPS.Common.Model.API.DVHData IEsapiWrapper<VMS.TPS.Common.Model.API.DVHData>.Inner => _inner;

        // Explicit or Implicit implementation of Service
        // Since _service is private, we expose it via the interface
        IEsapiService IEsapiWrapper<VMS.TPS.Common.Model.API.DVHData>.Service => _service;
    }
}
