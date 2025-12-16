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
        // --- Simple Properties --- //
        ApplicationScriptApprovalStatus ApprovalStatus { get; } // simple property
        string ApprovalStatusDisplayText { get; } // simple property
        System.Reflection.AssemblyName AssemblyName { get; } // simple property
        DateTime? ExpirationDate { get; } // simple property
        bool IsReadOnlyScript { get; } // simple property
        bool IsWriteableScript { get; } // simple property
        string PublisherName { get; } // simple property
        ApplicationScriptType ScriptType { get; } // simple property
        DateTime? StatusDate { get; } // simple property
        UserIdentity StatusUserIdentity { get; } // simple property

        // --- RunAsync --- //
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
