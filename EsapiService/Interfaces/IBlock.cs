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
        Task WriteXmlAsync(System.Xml.XmlWriter writer);
        Task<IAddOnMaterial> GetAddOnMaterialAsync();
        bool IsDiverging { get; }
        System.Windows.Point[][] Outline { get; }
        Task SetOutlineAsync(System.Windows.Point[][] value);
        double TransmissionFactor { get; }
        Task<ITray> GetTrayAsync();
        double TrayTransmissionFactor { get; }
        VMS.TPS.Common.Model.Types.BlockType Type { get; }

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
