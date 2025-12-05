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
    public interface IStudy : IApiDataObject
    {
        Task WriteXmlAsync(System.Xml.XmlWriter writer);
        System.Collections.Generic.IReadOnlyList<System.DateTime> CreationDateTime { get; }
        System.Collections.Generic.IReadOnlyList<IImage> Images3D { get; }
        System.Collections.Generic.IReadOnlyList<ISeries> Series { get; }
        string UID { get; }

        /// <summary>
        /// Runs a function against the raw ESAPI VMS.TPS.Common.Model.API.Study object safely on the ESAPI thread.
        /// </summary>
        Task RunAsync(Action<VMS.TPS.Common.Model.API.Study> action);

        /// <summary>
        /// Runs a function against the raw ESAPI VMS.TPS.Common.Model.API.Study object safely on the ESAPI thread.
        /// </summary>
        Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.Study, T> func);
    }
}
