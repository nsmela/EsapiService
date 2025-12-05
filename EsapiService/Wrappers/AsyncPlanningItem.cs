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

        public IReadOnlyList<ClinicalGoal> GetClinicalGoals() => _inner.GetClinicalGoals()?.ToList();
        public IDVHData GetDVHCumulativeData(IStructure structure, DoseValuePresentation dosePresentation, VolumePresentation volumePresentation, double binWidth) => _inner.GetDVHCumulativeData(structure, dosePresentation, volumePresentation, binWidth) is var result && result is null ? null : new AsyncDVHData(result, _service);
        public DoseValue GetDoseAtVolume(IStructure structure, double volume, VolumePresentation volumePresentation, DoseValuePresentation requestedDosePresentation) => _inner.GetDoseAtVolume(structure, volume, volumePresentation, requestedDosePresentation);
        public double GetVolumeAtDose(IStructure structure, DoseValue dose, VolumePresentation requestedVolumePresentation) => _inner.GetVolumeAtDose(structure, dose, requestedVolumePresentation);
        public ICourse Course => _inner.Course is null ? null : new AsyncCourse(_inner.Course, _service);

        public IReadOnlyList<DateTime> CreationDateTime => _inner.CreationDateTime?.ToList();
        public IPlanningItemDose Dose => _inner.Dose is null ? null : new AsyncPlanningItemDose(_inner.Dose, _service);

        public DoseValuePresentation DoseValuePresentation => _inner.DoseValuePresentation;
        public async Task SetDoseValuePresentationAsync(DoseValuePresentation value) => _service.RunAsync(() => _inner.DoseValuePresentation = value);
        public IStructureSet StructureSet => _inner.StructureSet is null ? null : new AsyncStructureSet(_inner.StructureSet, _service);

        public IReadOnlyList<IStructure> StructuresSelectedForDvh => _inner.StructuresSelectedForDvh?.Select(x => new AsyncStructure(x, _service)).ToList();

        public Task RunAsync(Action<VMS.TPS.Common.Model.API.PlanningItem> action) => _service.RunAsync(() => action(_inner));
        public Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.PlanningItem, T> func) => _service.RunAsync(() => func(_inner));
    }
}
    }
}
