using System.Threading.Tasks;
using VMS.TPS.Common.Model.API;
using VMS.TPS.Common.Model.Types;

namespace EsapiService.Wrappers
{
    public class AsyncDVHData : IDVHData
    {
        internal readonly VMS.TPS.Common.Model.API.DVHData _inner;

        // Store the inner ESAPI object reference
        // internal so other wrappers can access it
        // new to override any inherited _inner fields
        internal new readonly IEsapiService _service;

        public AsyncDVHData(VMS.TPS.Common.Model.API.DVHData inner, IEsapiService service) : base(inner, service)
        {
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

        public Task RunAsync(Action<VMS.TPS.Common.Model.API.DVHData> action) => _service.RunAsync(() => action(_inner));
        public Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.DVHData, T> func) => _service.RunAsync(() => func(_inner));
    }
}
    }
}
