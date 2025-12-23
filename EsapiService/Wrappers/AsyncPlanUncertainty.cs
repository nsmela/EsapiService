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
    public class AsyncPlanUncertainty : AsyncApiDataObject, IPlanUncertainty, IEsapiWrapper<VMS.TPS.Common.Model.API.PlanUncertainty>
    {
        internal new readonly VMS.TPS.Common.Model.API.PlanUncertainty _inner;

        // Store the inner ESAPI object reference
        // internal so other wrappers can access it
        // new to override any inherited _inner fields
        internal new readonly IEsapiService _service;

        public AsyncPlanUncertainty(VMS.TPS.Common.Model.API.PlanUncertainty inner, IEsapiService service) : base(inner, service)
        {
            if (inner is null) throw new ArgumentNullException(nameof(inner));
            if (service is null) throw new ArgumentNullException(nameof(service));

            _inner = inner;
            _service = service;

            CalibrationCurveError = inner.CalibrationCurveError;
            DisplayName = inner.DisplayName;
            IsocenterShift = inner.IsocenterShift;
            UncertaintyType = inner.UncertaintyType;
        }


        public async Task<IDVHData> GetDVHCumulativeDataAsync(IStructure structure, DoseValuePresentation dosePresentation, VolumePresentation volumePresentation, double binWidth)
        {
            return await _service.PostAsync(context => 
                _inner.GetDVHCumulativeData(((AsyncStructure)structure)._inner, dosePresentation, volumePresentation, binWidth) is var result && result is null ? null : new AsyncDVHData(result, _service));
        }


        public async Task<IReadOnlyList<IBeamUncertainty>> GetBeamUncertaintiesAsync()
        {
            return await _service.PostAsync(context => 
                _inner.BeamUncertainties?.Select(x => new AsyncBeamUncertainty(x, _service)).ToList());
        }


        public double CalibrationCurveError { get; }

        public string DisplayName { get; }

        public async Task<IDose> GetDoseAsync()
        {
            return await _service.PostAsync(context => {
                var innerResult = _inner.Dose is null ? null : new AsyncDose(_inner.Dose, _service);
                return innerResult;
            });
        }

        public VVector IsocenterShift { get; }

        public PlanUncertaintyType UncertaintyType { get; }

        public Task RunAsync(Action<VMS.TPS.Common.Model.API.PlanUncertainty> action) => _service.PostAsync((context) => action(_inner));
        public Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.PlanUncertainty, T> func) => _service.PostAsync<T>((context) => func(_inner));

        public static implicit operator VMS.TPS.Common.Model.API.PlanUncertainty(AsyncPlanUncertainty wrapper) => wrapper._inner;

        // Internal Explicit Implementation to expose _inner safely for covariance
        VMS.TPS.Common.Model.API.PlanUncertainty IEsapiWrapper<VMS.TPS.Common.Model.API.PlanUncertainty>.Inner => _inner;

        // Explicit or Implicit implementation of Service
        // Since _service is private, we expose it via the interface
        IEsapiService IEsapiWrapper<VMS.TPS.Common.Model.API.PlanUncertainty>.Service => _service;
    }
}
