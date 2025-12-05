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
    public interface ITradeoffObjective
    {
        int Id { get; }
        System.Collections.Generic.IReadOnlyList<IOptimizationObjective> OptimizationObjectives { get; }
        Task<IStructure> GetStructureAsync();

        /// <summary>
        /// Runs a function against the raw ESAPI VMS.TPS.Common.Model.API.TradeoffObjective object safely on the ESAPI thread.
        /// </summary>
        Task RunAsync(Action<VMS.TPS.Common.Model.API.TradeoffObjective> action);

        /// <summary>
        /// Runs a function against the raw ESAPI VMS.TPS.Common.Model.API.TradeoffObjective object safely on the ESAPI thread.
        /// </summary>
        Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.TradeoffObjective, T> func);
    }
}
