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
    public interface IEstimatedDVH : IApiDataObject
    {
        Task WriteXmlAsync(System.Xml.XmlWriter writer);
        VMS.TPS.Common.Model.Types.DVHPoint[] CurveData { get; }
        Task<IPlanSetup> GetPlanSetupAsync();
        string PlanSetupId { get; }
        Task<IStructure> GetStructureAsync();
        string StructureId { get; }
        VMS.TPS.Common.Model.Types.DoseValue TargetDoseLevel { get; }
        VMS.TPS.Common.Model.Types.DVHEstimateType Type { get; }

        /// <summary>
        /// Runs a function against the raw ESAPI VMS.TPS.Common.Model.API.EstimatedDVH object safely on the ESAPI thread.
        /// </summary>
        Task RunAsync(Action<VMS.TPS.Common.Model.API.EstimatedDVH> action);

        /// <summary>
        /// Runs a function against the raw ESAPI VMS.TPS.Common.Model.API.EstimatedDVH object safely on the ESAPI thread.
        /// </summary>
        Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.EstimatedDVH, T> func);
    }
}
