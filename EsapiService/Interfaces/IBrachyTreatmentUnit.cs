using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Media;
using VMS.TPS.Common.Model.API;
using VMS.TPS.Common.Model.Types;
using Esapi.Services;

namespace Esapi.Interfaces
{
    public interface IBrachyTreatmentUnit : IApiDataObject
    {
        // --- Simple Properties --- //
        string DoseRateMode { get; }
        double DwellTimeResolution { get; }
        string MachineInterface { get; }
        string MachineModel { get; }
        double MaxDwellTimePerChannel { get; }
        double MaxDwellTimePerPos { get; }
        double MaxDwellTimePerTreatment { get; }
        double MaximumChannelLength { get; }
        int MaximumDwellPositionsPerChannel { get; }
        double MaximumStepSize { get; }
        double MinAllowedSourcePos { get; }
        double MinimumChannelLength { get; }
        double MinimumStepSize { get; }
        int NumberOfChannels { get; }
        double SourceCenterOffsetFromTip { get; }
        string SourceMovementType { get; }
        double StepSizeResolution { get; }

        // --- Methods --- //
        Task<IRadioactiveSource> GetActiveRadioactiveSourceAsync(); // complex method

        // --- RunAsync --- //
        /// <summary>
        /// Runs a function against the raw ESAPI VMS.TPS.Common.Model.API.BrachyTreatmentUnit object safely on the ESAPI thread.
        /// </summary>
        Task RunAsync(Action<VMS.TPS.Common.Model.API.BrachyTreatmentUnit> action);

        /// <summary>
        /// Runs a function against the raw ESAPI VMS.TPS.Common.Model.API.BrachyTreatmentUnit object safely on the ESAPI thread.
        /// </summary>
        Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.BrachyTreatmentUnit, T> func);
    }
}
