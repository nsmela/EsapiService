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
    public class AsyncStructureSet : AsyncApiDataObject, IStructureSet
    {
        internal new readonly VMS.TPS.Common.Model.API.StructureSet _inner;

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


        public async Task<(bool Result, IReadOnlyList<string> removedStructureIds, string error)> RemoveCouchStructuresAsync()
        {
            IReadOnlyList<string> removedStructureIds_temp = default(IReadOnlyList<string>);
            string error_temp = default(string);
            var result = await _service.PostAsync(context => _inner.RemoveCouchStructures(out removedStructureIds_temp, out error_temp));
            return (result, removedStructureIds_temp, error_temp);
        }

        public async Task<IStructure> AddStructureAsync(string dicomType, string id)
        {
            return await _service.PostAsync(context => 
                _inner.AddStructure(dicomType, id) is var result && result is null ? null : new AsyncStructure(result, _service));
        }


        public async Task<(bool Result, string error)> CanAddCouchStructuresAsync()
        {
            string error_temp = default(string);
            var result = await _service.PostAsync(context => _inner.CanAddCouchStructures(out error_temp));
            return (result, error_temp);
        }

        public Task<bool> CanAddStructureAsync(string dicomType, string id) => _service.PostAsync(context => _inner.CanAddStructure(dicomType, id));

        public async Task<(bool Result, string error)> CanRemoveCouchStructuresAsync()
        {
            string error_temp = default(string);
            var result = await _service.PostAsync(context => _inner.CanRemoveCouchStructures(out error_temp));
            return (result, error_temp);
        }

        public Task<bool> CanRemoveStructureAsync(IStructure structure) => _service.PostAsync(context => _inner.CanRemoveStructure(((AsyncStructure)structure)._inner));

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


        public Task DeleteAsync() => _service.PostAsync(context => _inner.Delete());

        public async Task<ISearchBodyParameters> GetDefaultSearchBodyParametersAsync()
        {
            return await _service.PostAsync(context => 
                _inner.GetDefaultSearchBodyParameters() is var result && result is null ? null : new AsyncSearchBodyParameters(result, _service));
        }


        public Task RemoveStructureAsync(IStructure structure) => _service.PostAsync(context => _inner.RemoveStructure(((AsyncStructure)structure)._inner));

        public async Task<IReadOnlyList<IStructure>> GetStructuresAsync()
        {
            return await _service.PostAsync(context => 
                _inner.Structures?.Select(x => new AsyncStructure(x, _service)).ToList());
        }


        public async Task<IReadOnlyList<IApplicationScriptLog>> GetApplicationScriptLogsAsync()
        {
            return await _service.PostAsync(context => 
                _inner.ApplicationScriptLogs?.Select(x => new AsyncApplicationScriptLog(x, _service)).ToList());
        }


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
    }
}
