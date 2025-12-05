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
    public interface IApplicationScriptLog : IApiDataObject
    {
        Task WriteXmlAsync(System.Xml.XmlWriter writer);
        string CourseId { get; }
        string PatientId { get; }
        string PlanSetupId { get; }
        string PlanUID { get; }
        Task<IApplicationScript> GetScriptAsync();
        string ScriptFullName { get; }
        string StructureSetId { get; }
        string StructureSetUID { get; }

        /// <summary>
        /// Runs a function against the raw ESAPI VMS.TPS.Common.Model.API.ApplicationScriptLog object safely on the ESAPI thread.
        /// </summary>
        Task RunAsync(Action<VMS.TPS.Common.Model.API.ApplicationScriptLog> action);

        /// <summary>
        /// Runs a function against the raw ESAPI VMS.TPS.Common.Model.API.ApplicationScriptLog object safely on the ESAPI thread.
        /// </summary>
        Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.ApplicationScriptLog, T> func);
    }
}
