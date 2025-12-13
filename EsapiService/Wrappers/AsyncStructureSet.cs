using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VMS.TPS.Common.Model.API;
using VMS.TPS.Common.Model.Types;
using Esapi.Interfaces;
using Esapi.Services;

namespace Esapi.Wrappers
{
    public class AsyncStructureSet : AsyncApiDataObject, IStructureSet, IEsapiWrapper<VMS.TPS.Common.Model.API.StructureSet>
    {
        internal new readonly VMS.TPS.Common.Model.API.StructureSet _inner;

        // Store the inner ESAPI object reference
        // internal so other wrappers can access it
        // new to override any inherited _inner fields
        internal new readonly IEsapiService _service;

public AsyncStructureSet(VMS.TPS.Common.Model.API.StructureSet inner, IEsapiService service) : base(inner, service)
        {
            if (inner == null) throw new ArgumentNullException(nameof(inner));
            if (service == null) throw new ArgumentNullException(nameof(service));

            _inner = inner;
            _service = service;

            Structures = inner.Structures;
            ApplicationScriptLogs = inner.ApplicationScriptLogs;
            SeriesUID = inner.SeriesUID;
            UID = inner.UID;
        }

        public async Task<(bool result, IReadOnlyList<IStructure> addedStructures, bool imageResized, string error)> AddCouchStructuresAsync(string couchModel, PatientOrientation orientation, RailPosition railA, RailPosition railB, double? surfaceHU, double? interiorHU, double? railHU)
        {
            var postResult = await _service.PostAsync(context => {
                IReadOnlyList<Structure> addedStructures_temp = default(IReadOnlyList<Structure>);
                bool imageResized_temp = default(bool);
                string error_temp = default(string);
                var result = _inner.AddCouchStructures(couchModel, orientation, railA, railB, surfaceHU, interiorHU, railHU, out addedStructures_temp, out imageResized_temp, out error_temp);
                return (result, addedStructures_temp, imageResized_temp, error_temp);
            });
            return (postResult.Item1,
                    postResult.Item2?.Select(x => new AsyncStructure(x, _service)).ToList(),
                    postResult.Item3,
                    postResult.Item4);
        }


        public async Task<(bool result, IReadOnlyList<string> removedStructureIds, string error)> RemoveCouchStructuresAsync()
        {
            var postResult = await _service.PostAsync(context => {
                IReadOnlyList<string> removedStructureIds_temp = default(IReadOnlyList<string>);
                string error_temp = default(string);
                var result = _inner.RemoveCouchStructures(out removedStructureIds_temp, out error_temp);
                return (result, removedStructureIds_temp, error_temp);
            });
            return (postResult.Item1,
                    postResult.Item2,
                    postResult.Item3);
        }


        public async Task<IStructure> AddReferenceLineAsync(string name, string id, VVector[] referenceLinePoints)
        {
            return await _service.PostAsync(context => 
                _inner.AddReferenceLine(name, id, referenceLinePoints) is var result && result is null ? null : new AsyncStructure(result, _service));
        }


        public async Task<IStructure> AddStructureAsync(string dicomType, string id)
        {
            return await _service.PostAsync(context => 
                _inner.AddStructure(dicomType, id) is var result && result is null ? null : new AsyncStructure(result, _service));
        }


        public async Task<IStructure> AddStructureAsync(StructureCodeInfo code)
        {
            return await _service.PostAsync(context => 
                _inner.AddStructure(code) is var result && result is null ? null : new AsyncStructure(result, _service));
        }


        public async Task<(bool result, string error)> CanAddCouchStructuresAsync()
        {
            var postResult = await _service.PostAsync(context => {
                string error_temp = default(string);
                var result = _inner.CanAddCouchStructures(out error_temp);
                return (result, error_temp);
            });
            return (postResult.Item1,
                    postResult.Item2);
        }


        // Simple Method
        public Task<bool> CanAddStructureAsync(string dicomType, string id) => 
            _service.PostAsync(context => _inner.CanAddStructure(dicomType, id));

        public async Task<(bool result, string error)> CanRemoveCouchStructuresAsync()
        {
            var postResult = await _service.PostAsync(context => {
                string error_temp = default(string);
                var result = _inner.CanRemoveCouchStructures(out error_temp);
                return (result, error_temp);
            });
            return (postResult.Item1,
                    postResult.Item2);
        }


        // Simple Method
        public Task<bool> CanRemoveStructureAsync(IStructure structure) => 
            _service.PostAsync(context => _inner.CanRemoveStructure(((AsyncStructure)structure)._inner));

        public async Task<IStructureSet> CopyAsync()
        {
            return await _service.PostAsync(context => 
                _inner.Copy() is var result && result is null ? null : new AsyncStructureSet(result, _service));
        }


        public async Task<IStructure> CreateAndSearchBodyAsync(ISearchBodyParameters parameters)
        {
            return await _service.PostAsync(context => 
                _inner.CreateAndSearchBody(((AsyncSearchBodyParameters)parameters)._inner) is var result && result is null ? null : new AsyncStructure(result, _service));
        }


        // Simple Void Method
        public Task DeleteAsync() =>
            _service.PostAsync(context => _inner.Delete());

        public async Task<ISearchBodyParameters> GetDefaultSearchBodyParametersAsync()
        {
            return await _service.PostAsync(context => 
                _inner.GetDefaultSearchBodyParameters() is var result && result is null ? null : new AsyncSearchBodyParameters(result, _service));
        }


        // Simple Void Method
        public Task RemoveStructureAsync(IStructure structure) =>
            _service.PostAsync(context => _inner.RemoveStructure(((AsyncStructure)structure)._inner));

        public IEnumerable<Structure> Structures { get; }

        public IEnumerable<ApplicationScriptLog> ApplicationScriptLogs { get; }

        public async Task<IImage> GetImageAsync()
        {
            return await _service.PostAsync(context => 
                _inner.Image is null ? null : new AsyncImage(_inner.Image, _service));
        }

        public async Task<IPatient> GetPatientAsync()
        {
            return await _service.PostAsync(context => 
                _inner.Patient is null ? null : new AsyncPatient(_inner.Patient, _service));
        }

        public async Task<ISeries> GetSeriesAsync()
        {
            return await _service.PostAsync(context => 
                _inner.Series is null ? null : new AsyncSeries(_inner.Series, _service));
        }

        public string SeriesUID { get; }

        public string UID { get; }

        public Task RunAsync(Action<VMS.TPS.Common.Model.API.StructureSet> action) => _service.PostAsync((context) => action(_inner));
        public Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.StructureSet, T> func) => _service.PostAsync<T>((context) => func(_inner));

        public static implicit operator VMS.TPS.Common.Model.API.StructureSet(AsyncStructureSet wrapper) => wrapper._inner;

        // Internal Explicit Implementation to expose _inner safely for covariance
        VMS.TPS.Common.Model.API.StructureSet IEsapiWrapper<VMS.TPS.Common.Model.API.StructureSet>.Inner => _inner;

        /* --- Skipped Members (Not generated) ---
           - Id: Shadows member in wrapped base class
           - Name: Shadows member in wrapped base class
           - Comment: Shadows member in wrapped base class
        */
    }
}
