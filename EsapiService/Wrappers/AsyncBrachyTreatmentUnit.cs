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

            DoseRateMode = inner.DoseRateMode;
            DwellTimeResolution = inner.DwellTimeResolution;
            MachineInterface = inner.MachineInterface;
            MachineModel = inner.MachineModel;
            MaxDwellTimePerChannel = inner.MaxDwellTimePerChannel;
            MaxDwellTimePerPos = inner.MaxDwellTimePerPos;
            MaxDwellTimePerTreatment = inner.MaxDwellTimePerTreatment;
            MaximumChannelLength = inner.MaximumChannelLength;
            MaximumDwellPositionsPerChannel = inner.MaximumDwellPositionsPerChannel;
            MaximumStepSize = inner.MaximumStepSize;
            MinAllowedSourcePos = inner.MinAllowedSourcePos;
            MinimumChannelLength = inner.MinimumChannelLength;
            MinimumStepSize = inner.MinimumStepSize;
            NumberOfChannels = inner.NumberOfChannels;
            SourceCenterOffsetFromTip = inner.SourceCenterOffsetFromTip;
            SourceMovementType = inner.SourceMovementType;
            StepSizeResolution = inner.StepSizeResolution;
        }


        public async Task<IRadioactiveSource> GetActiveRadioactiveSourceAsync()
        {
            return await _service.PostAsync(context => 
                _inner.GetActiveRadioactiveSource() is var result && result is null ? null : new AsyncRadioactiveSource(result, _service));
        }

        public string DoseRateMode { get; private set; }


        public double DwellTimeResolution { get; private set; }


        public string MachineInterface { get; private set; }


        public string MachineModel { get; private set; }


        public double MaxDwellTimePerChannel { get; private set; }


        public double MaxDwellTimePerPos { get; private set; }


        public double MaxDwellTimePerTreatment { get; private set; }


        public double MaximumChannelLength { get; private set; }


        public int MaximumDwellPositionsPerChannel { get; private set; }


        public double MaximumStepSize { get; private set; }


        public double MinAllowedSourcePos { get; private set; }


        public double MinimumChannelLength { get; private set; }


        public double MinimumStepSize { get; private set; }


        public int NumberOfChannels { get; private set; }


        public double SourceCenterOffsetFromTip { get; private set; }


        public string SourceMovementType { get; private set; }


        public double StepSizeResolution { get; private set; }


        public Task RunAsync(Action<VMS.TPS.Common.Model.API.BrachyTreatmentUnit> action) => _service.PostAsync((context) => action(_inner));
        public Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.BrachyTreatmentUnit, T> func) => _service.PostAsync<T>((context) => func(_inner));

        // updates simple properties that might have changed
        public new void Refresh()
        {
            base.Refresh();

            DoseRateMode = _inner.DoseRateMode;
            DwellTimeResolution = _inner.DwellTimeResolution;
            MachineInterface = _inner.MachineInterface;
            MachineModel = _inner.MachineModel;
            MaxDwellTimePerChannel = _inner.MaxDwellTimePerChannel;
            MaxDwellTimePerPos = _inner.MaxDwellTimePerPos;
            MaxDwellTimePerTreatment = _inner.MaxDwellTimePerTreatment;
            MaximumChannelLength = _inner.MaximumChannelLength;
            MaximumDwellPositionsPerChannel = _inner.MaximumDwellPositionsPerChannel;
            MaximumStepSize = _inner.MaximumStepSize;
            MinAllowedSourcePos = _inner.MinAllowedSourcePos;
            MinimumChannelLength = _inner.MinimumChannelLength;
            MinimumStepSize = _inner.MinimumStepSize;
            NumberOfChannels = _inner.NumberOfChannels;
            SourceCenterOffsetFromTip = _inner.SourceCenterOffsetFromTip;
            SourceMovementType = _inner.SourceMovementType;
            StepSizeResolution = _inner.StepSizeResolution;
        }

        public static implicit operator VMS.TPS.Common.Model.API.BrachyTreatmentUnit(AsyncBrachyTreatmentUnit wrapper) => wrapper._inner;

        // Internal Explicit Implementation to expose _inner safely for covariance
        VMS.TPS.Common.Model.API.BrachyTreatmentUnit IEsapiWrapper<VMS.TPS.Common.Model.API.BrachyTreatmentUnit>.Inner => _inner;

        // Explicit or Implicit implementation of Service
        // Since _service is private, we expose it via the interface
        IEsapiService IEsapiWrapper<VMS.TPS.Common.Model.API.BrachyTreatmentUnit>.Service => _service;
    }
}
