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
    public interface ISeedCollection : IApiDataObject
    {
        Task WriteXmlAsync(System.Xml.XmlWriter writer);
        System.Collections.Generic.IReadOnlyList<IBrachyFieldReferencePoint> BrachyFieldReferencePoints { get; }
        System.Windows.Media.Color Color { get; }
        System.Collections.Generic.IReadOnlyList<ISourcePosition> SourcePositions { get; }

        /// <summary>
        /// Runs a function against the raw ESAPI VMS.TPS.Common.Model.API.SeedCollection object safely on the ESAPI thread.
        /// </summary>
        Task RunAsync(Action<VMS.TPS.Common.Model.API.SeedCollection> action);

        /// <summary>
        /// Runs a function against the raw ESAPI VMS.TPS.Common.Model.API.SeedCollection object safely on the ESAPI thread.
        /// </summary>
        Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.SeedCollection, T> func);
    }
}
