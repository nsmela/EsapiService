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
    public interface IBlock : IApiDataObject
    {
        // --- Simple Properties --- //
        bool IsDiverging { get; } // simple property
        System.Windows.Point[][] Outline { get; set; } // simple property
        double TransmissionFactor { get; } // simple property
        double TrayTransmissionFactor { get; } // simple property
        BlockType Type { get; } // simple property

        // --- Accessors --- //
        Task<IAddOnMaterial> GetAddOnMaterialAsync(); // read complex property
        Task<ITray> GetTrayAsync(); // read complex property

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
