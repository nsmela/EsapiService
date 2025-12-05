namespace EsapiService.Wrappers
{
    using System.Linq;
    using System.Collections.Generic;
    public class AsyncPlanUncertainty : IPlanUncertainty
    {
        internal readonly VMS.TPS.Common.Model.API.PlanUncertainty _inner;

        // Store the inner ESAPI object reference
        // internal so other wrappers can access it
        // new to override any inherited _inner fields
        internal new readonly IEsapiService _service;

        public AsyncPlanUncertainty(VMS.TPS.Common.Model.API.PlanUncertainty inner, IEsapiService service) : base(inner, service)
        {
            _inner = inner;
            _service = service;

            CalibrationCurveError = inner.CalibrationCurveError;
            DisplayName = inner.DisplayName;
            IsocenterShift = inner.IsocenterShift;
            UncertaintyType = inner.UncertaintyType;
        }

        public IDVHData GetDVHCumulativeData(IStructure structure, DoseValuePresentation dosePresentation, VolumePresentation volumePresentation, double binWidth) => _inner.GetDVHCumulativeData(structure, dosePresentation, volumePresentation, binWidth) is var result && result is null ? null : new AsyncDVHData(result, _service);
        public IReadOnlyList<IBeamUncertainty> BeamUncertainties => _inner.BeamUncertainties?.Select(x => new AsyncBeamUncertainty(x, _service)).ToList();
        public double CalibrationCurveError { get; }
        public string DisplayName { get; }
        public IDose Dose => _inner.Dose is null ? null : new AsyncDose(_inner.Dose, _service);

        public VVector IsocenterShift { get; }
        public PlanUncertaintyType UncertaintyType { get; }

        public Task RunAsync(Action<VMS.TPS.Common.Model.API.PlanUncertainty> action) => _service.RunAsync(() => action(_inner));
        public Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.PlanUncertainty, T> func) => _service.RunAsync(() => func(_inner));
    }
}
    }
}
