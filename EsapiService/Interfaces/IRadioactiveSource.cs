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
    public interface IRadioactiveSource : IApiDataObject
    {
        // --- Simple Properties --- //
        DateTime? CalibrationDate { get; } // simple property
        bool NominalActivity { get; } // simple property
        string SerialNumber { get; } // simple property
        double Strength { get; } // simple property

        // --- Accessors --- //
        Task<IRadioactiveSourceModel> GetRadioactiveSourceModelAsync(); // read complex property

        // --- RunAsync --- //
        /// <summary>
        /// Runs a function against the raw ESAPI VMS.TPS.Common.Model.API.RadioactiveSource object safely on the ESAPI thread.
        /// </summary>
        Task RunAsync(Action<VMS.TPS.Common.Model.API.RadioactiveSource> action);

        /// <summary>
        /// Runs a function against the raw ESAPI VMS.TPS.Common.Model.API.RadioactiveSource object safely on the ESAPI thread.
        /// </summary>
        Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.RadioactiveSource, T> func);

        /// <summary>
        /// Updated the properties from the raw Esapi VMS.TPS.Common.Model.API.RadioactiveSource object
        /// </summary>
        new void Refresh();
    }
}
