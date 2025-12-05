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
    public interface IDynamicWedge : IWedge
    {
        Task WriteXmlAsync(System.Xml.XmlWriter writer);

        /// <summary>
        /// Runs a function against the raw ESAPI VMS.TPS.Common.Model.API.DynamicWedge object safely on the ESAPI thread.
        /// </summary>
        Task RunAsync(Action<VMS.TPS.Common.Model.API.DynamicWedge> action);

        /// <summary>
        /// Runs a function against the raw ESAPI VMS.TPS.Common.Model.API.DynamicWedge object safely on the ESAPI thread.
        /// </summary>
        Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.DynamicWedge, T> func);
    }
}
