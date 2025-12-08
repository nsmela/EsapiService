using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Media;
using VMS.TPS.Common.Model.API;
using VMS.TPS.Common.Model.Types;
using Esapi.Services;
using Esapi.Interfaces;

namespace Esapi.Interfaces
{
    public interface IBlock : IApiDataObject
    {
        // --- Simple Properties --- //
        bool IsDiverging { get; }
        Windows.Point[][] Outline { get; }
        Task SetOutlineAsync(Windows.Point[][] value);
        double TransmissionFactor { get; }
        double TrayTransmissionFactor { get; }
        BlockType Type { get; }

        // --- Accessors --- //
        Task<IAddOnMaterial> GetAddOnMaterialAsync();
        Task<ITray> GetTrayAsync();

        // --- RunAsync --- //
        /// <summary>
        /// Runs a function against the raw ESAPI VMS.TPS.Common.Model.API.Block object safely on the ESAPI thread.
        /// </summary>
        Task RunAsync(Action<VMS.TPS.Common.Model.API.Block> action);

        /// <summary>
        /// Runs a function against the raw ESAPI VMS.TPS.Common.Model.API.Block object safely on the ESAPI thread.
        /// </summary>
        Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.Block, T> func);
    }
}
