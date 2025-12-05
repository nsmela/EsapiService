namespace EsapiService.Wrappers
{
    using System.Linq;
    using System.Collections.Generic;
    public class AsyncApplication : IApplication
    {
        internal readonly VMS.TPS.Common.Model.API.Application _inner;

        // Store the inner ESAPI object reference
        // internal so other wrappers can access it
        // new to override any inherited _inner fields
        internal new readonly IEsapiService _service;

        public AsyncApplication(VMS.TPS.Common.Model.API.Application inner, IEsapiService service) : base(inner, service)
        {
            _inner = inner;
            _service = service;

            SiteProgramDataDir = inner.SiteProgramDataDir;
        }

        public void Dispose() => _inner.Dispose();
        public IPatient OpenPatient(VMS.TPS.Common.Model.API.PatientSummary patientSummary) => _inner.OpenPatient(patientSummary) is var result && result is null ? null : new AsyncPatient(result, _service);
        public IPatient OpenPatientById(string id) => _inner.OpenPatientById(id) is var result && result is null ? null : new AsyncPatient(result, _service);
        public void ClosePatient() => _inner.ClosePatient();
        public void SaveModifications() => _inner.SaveModifications();
        public void WriteXml(System.Xml.XmlWriter writer) => _inner.WriteXml(writer);
        public IUser CurrentUser => _inner.CurrentUser is null ? null : new AsyncUser(_inner.CurrentUser, _service);

        public string SiteProgramDataDir { get; }
        public System.Collections.Generic.IReadOnlyList<IPatientSummary> PatientSummaries => _inner.PatientSummaries?.Select(x => new AsyncPatientSummary(x, _service)).ToList();
        public ICalculation Calculation => _inner.Calculation is null ? null : new AsyncCalculation(_inner.Calculation, _service);

        public IActiveStructureCodeDictionaries StructureCodes => _inner.StructureCodes is null ? null : new AsyncActiveStructureCodeDictionaries(_inner.StructureCodes, _service);

        public IEquipment Equipment => _inner.Equipment is null ? null : new AsyncEquipment(_inner.Equipment, _service);

        public IScriptEnvironment ScriptEnvironment => _inner.ScriptEnvironment is null ? null : new AsyncScriptEnvironment(_inner.ScriptEnvironment, _service);

    }
}
