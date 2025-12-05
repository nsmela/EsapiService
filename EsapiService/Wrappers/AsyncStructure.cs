    using System.Threading.Tasks;
namespace EsapiService.Wrappers
{
    public class AsyncStructure : IStructure
    {
        internal readonly VMS.TPS.Common.Model.API.Structure _inner;

        // Store the inner ESAPI object reference
        // internal so other wrappers can access it
        // new to override any inherited _inner fields
        internal new readonly IEsapiService _service;

        public AsyncStructure(VMS.TPS.Common.Model.API.Structure inner, IEsapiService service) : base(inner, service)
        {
            _inner = inner;
            _service = service;

            CenterPoint = inner.CenterPoint;
            DicomType = inner.DicomType;
            HasCalculatedPlans = inner.HasCalculatedPlans;
            HasSegment = inner.HasSegment;
            IsApproved = inner.IsApproved;
            IsEmpty = inner.IsEmpty;
            IsHighResolution = inner.IsHighResolution;
            IsTarget = inner.IsTarget;
            MeshGeometry = inner.MeshGeometry;
            ROINumber = inner.ROINumber;
            Volume = inner.Volume;
        }

        public void AddContourOnImagePlane(VVector[] contour, int z) => _inner.AddContourOnImagePlane(contour, z);
        public ISegmentVolume And(ISegmentVolume other) => _inner.And(other) is var result && result is null ? null : new AsyncSegmentVolume(result, _service);
        public ISegmentVolume AsymmetricMargin(AxisAlignedMargins margins) => _inner.AsymmetricMargin(margins) is var result && result is null ? null : new AsyncSegmentVolume(result, _service);
        public bool CanConvertToHighResolution() => _inner.CanConvertToHighResolution();
        public async System.Threading.Tasks.Task<(bool Result, string errorMessage)> CanEditSegmentVolumeAsync()
        {
            string errorMessage_temp;
            var result = await _service.RunAsync(() => _inner.CanEditSegmentVolume(out errorMessage_temp));
            return (result, errorMessage_temp);
        }
        public async System.Threading.Tasks.Task<(bool Result, string errorMessage)> CanSetAssignedHUAsync()
        {
            string errorMessage_temp;
            var result = await _service.RunAsync(() => _inner.CanSetAssignedHU(out errorMessage_temp));
            return (result, errorMessage_temp);
        }
        public void ClearAllContoursOnImagePlane(int z) => _inner.ClearAllContoursOnImagePlane(z);
        public void ConvertDoseLevelToStructure(IDose dose, DoseValue doseLevel) => _inner.ConvertDoseLevelToStructure(dose, doseLevel);
        public void ConvertToHighResolution() => _inner.ConvertToHighResolution();
        public async System.Threading.Tasks.Task<(bool Result, double huValue)> GetAssignedHUAsync()
        {
            double huValue_temp;
            var result = await _service.RunAsync(() => _inner.GetAssignedHU(out huValue_temp));
            return (result, huValue_temp);
        }
        public VVector[][] GetContoursOnImagePlane(int z) => _inner.GetContoursOnImagePlane(z);
        public int GetNumberOfSeparateParts() => _inner.GetNumberOfSeparateParts();
        public VVector[] GetReferenceLinePoints() => _inner.GetReferenceLinePoints();
        public SegmentProfile GetSegmentProfile(VVector start, VVector stop, Collections.BitArray preallocatedBuffer) => _inner.GetSegmentProfile(start, stop, preallocatedBuffer);
        public bool IsPointInsideSegment(VVector point) => _inner.IsPointInsideSegment(point);
        public ISegmentVolume Margin(double marginInMM) => _inner.Margin(marginInMM) is var result && result is null ? null : new AsyncSegmentVolume(result, _service);
        public ISegmentVolume Not() => _inner.Not() is var result && result is null ? null : new AsyncSegmentVolume(result, _service);
        public ISegmentVolume Or(ISegmentVolume other) => _inner.Or(other) is var result && result is null ? null : new AsyncSegmentVolume(result, _service);
        public bool ResetAssignedHU() => _inner.ResetAssignedHU();
        public void SetAssignedHU(double huValue) => _inner.SetAssignedHU(huValue);
        public ISegmentVolume Sub(ISegmentVolume other) => _inner.Sub(other) is var result && result is null ? null : new AsyncSegmentVolume(result, _service);
        public void SubtractContourOnImagePlane(VVector[] contour, int z) => _inner.SubtractContourOnImagePlane(contour, z);
        public ISegmentVolume Xor(ISegmentVolume other) => _inner.Xor(other) is var result && result is null ? null : new AsyncSegmentVolume(result, _service);
        public IReadOnlyList<StructureApprovalHistoryEntry> ApprovalHistory => _inner.ApprovalHistory?.ToList();
        public VVector CenterPoint { get; }
        public Windows.Media.Color Color => _inner.Color;
        public async Task SetColorAsync(Windows.Media.Color value) => _service.RunAsync(() => _inner.Color = value);
        public string DicomType { get; }
        public bool HasCalculatedPlans { get; }
        public bool HasSegment { get; }
        public bool IsApproved { get; }
        public bool IsEmpty { get; }
        public bool IsHighResolution { get; }
        public bool IsTarget { get; }
        public Windows.Media.Media3D.MeshGeometry3D MeshGeometry { get; }
        public int ROINumber { get; }
        public ISegmentVolume SegmentVolume => _inner.SegmentVolume is null ? null : new AsyncSegmentVolume(_inner.SegmentVolume, _service);
        public System.Threading.Tasks.Task SetSegmentVolumeAsync(ISegmentVolume value)
        {
            // Unwrap the interface to get the Varian object
            if (value is AsyncSegmentVolume wrapper)
            {
                 return _service.RunAsync(() => _inner.SegmentVolume = wrapper._inner);
            }
            throw new System.ArgumentException("Value must be of type AsyncSegmentVolume");
        }

        public IStructureCode StructureCode => _inner.StructureCode is null ? null : new AsyncStructureCode(_inner.StructureCode, _service);
        public System.Threading.Tasks.Task SetStructureCodeAsync(IStructureCode value)
        {
            // Unwrap the interface to get the Varian object
            if (value is AsyncStructureCode wrapper)
            {
                 return _service.RunAsync(() => _inner.StructureCode = wrapper._inner);
            }
            throw new System.ArgumentException("Value must be of type AsyncStructureCode");
        }

        public IReadOnlyList<StructureCodeInfo> StructureCodeInfos => _inner.StructureCodeInfos?.ToList();
        public double Volume { get; }

        public Task RunAsync(Action<VMS.TPS.Common.Model.API.Structure> action) => _service.RunAsync(() => action(_inner));
        public Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.Structure, T> func) => _service.RunAsync(() => func(_inner));
    }
}
    }
}
