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
        string DoseRateMode { get; } // simple property
        double DwellTimeResolution { get; } // simple property
        string MachineInterface { get; } // simple property
        string MachineModel { get; } // simple property
        double MaxDwellTimePerChannel { get; } // simple property
        double MaxDwellTimePerPos { get; } // simple property
        double MaxDwellTimePerTreatment { get; } // simple property
        double MaximumChannelLength { get; } // simple property
        int MaximumDwellPositionsPerChannel { get; } // simple property
        double MaximumStepSize { get; } // simple property
        double MinAllowedSourcePos { get; } // simple property
        double MinimumChannelLength { get; } // simple property
        double MinimumStepSize { get; } // simple property
        int NumberOfChannels { get; } // simple property
        double SourceCenterOffsetFromTip { get; } // simple property
        string SourceMovementType { get; } // simple property
        double StepSizeResolution { get; } // simple property

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

        /// <summary>
        /// Updated the properties from the raw Esapi VMS.TPS.Common.Model.API.BrachyTreatmentUnit object
        /// </summary>
        new void Refresh();
    }
}
