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
            _inner = inner;
            _service = service;

            BeamUncertainties = inner.BeamUncertainties;
            CalibrationCurveError = inner.CalibrationCurveError;
            DisplayName = inner.DisplayName;
        }

        public IEnumerable<BeamUncertainty> BeamUncertainties { get; }

        public double CalibrationCurveError { get; }

        public string DisplayName { get; }

        public async Task<IDose> GetDoseAsync()
        {
            return await _service.PostAsync(context => 
                _inner.Dose is null ? null : new AsyncDose(_inner.Dose, _service));
        }

        public Task RunAsync(Action<VMS.TPS.Common.Model.API.PlanUncertainty> action) => _service.PostAsync((context) => action(_inner));
        public Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.PlanUncertainty, T> func) => _service.PostAsync<T>((context) => func(_inner));

        public static implicit operator VMS.TPS.Common.Model.API.PlanUncertainty(AsyncPlanUncertainty wrapper) => wrapper;

        // Internal Explicit Implementation to expose _inner safely for covariance
        VMS.TPS.Common.Model.API.PlanUncertainty IEsapiWrapper<VMS.TPS.Common.Model.API.PlanUncertainty>.Inner => _inner;
    }
}
