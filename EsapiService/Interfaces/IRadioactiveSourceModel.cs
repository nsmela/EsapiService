using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Media;
using VMS.TPS.Common.Model.API;
using VMS.TPS.Common.Model.Types;
using Esapi.Services;
using Esapi.Interfaces;

namespace Esapi.Interfaces
{
    public interface IRadioactiveSourceModel : IApiDataObject
    {
        // --- Simple Properties --- //
        VVector ActiveSize { get; }
        double ActivityConversionFactor { get; }
        string CalculationModel { get; }
        double DoseRateConstant { get; }
        double HalfLife { get; }
        string LiteratureReference { get; }
        string Manufacturer { get; }
        string SourceType { get; }
        string Status { get; }
        string StatusUserName { get; }

        // --- Collections --- //
        IReadOnlyList<DateTime> StatusDate { get; }

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
