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
    public class AsyncTreatmentUnitOperatingLimit : AsyncApiDataObject, ITreatmentUnitOperatingLimit, IEsapiWrapper<VMS.TPS.Common.Model.API.TreatmentUnitOperatingLimit>
    {
        internal new readonly VMS.TPS.Common.Model.API.TreatmentUnitOperatingLimit _inner;

        // Store the inner ESAPI object reference
        // internal so other wrappers can access it
        // new to override any inherited _inner fields
        internal new readonly IEsapiService _service;

        public AsyncTreatmentUnitOperatingLimit(VMS.TPS.Common.Model.API.TreatmentUnitOperatingLimit inner, IEsapiService service) : base(inner, service)
        {
            if (inner is null) throw new ArgumentNullException(nameof(inner));
            if (service is null) throw new ArgumentNullException(nameof(service));

            _inner = inner;
            _service = service;

            Label = inner.Label;
            MaxValue = inner.MaxValue;
            MinValue = inner.MinValue;
            Precision = inner.Precision;
            UnitString = inner.UnitString;
        }


        public string Label { get; private set; }


        public double MaxValue { get; private set; }


        public double MinValue { get; private set; }


        public int? Precision { get; private set; }


        public string UnitString { get; private set; }


        public Task RunAsync(Action<VMS.TPS.Common.Model.API.TreatmentUnitOperatingLimit> action) => _service.PostAsync((context) => action(_inner));
        public Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.TreatmentUnitOperatingLimit, T> func) => _service.PostAsync<T>((context) => func(_inner));

        // updates simple properties that might have changed
        public new void Refresh()
        {
            base.Refresh();

            Label = _inner.Label;
            MaxValue = _inner.MaxValue;
            MinValue = _inner.MinValue;
            Precision = _inner.Precision;
            UnitString = _inner.UnitString;
        }

        public static implicit operator VMS.TPS.Common.Model.API.TreatmentUnitOperatingLimit(AsyncTreatmentUnitOperatingLimit wrapper) => wrapper._inner;

        // Internal Explicit Implementation to expose _inner safely for covariance
        VMS.TPS.Common.Model.API.TreatmentUnitOperatingLimit IEsapiWrapper<VMS.TPS.Common.Model.API.TreatmentUnitOperatingLimit>.Inner => _inner;

        // Explicit or Implicit implementation of Service
        // Since _service is private, we expose it via the interface
        IEsapiService IEsapiWrapper<VMS.TPS.Common.Model.API.TreatmentUnitOperatingLimit>.Service => _service;
    }
}
