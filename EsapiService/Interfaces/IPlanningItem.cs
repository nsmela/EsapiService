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
        Task WriteXmlAsync(System.Xml.XmlWriter writer);
        Task<System.Collections.Generic.IReadOnlyList<VMS.TPS.Common.Model.Types.ClinicalGoal>> GetClinicalGoalsAsync();
        Task<IDVHData> GetDVHCumulativeDataAsync(VMS.TPS.Common.Model.API.Structure structure, VMS.TPS.Common.Model.Types.DoseValuePresentation dosePresentation, VMS.TPS.Common.Model.Types.VolumePresentation volumePresentation, double binWidth);
        Task<VMS.TPS.Common.Model.Types.DoseValue> GetDoseAtVolumeAsync(VMS.TPS.Common.Model.API.Structure structure, double volume, VMS.TPS.Common.Model.Types.VolumePresentation volumePresentation, VMS.TPS.Common.Model.Types.DoseValuePresentation requestedDosePresentation);
        Task<double> GetVolumeAtDoseAsync(VMS.TPS.Common.Model.API.Structure structure, VMS.TPS.Common.Model.Types.DoseValue dose, VMS.TPS.Common.Model.Types.VolumePresentation requestedVolumePresentation);
        Task<ICourse> GetCourseAsync();
        System.Collections.Generic.IReadOnlyList<System.DateTime> CreationDateTime { get; }
        Task<IPlanningItemDose> GetDoseAsync();
        VMS.TPS.Common.Model.Types.DoseValuePresentation DoseValuePresentation { get; }
        Task SetDoseValuePresentationAsync(VMS.TPS.Common.Model.Types.DoseValuePresentation value);
        Task<IStructureSet> GetStructureSetAsync();
        System.Collections.Generic.IReadOnlyList<IStructure> StructuresSelectedForDvh { get; }

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
