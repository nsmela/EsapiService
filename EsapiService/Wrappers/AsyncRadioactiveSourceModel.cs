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
    public partial class AsyncRadioactiveSourceModel : AsyncApiDataObject, IRadioactiveSourceModel, IEsapiWrapper<VMS.TPS.Common.Model.API.RadioactiveSourceModel>
    {
        internal new readonly VMS.TPS.Common.Model.API.RadioactiveSourceModel _inner;

        // Store the inner ESAPI object reference
        // internal so other wrappers can access it
        // new to override any inherited _inner fields
        internal new readonly IEsapiService _service;

        public AsyncRadioactiveSourceModel(VMS.TPS.Common.Model.API.RadioactiveSourceModel inner, IEsapiService service) : base(inner, service)
        {
            if (inner is null) throw new ArgumentNullException(nameof(inner));
            if (service is null) throw new ArgumentNullException(nameof(service));

            _inner = inner;
            _service = service;
        }


        public VVector ActiveSize =>
            _inner.ActiveSize;


        public double ActivityConversionFactor =>
            _inner.ActivityConversionFactor;


        public string CalculationModel =>
            _inner.CalculationModel;


        public double DoseRateConstant =>
            _inner.DoseRateConstant;


        public double HalfLife =>
            _inner.HalfLife;


        public string LiteratureReference =>
            _inner.LiteratureReference;


        public string Manufacturer =>
            _inner.Manufacturer;


        public string SourceType =>
            _inner.SourceType;


        public string Status =>
            _inner.Status;


        public DateTime? StatusDate =>
            _inner.StatusDate;


        public string StatusUserName =>
            _inner.StatusUserName;


        public Task RunAsync(Action<VMS.TPS.Common.Model.API.RadioactiveSourceModel> action) => _service.PostAsync((context) => action(_inner));
        public Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.RadioactiveSourceModel, T> func) => _service.PostAsync<T>((context) => func(_inner));

        // --- Validates --- //
        /// <summary>
        /// Verifies is the wrapped ESAPI object isn't null.
        /// </summary>
        public new bool IsValid() => !IsNotValid();

        /// <summary>
        /// Verifies is the wrapped ESAPI object is null.
        /// </summary>
        public new bool IsNotValid() => _inner is null;

        public static implicit operator VMS.TPS.Common.Model.API.RadioactiveSourceModel(AsyncRadioactiveSourceModel wrapper) => wrapper._inner;

        // Internal Explicit Implementation to expose _inner safely for covariance
        VMS.TPS.Common.Model.API.RadioactiveSourceModel IEsapiWrapper<VMS.TPS.Common.Model.API.RadioactiveSourceModel>.Inner => _inner;

        // Explicit or Implicit implementation of Service
        // Since _service is private, we expose it via the interface
        IEsapiService IEsapiWrapper<VMS.TPS.Common.Model.API.RadioactiveSourceModel>.Service => _service;
    }
}
