using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;
using VMS.TPS.Common.Model.API;
using VMS.TPS.Common.Model.Types;
using Esapi.Interfaces;
using Esapi.Services;

namespace Esapi.Wrappers
{
    public class AsyncRadioactiveSourceModel : AsyncApiDataObject, IRadioactiveSourceModel
    {
        internal readonly VMS.TPS.Common.Model.API.RadioactiveSourceModel _inner;

        // Store the inner ESAPI object reference
        // internal so other wrappers can access it
        // new to override any inherited _inner fields
        internal new readonly IEsapiService _service;

        public AsyncRadioactiveSourceModel(VMS.TPS.Common.Model.API.RadioactiveSourceModel inner, IEsapiService service) : base(inner, service)
        {
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


        public VVector ActiveSize { get; }

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

        public Task RunAsync(Action<VMS.TPS.Common.Model.API.RadioactiveSourceModel> action) => _service.RunAsync(() => action(_inner));
        public Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.RadioactiveSourceModel, T> func) => _service.RunAsync(() => func(_inner));
    }
}
