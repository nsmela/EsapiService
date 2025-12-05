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
        Task<(bool Result, System.Collections.Generic.IReadOnlyList<VMS.TPS.Common.Model.API.Structure> addedStructures, bool imageResized, string error)> AddCouchStructuresAsync(string couchModel, VMS.TPS.Common.Model.Types.PatientOrientation orientation, VMS.TPS.Common.Model.Types.RailPosition railA, VMS.TPS.Common.Model.Types.RailPosition railB, System.Nullable<double> surfaceHU, System.Nullable<double> interiorHU, System.Nullable<double> railHU);
        Task<(bool Result, System.Collections.Generic.IReadOnlyList<string> removedStructureIds, string error)> RemoveCouchStructuresAsync();
        Task WriteXmlAsync(System.Xml.XmlWriter writer);
        Task<IStructure> AddReferenceLineAsync(string name, string id, VMS.TPS.Common.Model.Types.VVector[] referenceLinePoints);
        Task<IStructure> AddStructureAsync(string dicomType, string id);
        Task<IStructure> AddStructureAsync(VMS.TPS.Common.Model.Types.StructureCodeInfo code);
        Task<(bool Result, string error)> CanAddCouchStructuresAsync();
        Task<bool> CanAddStructureAsync(string dicomType, string id);
        Task<(bool Result, string error)> CanRemoveCouchStructuresAsync();
        Task<bool> CanRemoveStructureAsync(VMS.TPS.Common.Model.API.Structure structure);
        Task<IStructureSet> CopyAsync();
        Task<IStructure> CreateAndSearchBodyAsync(VMS.TPS.Common.Model.API.SearchBodyParameters parameters);
        Task DeleteAsync();
        Task<ISearchBodyParameters> GetDefaultSearchBodyParametersAsync();
        Task RemoveStructureAsync(VMS.TPS.Common.Model.API.Structure structure);
        System.Collections.Generic.IReadOnlyList<IStructure> Structures { get; }
        string Id { get; }
        Task SetIdAsync(string value);
        string Name { get; }
        Task SetNameAsync(string value);
        string Comment { get; }
        Task SetCommentAsync(string value);
        System.Collections.Generic.IReadOnlyList<IApplicationScriptLog> ApplicationScriptLogs { get; }
        Task<IImage> GetImageAsync();
        Task<IPatient> GetPatientAsync();
        Task<ISeries> GetSeriesAsync();
        string SeriesUID { get; }
        string UID { get; }

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
