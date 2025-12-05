    using System.Threading.Tasks;
namespace EsapiService.Wrappers
{
    using System.Linq;
    using System.Collections.Generic;
    public class AsyncStructureSet : IStructureSet
    {
        internal readonly VMS.TPS.Common.Model.API.StructureSet _inner;

        // Store the inner ESAPI object reference
        // internal so other wrappers can access it
        // new to override any inherited _inner fields
        internal new readonly IEsapiService _service;

        public AsyncStructureSet(VMS.TPS.Common.Model.API.StructureSet inner, IEsapiService service) : base(inner, service)
        {
            _inner = inner;
            _service = service;

            SeriesUID = inner.SeriesUID;
            UID = inner.UID;
        }

        public async System.Threading.Tasks.Task<(bool Result, System.Collections.Generic.IReadOnlyList<VMS.TPS.Common.Model.API.Structure> addedStructures, bool imageResized, string error)> AddCouchStructuresAsync(string couchModel, VMS.TPS.Common.Model.Types.PatientOrientation orientation, VMS.TPS.Common.Model.Types.RailPosition railA, VMS.TPS.Common.Model.Types.RailPosition railB, System.Nullable<double> surfaceHU, System.Nullable<double> interiorHU, System.Nullable<double> railHU)
        {
            System.Collections.Generic.IReadOnlyList<VMS.TPS.Common.Model.API.Structure> addedStructures_temp;
            bool imageResized_temp;
            string error_temp;
            var result = await _service.RunAsync(() => _inner.AddCouchStructures(couchModel, orientation, railA, railB, surfaceHU, interiorHU, railHU, out addedStructures_temp, out imageResized_temp, out error_temp));
            return (result, addedStructures_temp, imageResized_temp, error_temp);
        }
        public async System.Threading.Tasks.Task<(bool Result, System.Collections.Generic.IReadOnlyList<string> removedStructureIds, string error)> RemoveCouchStructuresAsync()
        {
            System.Collections.Generic.IReadOnlyList<string> removedStructureIds_temp;
            string error_temp;
            var result = await _service.RunAsync(() => _inner.RemoveCouchStructures(out removedStructureIds_temp, out error_temp));
            return (result, removedStructureIds_temp, error_temp);
        }
        public void WriteXml(System.Xml.XmlWriter writer) => _inner.WriteXml(writer);
        public IStructure AddReferenceLine(string name, string id, VMS.TPS.Common.Model.Types.VVector[] referenceLinePoints) => _inner.AddReferenceLine(name, id, referenceLinePoints) is var result && result is null ? null : new AsyncStructure(result, _service);
        public IStructure AddStructure(string dicomType, string id) => _inner.AddStructure(dicomType, id) is var result && result is null ? null : new AsyncStructure(result, _service);
        public IStructure AddStructure(VMS.TPS.Common.Model.Types.StructureCodeInfo code) => _inner.AddStructure(code) is var result && result is null ? null : new AsyncStructure(result, _service);
        public async System.Threading.Tasks.Task<(bool Result, string error)> CanAddCouchStructuresAsync()
        {
            string error_temp;
            var result = await _service.RunAsync(() => _inner.CanAddCouchStructures(out error_temp));
            return (result, error_temp);
        }
        public bool CanAddStructure(string dicomType, string id) => _inner.CanAddStructure(dicomType, id);
        public async System.Threading.Tasks.Task<(bool Result, string error)> CanRemoveCouchStructuresAsync()
        {
            string error_temp;
            var result = await _service.RunAsync(() => _inner.CanRemoveCouchStructures(out error_temp));
            return (result, error_temp);
        }
        public bool CanRemoveStructure(VMS.TPS.Common.Model.API.Structure structure) => _inner.CanRemoveStructure(structure);
        public IStructureSet Copy() => _inner.Copy() is var result && result is null ? null : new AsyncStructureSet(result, _service);
        public IStructure CreateAndSearchBody(VMS.TPS.Common.Model.API.SearchBodyParameters parameters) => _inner.CreateAndSearchBody(parameters) is var result && result is null ? null : new AsyncStructure(result, _service);
        public void Delete() => _inner.Delete();
        public ISearchBodyParameters GetDefaultSearchBodyParameters() => _inner.GetDefaultSearchBodyParameters() is var result && result is null ? null : new AsyncSearchBodyParameters(result, _service);
        public void RemoveStructure(VMS.TPS.Common.Model.API.Structure structure) => _inner.RemoveStructure(structure);
        public System.Collections.Generic.IReadOnlyList<IStructure> Structures => _inner.Structures?.Select(x => new AsyncStructure(x, _service)).ToList();
        public string Id => _inner.Id;
        public async Task SetIdAsync(string value) => _service.RunAsync(() => _inner.Id = value);
        public string Name => _inner.Name;
        public async Task SetNameAsync(string value) => _service.RunAsync(() => _inner.Name = value);
        public string Comment => _inner.Comment;
        public async Task SetCommentAsync(string value) => _service.RunAsync(() => _inner.Comment = value);
        public System.Collections.Generic.IReadOnlyList<IApplicationScriptLog> ApplicationScriptLogs => _inner.ApplicationScriptLogs?.Select(x => new AsyncApplicationScriptLog(x, _service)).ToList();
        public IImage Image => _inner.Image is null ? null : new AsyncImage(_inner.Image, _service);

        public IPatient Patient => _inner.Patient is null ? null : new AsyncPatient(_inner.Patient, _service);

        public ISeries Series => _inner.Series is null ? null : new AsyncSeries(_inner.Series, _service);

        public string SeriesUID { get; }
        public string UID { get; }

        public Task RunAsync(Action<VMS.TPS.Common.Model.API.StructureSet> action) => _service.RunAsync(() => action(_inner));
        public Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.StructureSet, T> func) => _service.RunAsync(() => func(_inner));
    }
}
    }
}
