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
        DateTime? CreationDateTime { get; }
        IEnumerable<Structure> StructuresSelectedForDvh { get; }

        // --- Accessors --- //
        Task<ICourse> GetCourseAsync(); // read complex property
        Task<IPlanningItemDose> GetDoseAsync(); // read complex property
        Task<IStructureSet> GetStructureSetAsync(); // read complex property

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
