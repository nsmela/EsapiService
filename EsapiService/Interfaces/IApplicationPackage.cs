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
    public interface IApplicationPackage : IApiDataObject
    {
        Task WriteXmlAsync(System.Xml.XmlWriter writer);
        VMS.TPS.Common.Model.Types.ApplicationScriptApprovalStatus ApprovalStatus { get; }
        string Description { get; }
        System.Collections.Generic.IReadOnlyList<System.DateTime> ExpirationDate { get; }
        string PackageId { get; }
        string PackageName { get; }
        string PackageVersion { get; }
        string PublisherData { get; }
        string PublisherName { get; }

        /// <summary>
        /// Runs a function against the raw ESAPI VMS.TPS.Common.Model.API.ApplicationPackage object safely on the ESAPI thread.
        /// </summary>
        Task RunAsync(Action<VMS.TPS.Common.Model.API.ApplicationPackage> action);

        /// <summary>
        /// Runs a function against the raw ESAPI VMS.TPS.Common.Model.API.ApplicationPackage object safely on the ESAPI thread.
        /// </summary>
        Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.ApplicationPackage, T> func);
    }
}
