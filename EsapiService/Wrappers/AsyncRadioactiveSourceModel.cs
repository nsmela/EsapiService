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
            if (inner is null) throw new ArgumentNullException(nameof(inner));
            if (service is null) throw new ArgumentNullException(nameof(service));

            _inner = inner;
            _service = service;

            ActiveSize = inner.ActiveSize;
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


        public VVector ActiveSize { get; private set; }


        public double ActivityConversionFactor { get; private set; }


        public string CalculationModel { get; private set; }


        public double DoseRateConstant { get; private set; }


        public double HalfLife { get; private set; }


        public string LiteratureReference { get; private set; }


        public string Manufacturer { get; private set; }


        public string SourceType { get; private set; }


        public string Status { get; private set; }


        public DateTime? StatusDate { get; private set; }


        public string StatusUserName { get; private set; }


        public Task RunAsync(Action<VMS.TPS.Common.Model.API.RadioactiveSourceModel> action) => _service.PostAsync((context) => action(_inner));
        public Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.RadioactiveSourceModel, T> func) => _service.PostAsync<T>((context) => func(_inner));

        // updates simple properties that might have changed
        public new void Refresh()
        {
            base.Refresh();

            ActiveSize = _inner.ActiveSize;
            ActivityConversionFactor = _inner.ActivityConversionFactor;
            CalculationModel = _inner.CalculationModel;
            DoseRateConstant = _inner.DoseRateConstant;
            HalfLife = _inner.HalfLife;
            LiteratureReference = _inner.LiteratureReference;
            Manufacturer = _inner.Manufacturer;
            SourceType = _inner.SourceType;
            Status = _inner.Status;
            StatusDate = _inner.StatusDate;
            StatusUserName = _inner.StatusUserName;
        }

        public static implicit operator VMS.TPS.Common.Model.API.RadioactiveSourceModel(AsyncRadioactiveSourceModel wrapper) => wrapper._inner;

        // Internal Explicit Implementation to expose _inner safely for covariance
        VMS.TPS.Common.Model.API.RadioactiveSourceModel IEsapiWrapper<VMS.TPS.Common.Model.API.RadioactiveSourceModel>.Inner => _inner;

        // Explicit or Implicit implementation of Service
        // Since _service is private, we expose it via the interface
        IEsapiService IEsapiWrapper<VMS.TPS.Common.Model.API.RadioactiveSourceModel>.Service => _service;
    }
}
