    using System.Threading.Tasks;
namespace EsapiService.Wrappers
{
    using System.Linq;
    using System.Collections.Generic;
    public class AsyncBrachyPlanSetup : IBrachyPlanSetup
    {
        internal readonly VMS.TPS.Common.Model.API.BrachyPlanSetup _inner;

        // Store the inner ESAPI object reference
        // internal so other wrappers can access it
        // new to override any inherited _inner fields
        internal new readonly IEsapiService _service;

        public AsyncBrachyPlanSetup(VMS.TPS.Common.Model.API.BrachyPlanSetup inner, IEsapiService service) : base(inner, service)
        {
            _inner = inner;
            _service = service;

            ApplicationSetupType = inner.ApplicationSetupType;
            TreatmentTechnique = inner.TreatmentTechnique;
        }

        public void WriteXml(System.Xml.XmlWriter writer) => _inner.WriteXml(writer);
        public ICatheter AddCatheter(string catheterId, VMS.TPS.Common.Model.API.BrachyTreatmentUnit treatmentUnit, System.Text.StringBuilder outputDiagnostics, bool appendChannelNumToId, int channelNum) => _inner.AddCatheter(catheterId, treatmentUnit, outputDiagnostics, appendChannelNumToId, channelNum) is var result && result is null ? null : new AsyncCatheter(result, _service);
        public void AddLocationToExistingReferencePoint(VMS.TPS.Common.Model.Types.VVector location, VMS.TPS.Common.Model.API.ReferencePoint referencePoint) => _inner.AddLocationToExistingReferencePoint(location, referencePoint);
        public IReferencePoint AddReferencePoint(bool target, string id) => _inner.AddReferencePoint(target, id) is var result && result is null ? null : new AsyncReferencePoint(result, _service);
        public VMS.TPS.Common.Model.Types.DoseProfile CalculateAccurateTG43DoseProfile(VMS.TPS.Common.Model.Types.VVector start, VMS.TPS.Common.Model.Types.VVector stop, double[] preallocatedBuffer) => _inner.CalculateAccurateTG43DoseProfile(start, stop, preallocatedBuffer);
        public async System.Threading.Tasks.Task<(VMS.TPS.Common.Model.Types.ChangeBrachyTreatmentUnitResult Result, System.Collections.Generic.List<string> messages)> ChangeTreatmentUnitAsync(IBrachyTreatmentUnit treatmentUnit, bool keepDoseIntact)
        {
            System.Collections.Generic.List<string> messages_temp;
            var result = await _service.RunAsync(() => _inner.ChangeTreatmentUnit(treatmentUnit._inner, keepDoseIntact, out messages_temp));
            return (result, messages_temp);
        }
        public ICalculateBrachy3DDoseResult CalculateTG43Dose() => _inner.CalculateTG43Dose() is var result && result is null ? null : new AsyncCalculateBrachy3DDoseResult(result, _service);
        public string ApplicationSetupType { get; }
        public VMS.TPS.Common.Model.Types.BrachyTreatmentTechniqueType BrachyTreatmentTechnique => _inner.BrachyTreatmentTechnique;
        public async Task SetBrachyTreatmentTechniqueAsync(VMS.TPS.Common.Model.Types.BrachyTreatmentTechniqueType value) => _service.RunAsync(() => _inner.BrachyTreatmentTechnique = value);
        public System.Collections.Generic.IReadOnlyList<ICatheter> Catheters => _inner.Catheters?.Select(x => new AsyncCatheter(x, _service)).ToList();
        public System.Collections.Generic.IReadOnlyList<int> NumberOfPdrPulses => _inner.NumberOfPdrPulses?.ToList();
        public System.Collections.Generic.IReadOnlyList<double> PdrPulseInterval => _inner.PdrPulseInterval?.ToList();
        public System.Collections.Generic.IReadOnlyList<IStructure> ReferenceLines => _inner.ReferenceLines?.Select(x => new AsyncStructure(x, _service)).ToList();
        public System.Collections.Generic.IReadOnlyList<ISeedCollection> SeedCollections => _inner.SeedCollections?.Select(x => new AsyncSeedCollection(x, _service)).ToList();
        public System.Collections.Generic.IReadOnlyList<IBrachySolidApplicator> SolidApplicators => _inner.SolidApplicators?.Select(x => new AsyncBrachySolidApplicator(x, _service)).ToList();
        public string TreatmentTechnique { get; }
        public System.Collections.Generic.IReadOnlyList<System.DateTime> TreatmentDateTime => _inner.TreatmentDateTime?.ToList();
    }
}
