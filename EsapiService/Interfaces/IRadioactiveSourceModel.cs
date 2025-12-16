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
    public interface IRadioactiveSourceModel : IApiDataObject
    {
        // --- Simple Properties --- //
        VVector ActiveSize { get; } // simple property
        double ActivityConversionFactor { get; } // simple property
        string CalculationModel { get; } // simple property
        double DoseRateConstant { get; } // simple property
        double HalfLife { get; } // simple property
        string LiteratureReference { get; } // simple property
        string Manufacturer { get; } // simple property
        string SourceType { get; } // simple property
        string Status { get; } // simple property
        DateTime? StatusDate { get; } // simple property
        string StatusUserName { get; } // simple property

        // --- RunAsync --- //
        /// <summary>
        /// Runs a function against the raw ESAPI VMS.TPS.Common.Model.API.RadioactiveSourceModel object safely on the ESAPI thread.
        /// </summary>
        Task RunAsync(Action<VMS.TPS.Common.Model.API.RadioactiveSourceModel> action);

        /// <summary>
        /// Runs a function against the raw ESAPI VMS.TPS.Common.Model.API.RadioactiveSourceModel object safely on the ESAPI thread.
        /// </summary>
        Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.RadioactiveSourceModel, T> func);
    }
}
