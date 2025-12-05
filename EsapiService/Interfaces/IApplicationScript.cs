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
    public interface IApplicationScript : IApiDataObject
    {
        Task WriteXmlAsync(System.Xml.XmlWriter writer);
        VMS.TPS.Common.Model.Types.ApplicationScriptApprovalStatus ApprovalStatus { get; }
        string ApprovalStatusDisplayText { get; }
        System.Reflection.AssemblyName AssemblyName { get; }
        System.Collections.Generic.IReadOnlyList<System.DateTime> ExpirationDate { get; }
        bool IsReadOnlyScript { get; }
        bool IsWriteableScript { get; }
        string PublisherName { get; }
        VMS.TPS.Common.Model.Types.ApplicationScriptType ScriptType { get; }
        System.Collections.Generic.IReadOnlyList<System.DateTime> StatusDate { get; }
        VMS.TPS.Common.Model.Types.UserIdentity StatusUserIdentity { get; }

        /// <summary>
        /// Runs a function against the raw ESAPI VMS.TPS.Common.Model.API.ApplicationScript object safely on the ESAPI thread.
        /// </summary>
        Task RunAsync(Action<VMS.TPS.Common.Model.API.ApplicationScript> action);

        /// <summary>
        /// Runs a function against the raw ESAPI VMS.TPS.Common.Model.API.ApplicationScript object safely on the ESAPI thread.
        /// </summary>
        Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.ApplicationScript, T> func);
    }
}
