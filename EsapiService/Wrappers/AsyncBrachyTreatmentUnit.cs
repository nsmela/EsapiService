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
    public class AsyncBrachyTreatmentUnit : AsyncApiDataObject, IBrachyTreatmentUnit, IEsapiWrapper<VMS.TPS.Common.Model.API.BrachyTreatmentUnit>
    {
        internal new readonly VMS.TPS.Common.Model.API.BrachyTreatmentUnit _inner;

        // Store the inner ESAPI object reference
        // internal so other wrappers can access it
        // new to override any inherited _inner fields
        internal new readonly IEsapiService _service;

        public AsyncBrachyTreatmentUnit(VMS.TPS.Common.Model.API.BrachyTreatmentUnit inner, IEsapiService service) : base(inner, service)
        {
            if (inner is null) throw new ArgumentNullException(nameof(inner));
            if (service is null) throw new ArgumentNullException(nameof(service));

            _inner = inner;
            _service = service;
        }


        public async Task<IRadioactiveSource> GetActiveRadioactiveSourceAsync()
        {
            return await _service.PostAsync(context => 
                _inner.GetActiveRadioactiveSource() is var result && result is null ? null : new AsyncRadioactiveSource(result, _service));
        }

        public string DoseRateMode =>
            _inner.DoseRateMode;


        public double DwellTimeResolution =>
            _inner.DwellTimeResolution;


        public string MachineInterface =>
            _inner.MachineInterface;


        public string MachineModel =>
            _inner.MachineModel;


        public double MaxDwellTimePerChannel =>
            _inner.MaxDwellTimePerChannel;


        public double MaxDwellTimePerPos =>
            _inner.MaxDwellTimePerPos;


        public double MaxDwellTimePerTreatment =>
            _inner.MaxDwellTimePerTreatment;


        public double MaximumChannelLength =>
            _inner.MaximumChannelLength;


        public int MaximumDwellPositionsPerChannel =>
            _inner.MaximumDwellPositionsPerChannel;


        public double MaximumStepSize =>
            _inner.MaximumStepSize;


        public double MinAllowedSourcePos =>
            _inner.MinAllowedSourcePos;


        public double MinimumChannelLength =>
            _inner.MinimumChannelLength;


        public double MinimumStepSize =>
            _inner.MinimumStepSize;


        public int NumberOfChannels =>
            _inner.NumberOfChannels;


        public double SourceCenterOffsetFromTip =>
            _inner.SourceCenterOffsetFromTip;


        public string SourceMovementType =>
            _inner.SourceMovementType;


        public double StepSizeResolution =>
            _inner.StepSizeResolution;


        public Task RunAsync(Action<VMS.TPS.Common.Model.API.BrachyTreatmentUnit> action) => _service.PostAsync((context) => action(_inner));
        public Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.BrachyTreatmentUnit, T> func) => _service.PostAsync<T>((context) => func(_inner));

        // --- Validates --- //
        /// <summary>
        /// Verifies is the wrapped ESAPI object isn't null.
        /// </summary>
        public new bool IsValid() => !IsNotValid();

        /// <summary>
        /// Verifies is the wrapped ESAPI object is null.
        /// </summary>
        public new bool IsNotValid() => _inner is null;

        public static implicit operator VMS.TPS.Common.Model.API.BrachyTreatmentUnit(AsyncBrachyTreatmentUnit wrapper) => wrapper._inner;

        // Internal Explicit Implementation to expose _inner safely for covariance
        VMS.TPS.Common.Model.API.BrachyTreatmentUnit IEsapiWrapper<VMS.TPS.Common.Model.API.BrachyTreatmentUnit>.Inner => _inner;

        // Explicit or Implicit implementation of Service
        // Since _service is private, we expose it via the interface
        IEsapiService IEsapiWrapper<VMS.TPS.Common.Model.API.BrachyTreatmentUnit>.Service => _service;
    }
}
