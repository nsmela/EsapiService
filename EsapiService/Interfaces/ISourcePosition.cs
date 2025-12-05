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
    public interface ISourcePosition : IApiDataObject
    {
        Task WriteXmlAsync(System.Xml.XmlWriter writer);
        double DwellTime { get; }
        System.Collections.Generic.IReadOnlyList<bool> DwellTimeLock { get; }
        double NominalDwellTime { get; }
        Task SetNominalDwellTimeAsync(double value);
        Task<IRadioactiveSource> GetRadioactiveSourceAsync();
        double[,] Transform { get; }
        VMS.TPS.Common.Model.Types.VVector Translation { get; }

        /// <summary>
        /// Runs a function against the raw ESAPI VMS.TPS.Common.Model.API.SourcePosition object safely on the ESAPI thread.
        /// </summary>
        Task RunAsync(Action<VMS.TPS.Common.Model.API.SourcePosition> action);

        /// <summary>
        /// Runs a function against the raw ESAPI VMS.TPS.Common.Model.API.SourcePosition object safely on the ESAPI thread.
        /// </summary>
        Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.SourcePosition, T> func);
    }
}
