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
    public interface ITreatmentUnitOperatingLimit : IApiDataObject
    {
        // --- Simple Properties --- //
        string Label { get; } // simple property
        double MaxValue { get; } // simple property
        double MinValue { get; } // simple property
        int? Precision { get; } // simple property
        string UnitString { get; } // simple property

        // --- RunAsync --- //
        /// <summary>
        /// Runs a function against the raw ESAPI VMS.TPS.Common.Model.API.TreatmentUnitOperatingLimit object safely on the ESAPI thread.
        /// </summary>
        Task RunAsync(Action<VMS.TPS.Common.Model.API.TreatmentUnitOperatingLimit> action);

        /// <summary>
        /// Runs a function against the raw ESAPI VMS.TPS.Common.Model.API.TreatmentUnitOperatingLimit object safely on the ESAPI thread.
        /// </summary>
        Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.TreatmentUnitOperatingLimit, T> func);

        /// <summary>
        /// Updated the properties from the raw Esapi VMS.TPS.Common.Model.API.TreatmentUnitOperatingLimit object
        /// </summary>
        new void Refresh();
    }
}
