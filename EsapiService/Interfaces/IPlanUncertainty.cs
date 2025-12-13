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
    public interface IPlanUncertainty : IApiDataObject
    {
        // --- Simple Properties --- //
        IEnumerable<BeamUncertainty> BeamUncertainties { get; }
        double CalibrationCurveError { get; }
        string DisplayName { get; }
        VVector IsocenterShift { get; }
        PlanUncertaintyType UncertaintyType { get; }

        // --- Accessors --- //
        Task<IDose> GetDoseAsync(); // read complex property

        // --- Methods --- //
        Task<IDVHData> GetDVHCumulativeDataAsync(IStructure structure, DoseValuePresentation dosePresentation, VolumePresentation volumePresentation, double binWidth); // complex method

        // --- RunAsync --- //
        /// <summary>
        /// Runs a function against the raw ESAPI VMS.TPS.Common.Model.API.PlanUncertainty object safely on the ESAPI thread.
        /// </summary>
        Task RunAsync(Action<VMS.TPS.Common.Model.API.PlanUncertainty> action);

        /// <summary>
        /// Runs a function against the raw ESAPI VMS.TPS.Common.Model.API.PlanUncertainty object safely on the ESAPI thread.
        /// </summary>
        Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.PlanUncertainty, T> func);
    }
}
