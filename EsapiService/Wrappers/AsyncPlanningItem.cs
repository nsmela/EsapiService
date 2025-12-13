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
    public class AsyncPlanningItem : AsyncApiDataObject, IPlanningItem, IEsapiWrapper<VMS.TPS.Common.Model.API.PlanningItem>
    {
        internal new readonly VMS.TPS.Common.Model.API.PlanningItem _inner;

        // Store the inner ESAPI object reference
        // internal so other wrappers can access it
        // new to override any inherited _inner fields
        internal new readonly IEsapiService _service;

public AsyncPlanningItem(VMS.TPS.Common.Model.API.PlanningItem inner, IEsapiService service) : base(inner, service)
        {
            if (inner == null) throw new ArgumentNullException(nameof(inner));
            if (service == null) throw new ArgumentNullException(nameof(service));

            _inner = inner;
            _service = service;

            CreationDateTime = inner.CreationDateTime;
            DoseValuePresentation = inner.DoseValuePresentation;
            StructuresSelectedForDvh = inner.StructuresSelectedForDvh;
        }

        // Simple Collection Method
        public Task<IReadOnlyList<ClinicalGoal>> GetClinicalGoalsAsync() => 
            _service.PostAsync(context => _inner.GetClinicalGoals()?.ToList());

        public async Task<IDVHData> GetDVHCumulativeDataAsync(IStructure structure, DoseValuePresentation dosePresentation, VolumePresentation volumePresentation, double binWidth)
        {
            return await _service.PostAsync(context => 
                _inner.GetDVHCumulativeData(((AsyncStructure)structure)._inner, dosePresentation, volumePresentation, binWidth) is var result && result is null ? null : new AsyncDVHData(result, _service));
        }


        // Simple Method
        public Task<DoseValue> GetDoseAtVolumeAsync(IStructure structure, double volume, VolumePresentation volumePresentation, DoseValuePresentation requestedDosePresentation) => 
            _service.PostAsync(context => _inner.GetDoseAtVolume(((AsyncStructure)structure)._inner, volume, volumePresentation, requestedDosePresentation));

        // Simple Method
        public Task<double> GetVolumeAtDoseAsync(IStructure structure, DoseValue dose, VolumePresentation requestedVolumePresentation) => 
            _service.PostAsync(context => _inner.GetVolumeAtDose(((AsyncStructure)structure)._inner, dose, requestedVolumePresentation));

        public async Task<ICourse> GetCourseAsync()
        {
            return await _service.PostAsync(context => 
                _inner.Course is null ? null : new AsyncCourse(_inner.Course, _service));
        }

        public DateTime? CreationDateTime { get; }

        public async Task<IPlanningItemDose> GetDoseAsync()
        {
            return await _service.PostAsync(context => 
                _inner.Dose is null ? null : new AsyncPlanningItemDose(_inner.Dose, _service));
        }

        public DoseValuePresentation DoseValuePresentation { get; private set; }
        public async Task SetDoseValuePresentationAsync(DoseValuePresentation value)
        {
            DoseValuePresentation = await _service.PostAsync(context => 
            {
                _inner.DoseValuePresentation = value;
                return _inner.DoseValuePresentation;
            });
        }

        public async Task<IStructureSet> GetStructureSetAsync()
        {
            return await _service.PostAsync(context => 
                _inner.StructureSet is null ? null : new AsyncStructureSet(_inner.StructureSet, _service));
        }

        public IEnumerable<Structure> StructuresSelectedForDvh { get; }

        public Task RunAsync(Action<VMS.TPS.Common.Model.API.PlanningItem> action) => _service.PostAsync((context) => action(_inner));
        public Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.PlanningItem, T> func) => _service.PostAsync<T>((context) => func(_inner));

        public static implicit operator VMS.TPS.Common.Model.API.PlanningItem(AsyncPlanningItem wrapper) => wrapper._inner;

        // Internal Explicit Implementation to expose _inner safely for covariance
        VMS.TPS.Common.Model.API.PlanningItem IEsapiWrapper<VMS.TPS.Common.Model.API.PlanningItem>.Inner => _inner;
    }
}
