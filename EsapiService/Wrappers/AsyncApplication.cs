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
    public class AsyncApplication : AsyncSerializableObject, IApplication, IEsapiWrapper<VMS.TPS.Common.Model.API.Application>
    {
        internal new readonly VMS.TPS.Common.Model.API.Application _inner;

        // Store the inner ESAPI object reference
        // internal so other wrappers can access it
        // new to override any inherited _inner fields
        internal new readonly IEsapiService _service;

public AsyncApplication(VMS.TPS.Common.Model.API.Application inner, IEsapiService service) : base(inner, service)
        {
            if (inner == null) throw new ArgumentNullException(nameof(inner));
            if (service == null) throw new ArgumentNullException(nameof(service));

            _inner = inner;
            _service = service;

            PatientSummaries = inner.PatientSummaries;
        }

        // Simple Void Method
        public Task DisposeAsync() =>
            _service.PostAsync(context => _inner.Dispose());

        public async Task<IPatient> OpenPatientAsync(IPatientSummary patientSummary)
        {
            return await _service.PostAsync(context => 
                _inner.OpenPatient(((AsyncPatientSummary)patientSummary)._inner) is var result && result is null ? null : new AsyncPatient(result, _service));
        }


        public async Task<IPatient> OpenPatientByIdAsync(string id)
        {
            return await _service.PostAsync(context => 
                _inner.OpenPatientById(id) is var result && result is null ? null : new AsyncPatient(result, _service));
        }


        // Simple Void Method
        public Task ClosePatientAsync() =>
            _service.PostAsync(context => _inner.ClosePatient());

        // Simple Void Method
        public Task SaveModificationsAsync() =>
            _service.PostAsync(context => _inner.SaveModifications());

        public async Task<IUser> GetCurrentUserAsync()
        {
            return await _service.PostAsync(context => 
                _inner.CurrentUser is null ? null : new AsyncUser(_inner.CurrentUser, _service));
        }

        public IEnumerable<PatientSummary> PatientSummaries { get; }

        public async Task<ICalculation> GetCalculationAsync()
        {
            return await _service.PostAsync(context => 
                _inner.Calculation is null ? null : new AsyncCalculation(_inner.Calculation, _service));
        }

        public async Task<IActiveStructureCodeDictionaries> GetStructureCodesAsync()
        {
            return await _service.PostAsync(context => 
                _inner.StructureCodes is null ? null : new AsyncActiveStructureCodeDictionaries(_inner.StructureCodes, _service));
        }

        public async Task<IEquipment> GetEquipmentAsync()
        {
            return await _service.PostAsync(context => 
                _inner.Equipment is null ? null : new AsyncEquipment(_inner.Equipment, _service));
        }

        public async Task<IScriptEnvironment> GetScriptEnvironmentAsync()
        {
            return await _service.PostAsync(context => 
                _inner.ScriptEnvironment is null ? null : new AsyncScriptEnvironment(_inner.ScriptEnvironment, _service));
        }

        public Task RunAsync(Action<VMS.TPS.Common.Model.API.Application> action) => _service.PostAsync((context) => action(_inner));
        public Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.Application, T> func) => _service.PostAsync<T>((context) => func(_inner));

        public static implicit operator VMS.TPS.Common.Model.API.Application(AsyncApplication wrapper) => wrapper._inner;

        // Internal Explicit Implementation to expose _inner safely for covariance
        VMS.TPS.Common.Model.API.Application IEsapiWrapper<VMS.TPS.Common.Model.API.Application>.Inner => _inner;
    }
}
