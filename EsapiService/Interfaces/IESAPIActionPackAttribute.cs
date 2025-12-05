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
    public interface IESAPIActionPackAttribute
    {
        bool IsWriteable { get; }
        Task SetIsWriteableAsync(bool value);

        /// <summary>
        /// Runs a function against the raw ESAPI VMS.TPS.Common.Model.API.ESAPIActionPackAttribute object safely on the ESAPI thread.
        /// </summary>
        Task RunAsync(Action<VMS.TPS.Common.Model.API.ESAPIActionPackAttribute> action);

        /// <summary>
        /// Runs a function against the raw ESAPI VMS.TPS.Common.Model.API.ESAPIActionPackAttribute object safely on the ESAPI thread.
        /// </summary>
        Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.ESAPIActionPackAttribute, T> func);
    }
}
