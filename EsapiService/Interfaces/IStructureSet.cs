using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Media;
using VMS.TPS.Common.Model.API;
using VMS.TPS.Common.Model.Types;
using Esapi.Services;
using Esapi.Interfaces;

namespace Esapi.Interfaces
{
    public interface IStructureSet : IApiDataObject
    {
        // --- Simple Properties --- //
        string SeriesUID { get; }
        string UID { get; }

        // --- Accessors --- //
        Task<IImage> GetImageAsync();
        Task<IPatient> GetPatientAsync();
        Task<ISeries> GetSeriesAsync();

        // --- Collections --- //
        Task<IReadOnlyList<IStructure>> GetStructuresAsync();
        Task<IReadOnlyList<IApplicationScriptLog>> GetApplicationScriptLogsAsync();

        // --- Methods --- //
        Task<(bool Result, IReadOnlyList<IStructure> addedStructures, bool imageResized, string error)> AddCouchStructuresAsync(string couchModel, PatientOrientation orientation, RailPosition railA, RailPosition railB, Nullable<double> surfaceHU, Nullable<double> interiorHU, Nullable<double> railHU);
        Task<(bool Result, IReadOnlyList<string> removedStructureIds, string error)> RemoveCouchStructuresAsync();
        Task<IStructure> AddReferenceLineAsync(string name, string id, VVector[] referenceLinePoints);
        Task<IStructure> AddStructureAsync(string dicomType, string id);
        Task<IStructure> AddStructureAsync(StructureCodeInfo code);
        Task<(bool Result, string error)> CanAddCouchStructuresAsync();
        Task<bool> CanAddStructureAsync(string dicomType, string id);
        Task<(bool Result, string error)> CanRemoveCouchStructuresAsync();
        Task<bool> CanRemoveStructureAsync(IStructure structure);
        Task<IStructureSet> CopyAsync();
        Task<IStructure> CreateAndSearchBodyAsync(ISearchBodyParameters parameters);
        Task DeleteAsync();
        Task<ISearchBodyParameters> GetDefaultSearchBodyParametersAsync();
        Task RemoveStructureAsync(IStructure structure);

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
