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
    public interface IStructureSet : IApiDataObject
    {
        // --- Simple Properties --- //
        string SeriesUID { get; }
        string UID { get; }

        // --- Accessors --- //
        Task<IImage> GetImageAsync(); // read complex property
        Task<IPatient> GetPatientAsync(); // read complex property
        Task<ISeries> GetSeriesAsync(); // read complex property

        // --- Collections --- //
        Task<IReadOnlyList<IStructure>> GetStructuresAsync(); // collection proeprty context
        Task<IReadOnlyList<IApplicationScriptLog>> GetApplicationScriptLogsAsync(); // collection proeprty context

        // --- Methods --- //
        Task<(bool result, IReadOnlyList<string> removedStructureIds, string error)> RemoveCouchStructuresAsync(); // out/ref parameter method
        Task<IStructure> AddStructureAsync(string dicomType, string id); // complex method
        Task<(bool result, string error)> CanAddCouchStructuresAsync(); // out/ref parameter method
        Task<bool> CanAddStructureAsync(string dicomType, string id); // simple method
        Task<(bool result, string error)> CanRemoveCouchStructuresAsync(); // out/ref parameter method
        Task<bool> CanRemoveStructureAsync(IStructure structure); // simple method
        Task<IStructureSet> CopyAsync(); // complex method
        Task<IStructure> CreateAndSearchBodyAsync(ISearchBodyParameters parameters); // complex method
        Task DeleteAsync(); // void method
        Task<ISearchBodyParameters> GetDefaultSearchBodyParametersAsync(); // complex method
        Task RemoveStructureAsync(IStructure structure); // void method

        // --- RunAsync --- //
        /// <summary>
        /// Runs a function against the raw ESAPI VMS.TPS.Common.Model.API.StructureSet object safely on the ESAPI thread.
        /// </summary>
        Task RunAsync(Action<VMS.TPS.Common.Model.API.StructureSet> action);

        /// <summary>
        /// Runs a function against the raw ESAPI VMS.TPS.Common.Model.API.StructureSet object safely on the ESAPI thread.
        /// </summary>
        Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.StructureSet, T> func);
    }
}
