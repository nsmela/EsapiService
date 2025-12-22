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
    public interface IPlanningItem : IApiDataObject
    {
        // --- Simple Properties --- //
        DateTime? CreationDateTime { get; } // simple property
        DoseValuePresentation DoseValuePresentation { get; } // simple property
        Task SetDoseValuePresentationAsync(DoseValuePresentation value);

        // --- Accessors --- //
        Task<IPlanningItemDose> GetDoseAsync(); // read complex property
        Task<IStructureSet> GetStructureSetAsync(); // read complex property

        // --- Collections --- //
        Task<IReadOnlyList<IStructure>> GetStructuresSelectedForDvhAsync(); // collection proeprty context

        // --- Methods --- //
        Task<IDVHData> GetDVHCumulativeDataAsync(IStructure structure, DoseValuePresentation dosePresentation, VolumePresentation volumePresentation, double binWidth); // complex method
        Task<DoseValue> GetDoseAtVolumeAsync(IStructure structure, double volume, VolumePresentation volumePresentation, DoseValuePresentation requestedDosePresentation); // simple method
        Task<double> GetVolumeAtDoseAsync(IStructure structure, DoseValue dose, VolumePresentation requestedVolumePresentation); // simple method

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
