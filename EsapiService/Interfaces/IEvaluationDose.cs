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
    public interface IEvaluationDose : IDose
    {
        Task WriteXmlAsync(System.Xml.XmlWriter writer);
        Task<int> DoseValueToVoxelAsync(VMS.TPS.Common.Model.Types.DoseValue doseValue);
        Task SetVoxelsAsync(int planeIndex, int[,] values);

        /// <summary>
        /// Runs a function against the raw ESAPI VMS.TPS.Common.Model.API.EvaluationDose object safely on the ESAPI thread.
        /// </summary>
        Task RunAsync(Action<VMS.TPS.Common.Model.API.EvaluationDose> action);

        /// <summary>
        /// Runs a function against the raw ESAPI VMS.TPS.Common.Model.API.EvaluationDose object safely on the ESAPI thread.
        /// </summary>
        Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.EvaluationDose, T> func);
    }
}
