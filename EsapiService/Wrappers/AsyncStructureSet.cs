using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;
using VMS.TPS.Common.Model.API;
using VMS.TPS.Common.Model.Types;

namespace EsapiService.Wrappers
{
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

        public async System.Threading.Tasks.Task<(bool Result, IReadOnlyList<IStructure> addedStructures, bool imageResized, string error)> AddCouchStructuresAsync(string couchModel, PatientOrientation orientation, RailPosition railA, RailPosition railB, Nullable<double> surfaceHU, Nullable<double> interiorHU, Nullable<double> railHU)
        {
            IReadOnlyList<Structure> addedStructures_temp;
            bool imageResized_temp;
            string error_temp;
            var result = await _service.RunAsync(() => _inner.AddCouchStructures(couchModel, orientation, railA, railB, surfaceHU, interiorHU, railHU, out addedStructures_temp, out imageResized_temp, out error_temp));
            return (result, addedStructures_temp is null ? null : new IReadOnlyList<AsyncStructure>(addedStructures_temp, _service), imageResized_temp, error_temp);
        }
        public async System.Threading.Tasks.Task<(bool Result, IReadOnlyList<string> removedStructureIds, string error)> RemoveCouchStructuresAsync()
        {
            IReadOnlyList<string> removedStructureIds_temp;
            string error_temp;
            var result = await _service.RunAsync(() => _inner.RemoveCouchStructures(out removedStructureIds_temp, out error_temp));
            return (result, removedStructureIds_temp, error_temp);
        }
        public async Task<IStructure> AddReferenceLineAsync(string name, string id, VVector[] referenceLinePoints)
        {
            return await _service.RunAsync(() => 
                _inner.AddReferenceLine(name, id, referenceLinePoints) is var result && result is null ? null : new AsyncStructure(result, _service));
        }

        public async Task<IStructure> AddStructureAsync(string dicomType, string id)
        {
            return await _service.RunAsync(() => 
                _inner.AddStructure(dicomType, id) is var result && result is null ? null : new AsyncStructure(result, _service));
        }

        public async Task<IStructure> AddStructureAsync(StructureCodeInfo code)
        {
            return await _service.RunAsync(() => 
                _inner.AddStructure(code) is var result && result is null ? null : new AsyncStructure(result, _service));
        }

        public async System.Threading.Tasks.Task<(bool Result, string error)> CanAddCouchStructuresAsync()
        {
            string error_temp;
            var result = await _service.RunAsync(() => _inner.CanAddCouchStructures(out error_temp));
            return (result, error_temp);
        }
        public Task<bool> CanAddStructureAsync(string dicomType, string id) => _service.RunAsync(() => _inner.CanAddStructure(dicomType, id));
        public async System.Threading.Tasks.Task<(bool Result, string error)> CanRemoveCouchStructuresAsync()
        {
            string error_temp;
            var result = await _service.RunAsync(() => _inner.CanRemoveCouchStructures(out error_temp));
            return (result, error_temp);
        }
        public Task<bool> CanRemoveStructureAsync(IStructure structure) => _service.RunAsync(() => _inner.CanRemoveStructure(structure));
        public async Task<IStructureSet> CopyAsync()
        {
            return await _service.RunAsync(() => 
                _inner.Copy() is var result && result is null ? null : new AsyncStructureSet(result, _service));
        }

        public async Task<IStructure> CreateAndSearchBodyAsync(ISearchBodyParameters parameters)
        {
            return await _service.RunAsync(() => 
                _inner.CreateAndSearchBody(parameters) is var result && result is null ? null : new AsyncStructure(result, _service));
        }

        public Task DeleteAsync() => _service.RunAsync(() => _inner.Delete());
        public async Task<ISearchBodyParameters> GetDefaultSearchBodyParametersAsync()
        {
            return await _service.RunAsync(() => 
                _inner.GetDefaultSearchBodyParameters() is var result && result is null ? null : new AsyncSearchBodyParameters(result, _service));
        }

        public Task RemoveStructureAsync(IStructure structure) => _service.RunAsync(() => _inner.RemoveStructure(structure));
        public async Task<IReadOnlyList<IStructure>> GetStructuresAsync()
        {
            return await _service.RunAsync(() => 
                _inner.Structures?.Select(x => new AsyncStructure(x, _service)).ToList());
        }

        public async Task<IReadOnlyList<IApplicationScriptLog>> GetApplicationScriptLogsAsync()
        {
            return await _service.RunAsync(() => 
                _inner.ApplicationScriptLogs?.Select(x => new AsyncApplicationScriptLog(x, _service)).ToList());
        }

        public async Task<IImage> GetImageAsync()
        {
            return await _service.RunAsync(() => 
                _inner.Image is null ? null : new AsyncImage(_inner.Image, _service));
        }
        public async Task<IPatient> GetPatientAsync()
        {
            return await _service.RunAsync(() => 
                _inner.Patient is null ? null : new AsyncPatient(_inner.Patient, _service));
        }
        public async Task<ISeries> GetSeriesAsync()
        {
            return await _service.RunAsync(() => 
                _inner.Series is null ? null : new AsyncSeries(_inner.Series, _service));
        }
        public string SeriesUID { get; }
        public string UID { get; }

        public Task RunAsync(Action<VMS.TPS.Common.Model.API.StructureSet> action) => _service.RunAsync(() => action(_inner));
        public Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.StructureSet, T> func) => _service.RunAsync(() => func(_inner));
    }
}
    }
}
