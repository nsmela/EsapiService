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
    public interface IActiveStructureCodeDictionaries
    {

        // --- Accessors --- //
        Task<IStructureCodeDictionary> GetFmaAsync(); // read complex property
        Task<IStructureCodeDictionary> GetRadLexAsync(); // read complex property
        Task<IStructureCodeDictionary> GetSrtAsync(); // read complex property
        Task<IStructureCodeDictionary> GetVmsStructCodeAsync(); // read complex property

        // --- RunAsync --- //
        /// <summary>
        /// Runs a function against the raw ESAPI VMS.TPS.Common.Model.API.ActiveStructureCodeDictionaries object safely on the ESAPI thread.
        /// </summary>
        Task RunAsync(Action<VMS.TPS.Common.Model.API.ActiveStructureCodeDictionaries> action);

        /// <summary>
        /// Runs a function against the raw ESAPI VMS.TPS.Common.Model.API.ActiveStructureCodeDictionaries object safely on the ESAPI thread.
        /// </summary>
        Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.ActiveStructureCodeDictionaries, T> func);

        /// <summary>
        /// Updated the properties from the raw Esapi VMS.TPS.Common.Model.API.ActiveStructureCodeDictionaries object
        /// </summary>
        void Refresh();

        /* --- Skipped Members (Not generated) ---
           - .ctor: Explicitly ignored by name
        */
    }
}
