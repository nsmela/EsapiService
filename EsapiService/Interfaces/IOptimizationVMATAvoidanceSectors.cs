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
    public interface IOptimizationVMATAvoidanceSectors : IOptimizationParameter
    {
        Task WriteXmlAsync(System.Xml.XmlWriter writer);
        VMS.TPS.Common.Model.Types.OptimizationAvoidanceSector AvoidanceSector1 { get; }
        VMS.TPS.Common.Model.Types.OptimizationAvoidanceSector AvoidanceSector2 { get; }
        Task<IBeam> GetBeamAsync();
        bool IsValid { get; }
        string ValidationError { get; }

        /// <summary>
        /// Runs a function against the raw ESAPI VMS.TPS.Common.Model.API.OptimizationVMATAvoidanceSectors object safely on the ESAPI thread.
        /// </summary>
        Task RunAsync(Action<VMS.TPS.Common.Model.API.OptimizationVMATAvoidanceSectors> action);

        /// <summary>
        /// Runs a function against the raw ESAPI VMS.TPS.Common.Model.API.OptimizationVMATAvoidanceSectors object safely on the ESAPI thread.
        /// </summary>
        Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.OptimizationVMATAvoidanceSectors, T> func);
    }
}
