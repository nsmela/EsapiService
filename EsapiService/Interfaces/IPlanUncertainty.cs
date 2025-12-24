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
        double CalibrationCurveError { get; } // simple property
        string DisplayName { get; } // simple property
        VVector IsocenterShift { get; } // simple property
        PlanUncertaintyType UncertaintyType { get; } // simple property

        // --- Accessors --- //
        Task<IDose> GetDoseAsync(); // read complex property

        // --- Collections --- //
        Task<IReadOnlyList<IBeamUncertainty>> GetBeamUncertaintiesAsync(); // collection proeprty context

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

        /// <summary>
        /// Updated the properties from the raw Esapi VMS.TPS.Common.Model.API.PlanUncertainty object
        /// </summary>
        new void Refresh();
    }
}
