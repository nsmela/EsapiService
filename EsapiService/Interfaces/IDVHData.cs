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
    public interface IDVHData : ISerializableObject
    {
        Task WriteXmlAsync(System.Xml.XmlWriter writer);
        double Coverage { get; }
        VMS.TPS.Common.Model.Types.DVHPoint[] CurveData { get; }
        VMS.TPS.Common.Model.Types.DoseValue MaxDose { get; }
        VMS.TPS.Common.Model.Types.VVector MaxDosePosition { get; }
        VMS.TPS.Common.Model.Types.DoseValue MeanDose { get; }
        VMS.TPS.Common.Model.Types.DoseValue MedianDose { get; }
        VMS.TPS.Common.Model.Types.DoseValue MinDose { get; }
        VMS.TPS.Common.Model.Types.VVector MinDosePosition { get; }
        double SamplingCoverage { get; }
        double StdDev { get; }
        double Volume { get; }

        /// <summary>
        /// Runs a function against the raw ESAPI VMS.TPS.Common.Model.API.DVHData object safely on the ESAPI thread.
        /// </summary>
        Task RunAsync(Action<VMS.TPS.Common.Model.API.DVHData> action);

        /// <summary>
        /// Runs a function against the raw ESAPI VMS.TPS.Common.Model.API.DVHData object safely on the ESAPI thread.
        /// </summary>
        Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.DVHData, T> func);
    }
}
