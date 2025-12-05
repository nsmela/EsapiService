    using System.Threading.Tasks;
namespace EsapiService.Wrappers
{
    using System.Linq;
    using System.Collections.Generic;
    public class AsyncPlanningItem : IPlanningItem
    {
        internal readonly VMS.TPS.Common.Model.API.PlanningItem _inner;

        // Store the inner ESAPI object reference
        // internal so other wrappers can access it
        // new to override any inherited _inner fields
        internal new readonly IEsapiService _service;

        public AsyncPlanningItem(VMS.TPS.Common.Model.API.PlanningItem inner, IEsapiService service) : base(inner, service)
        {
            _inner = inner;
            _service = service;

        }

        public void WriteXml(System.Xml.XmlWriter writer) => _inner.WriteXml(writer);
        public System.Collections.Generic.IReadOnlyList<VMS.TPS.Common.Model.Types.ClinicalGoal> GetClinicalGoals() => _inner.GetClinicalGoals()?.ToList();
        public IDVHData GetDVHCumulativeData(VMS.TPS.Common.Model.API.Structure structure, VMS.TPS.Common.Model.Types.DoseValuePresentation dosePresentation, VMS.TPS.Common.Model.Types.VolumePresentation volumePresentation, double binWidth) => _inner.GetDVHCumulativeData(structure, dosePresentation, volumePresentation, binWidth) is var result && result is null ? null : new AsyncDVHData(result, _service);
        public VMS.TPS.Common.Model.Types.DoseValue GetDoseAtVolume(VMS.TPS.Common.Model.API.Structure structure, double volume, VMS.TPS.Common.Model.Types.VolumePresentation volumePresentation, VMS.TPS.Common.Model.Types.DoseValuePresentation requestedDosePresentation) => _inner.GetDoseAtVolume(structure, volume, volumePresentation, requestedDosePresentation);
        public double GetVolumeAtDose(VMS.TPS.Common.Model.API.Structure structure, VMS.TPS.Common.Model.Types.DoseValue dose, VMS.TPS.Common.Model.Types.VolumePresentation requestedVolumePresentation) => _inner.GetVolumeAtDose(structure, dose, requestedVolumePresentation);
        public ICourse Course => _inner.Course is null ? null : new AsyncCourse(_inner.Course, _service);

        public System.Collections.Generic.IReadOnlyList<System.DateTime> CreationDateTime => _inner.CreationDateTime?.ToList();
        public IPlanningItemDose Dose => _inner.Dose is null ? null : new AsyncPlanningItemDose(_inner.Dose, _service);

        public VMS.TPS.Common.Model.Types.DoseValuePresentation DoseValuePresentation => _inner.DoseValuePresentation;
        public async Task SetDoseValuePresentationAsync(VMS.TPS.Common.Model.Types.DoseValuePresentation value) => _service.RunAsync(() => _inner.DoseValuePresentation = value);
        public IStructureSet StructureSet => _inner.StructureSet is null ? null : new AsyncStructureSet(_inner.StructureSet, _service);

        public System.Collections.Generic.IReadOnlyList<IStructure> StructuresSelectedForDvh => _inner.StructuresSelectedForDvh?.Select(x => new AsyncStructure(x, _service)).ToList();

        public Task RunAsync(Action<VMS.TPS.Common.Model.API.PlanningItem> action) => _service.RunAsync(() => action(_inner));
        public Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.PlanningItem, T> func) => _service.RunAsync(() => func(_inner));
    }
}
    }
}
