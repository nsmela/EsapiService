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
    public class AsyncRadioactiveSourceModel : AsyncApiDataObject, IRadioactiveSourceModel, IEsapiWrapper<VMS.TPS.Common.Model.API.RadioactiveSourceModel>
    {
        internal new readonly VMS.TPS.Common.Model.API.RadioactiveSourceModel _inner;

        // Store the inner ESAPI object reference
        // internal so other wrappers can access it
        // new to override any inherited _inner fields
        internal new readonly IEsapiService _service;

public AsyncRadioactiveSourceModel(VMS.TPS.Common.Model.API.RadioactiveSourceModel inner, IEsapiService service) : base(inner, service)
        {
            if (inner == null) throw new ArgumentNullException(nameof(inner));
            if (service == null) throw new ArgumentNullException(nameof(service));

            _inner = inner;
            _service = service;

            ActivityConversionFactor = inner.ActivityConversionFactor;
            CalculationModel = inner.CalculationModel;
            DoseRateConstant = inner.DoseRateConstant;
            HalfLife = inner.HalfLife;
            LiteratureReference = inner.LiteratureReference;
            Manufacturer = inner.Manufacturer;
            SourceType = inner.SourceType;
            Status = inner.Status;
            StatusDate = inner.StatusDate;
            StatusUserName = inner.StatusUserName;
        }

        public double ActivityConversionFactor { get; }

        public string CalculationModel { get; }

        public double DoseRateConstant { get; }

        public double HalfLife { get; }

        public string LiteratureReference { get; }

        public string Manufacturer { get; }

        public string SourceType { get; }

        public string Status { get; }

        public DateTime? StatusDate { get; }

        public string StatusUserName { get; }

        public Task RunAsync(Action<VMS.TPS.Common.Model.API.RadioactiveSourceModel> action) => _service.PostAsync((context) => action(_inner));
        public Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.RadioactiveSourceModel, T> func) => _service.PostAsync<T>((context) => func(_inner));

        public static implicit operator VMS.TPS.Common.Model.API.RadioactiveSourceModel(AsyncRadioactiveSourceModel wrapper) => wrapper;

        // Internal Explicit Implementation to expose _inner safely for covariance
        VMS.TPS.Common.Model.API.RadioactiveSourceModel IEsapiWrapper<VMS.TPS.Common.Model.API.RadioactiveSourceModel>.Inner => _inner;
    }
}
