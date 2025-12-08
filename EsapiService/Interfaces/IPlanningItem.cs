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
    public interface IPlanningItem : IApiDataObject
    {
        // --- Simple Properties --- //
        DoseValuePresentation DoseValuePresentation { get; }
        Task SetDoseValuePresentationAsync(DoseValuePresentation value);

        // --- Accessors --- //
        Task<ICourse> GetCourseAsync();
        Task<IPlanningItemDose> GetDoseAsync();
        Task<IStructureSet> GetStructureSetAsync();

        // --- Collections --- //
        IReadOnlyList<DateTime> CreationDateTime { get; }
        Task<IReadOnlyList<IStructure>> GetStructuresSelectedForDvhAsync();

        // --- Methods --- //
        Task<IReadOnlyList<ClinicalGoal>> GetClinicalGoalsAsync();
        Task<IDVHData> GetDVHCumulativeDataAsync(IStructure structure, DoseValuePresentation dosePresentation, VolumePresentation volumePresentation, double binWidth);
        Task<DoseValue> GetDoseAtVolumeAsync(IStructure structure, double volume, VolumePresentation volumePresentation, DoseValuePresentation requestedDosePresentation);
        Task<double> GetVolumeAtDoseAsync(IStructure structure, DoseValue dose, VolumePresentation requestedVolumePresentation);

        // --- RunAsync --- //
        /// <summary>
        /// Runs a function against the raw ESAPI VMS.TPS.Common.Model.API.PlanningItem object safely on the ESAPI thread.
        /// </summary>
        Task RunAsync(Action<VMS.TPS.Common.Model.API.PlanningItem> action);

        /// <summary>
        /// Runs a function against the raw ESAPI VMS.TPS.Common.Model.API.PlanningItem object safely on the ESAPI thread.
        /// </summary>
        Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.PlanningItem, T> func);
    }
}
