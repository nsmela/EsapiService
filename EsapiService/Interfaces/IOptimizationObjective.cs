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
    public interface IOptimizationObjective : ISerializableObject
    {
        // --- Simple Properties --- //
        OptimizationObjectiveOperator Operator { get; } // simple property
        double Priority { get; } // simple property
        string StructureId { get; } // simple property

        // --- Accessors --- //
        Task<IStructure> GetStructureAsync(); // read complex property

        // --- RunAsync --- //
        /// <summary>
        /// Runs a function against the raw ESAPI VMS.TPS.Common.Model.API.OptimizationObjective object safely on the ESAPI thread.
        /// </summary>
        Task RunAsync(Action<VMS.TPS.Common.Model.API.OptimizationObjective> action);

        /// <summary>
        /// Runs a function against the raw ESAPI VMS.TPS.Common.Model.API.OptimizationObjective object safely on the ESAPI thread.
        /// </summary>
        Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.OptimizationObjective, T> func);

        /// <summary>
        /// Updated the properties from the raw Esapi VMS.TPS.Common.Model.API.OptimizationObjective object
        /// </summary>
        new void Refresh();

        /* --- Skipped Members (Not generated) ---
           - op_Equality: Static members are not supported
           - op_Inequality: Static members are not supported
        */
    }
}
