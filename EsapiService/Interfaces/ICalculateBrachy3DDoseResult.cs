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
    public interface ICalculateBrachy3DDoseResult : ISerializableObject
    {
        // --- Simple Properties --- //
        IEnumerable<string> Errors { get; }
        double RoundedDwellTimeAdjustRatio { get; }
        bool Success { get; }

        // --- RunAsync --- //
        /// <summary>
        /// Runs a function against the raw ESAPI VMS.TPS.Common.Model.API.CalculateBrachy3DDoseResult object safely on the ESAPI thread.
        /// </summary>
        Task RunAsync(Action<VMS.TPS.Common.Model.API.CalculateBrachy3DDoseResult> action);

        /// <summary>
        /// Runs a function against the raw ESAPI VMS.TPS.Common.Model.API.CalculateBrachy3DDoseResult object safely on the ESAPI thread.
        /// </summary>
        Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.CalculateBrachy3DDoseResult, T> func);
    }
}
