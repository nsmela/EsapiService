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

        public void WriteXml(System.Xml.XmlWriter writer) => _inner.WriteXml(writer);
        public IDVHData GetDVHCumulativeData(VMS.TPS.Common.Model.API.Structure structure, VMS.TPS.Common.Model.Types.DoseValuePresentation dosePresentation, VMS.TPS.Common.Model.Types.VolumePresentation volumePresentation, double binWidth) => _inner.GetDVHCumulativeData(structure, dosePresentation, volumePresentation, binWidth) is var result && result is null ? null : new AsyncDVHData(result, _service);
        public System.Collections.Generic.IReadOnlyList<IBeamUncertainty> BeamUncertainties => _inner.BeamUncertainties?.Select(x => new AsyncBeamUncertainty(x, _service)).ToList();
        public double CalibrationCurveError { get; }
        public string DisplayName { get; }
        public IDose Dose => _inner.Dose is null ? null : new AsyncDose(_inner.Dose, _service);

        public VMS.TPS.Common.Model.Types.VVector IsocenterShift { get; }
        public VMS.TPS.Common.Model.Types.PlanUncertaintyType UncertaintyType { get; }

        public Task RunAsync(Action<VMS.TPS.Common.Model.API.PlanUncertainty> action) => _service.RunAsync(() => action(_inner));
        public Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.PlanUncertainty, T> func) => _service.RunAsync(() => func(_inner));
    }
}
    }
}
