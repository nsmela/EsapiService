using System.Threading.Tasks;
using VMS.TPS.Common.Model.API;
using VMS.TPS.Common.Model.Types;

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
            Color = inner.Color;
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


        public Task AddContourOnImagePlaneAsync(VVector[] contour, int z) => _service.RunAsync(() => _inner.AddContourOnImagePlane(contour, z));

        public async Task<ISegmentVolume> AndAsync(ISegmentVolume other)
        {
            return await _service.RunAsync(() => 
                _inner.And(other) is var result && result is null ? null : new AsyncSegmentVolume(result, _service));
        }


        public async Task<ISegmentVolume> AsymmetricMarginAsync(AxisAlignedMargins margins)
        {
            return await _service.RunAsync(() => 
                _inner.AsymmetricMargin(margins) is var result && result is null ? null : new AsyncSegmentVolume(result, _service));
        }


        public Task<bool> CanConvertToHighResolutionAsync() => _service.RunAsync(() => _inner.CanConvertToHighResolution());

        public async Task<(bool Result, string errorMessage)> CanEditSegmentVolumeAsync()
        {
            string errorMessage_temp;
            var result = await _service.RunAsync(() => _inner.CanEditSegmentVolume(out errorMessage_temp));
            return (result, errorMessage_temp);
        }

        public async Task<(bool Result, string errorMessage)> CanSetAssignedHUAsync()
        {
            string errorMessage_temp;
            var result = await _service.RunAsync(() => _inner.CanSetAssignedHU(out errorMessage_temp));
            return (result, errorMessage_temp);
        }

        public Task ClearAllContoursOnImagePlaneAsync(int z) => _service.RunAsync(() => _inner.ClearAllContoursOnImagePlane(z));

        public Task ConvertDoseLevelToStructureAsync(IDose dose, DoseValue doseLevel) => _service.RunAsync(() => _inner.ConvertDoseLevelToStructure(dose, doseLevel));

        public Task ConvertToHighResolutionAsync() => _service.RunAsync(() => _inner.ConvertToHighResolution());

        public async Task<(bool Result, double huValue)> GetAssignedHUAsync()
        {
            double huValue_temp;
            var result = await _service.RunAsync(() => _inner.GetAssignedHU(out huValue_temp));
            return (result, huValue_temp);
        }

        public Task<VVector[][]> GetContoursOnImagePlaneAsync(int z) => _service.RunAsync(() => _inner.GetContoursOnImagePlane(z));

        public Task<int> GetNumberOfSeparatePartsAsync() => _service.RunAsync(() => _inner.GetNumberOfSeparateParts());

        public Task<VVector[]> GetReferenceLinePointsAsync() => _service.RunAsync(() => _inner.GetReferenceLinePoints());

        public Task<SegmentProfile> GetSegmentProfileAsync(VVector start, VVector stop, Collections.BitArray preallocatedBuffer) => _service.RunAsync(() => _inner.GetSegmentProfile(start, stop, preallocatedBuffer));

        public Task<bool> IsPointInsideSegmentAsync(VVector point) => _service.RunAsync(() => _inner.IsPointInsideSegment(point));

        public async Task<ISegmentVolume> MarginAsync(double marginInMM)
        {
            return await _service.RunAsync(() => 
                _inner.Margin(marginInMM) is var result && result is null ? null : new AsyncSegmentVolume(result, _service));
        }


        public async Task<ISegmentVolume> NotAsync()
        {
            return await _service.RunAsync(() => 
                _inner.Not() is var result && result is null ? null : new AsyncSegmentVolume(result, _service));
        }


        public async Task<ISegmentVolume> OrAsync(ISegmentVolume other)
        {
            return await _service.RunAsync(() => 
                _inner.Or(other) is var result && result is null ? null : new AsyncSegmentVolume(result, _service));
        }


        public Task<bool> ResetAssignedHUAsync() => _service.RunAsync(() => _inner.ResetAssignedHU());

        public Task SetAssignedHUAsync(double huValue) => _service.RunAsync(() => _inner.SetAssignedHU(huValue));

        public async Task<ISegmentVolume> SubAsync(ISegmentVolume other)
        {
            return await _service.RunAsync(() => 
                _inner.Sub(other) is var result && result is null ? null : new AsyncSegmentVolume(result, _service));
        }


        public Task SubtractContourOnImagePlaneAsync(VVector[] contour, int z) => _service.RunAsync(() => _inner.SubtractContourOnImagePlane(contour, z));

        public async Task<ISegmentVolume> XorAsync(ISegmentVolume other)
        {
            return await _service.RunAsync(() => 
                _inner.Xor(other) is var result && result is null ? null : new AsyncSegmentVolume(result, _service));
        }


        public async Task<IReadOnlyList<StructureApprovalHistoryEntry>> GetApprovalHistoryAsync()
        {
            return await _service.RunAsync(() => _inner.ApprovalHistory?.ToList());
        }


        public VVector CenterPoint { get; }

        public Windows.Media.Color Color { get; private set; }
        public async Task SetColorAsync(Windows.Media.Color value)
        {
            Color = await _service.RunAsync(() =>
            {
                _inner.Color = value;
                return _inner.Color;
            });
        }

        public string DicomType { get; }

        public bool HasCalculatedPlans { get; }

        public bool HasSegment { get; }

        public bool IsApproved { get; }

        public bool IsEmpty { get; }

        public bool IsHighResolution { get; }

        public bool IsTarget { get; }

        public Windows.Media.Media3D.MeshGeometry3D MeshGeometry { get; }

        public int ROINumber { get; }

        public async Task<ISegmentVolume> GetSegmentVolumeAsync()
        {
            return await _service.RunAsync(() => 
                _inner.SegmentVolume is null ? null : new AsyncSegmentVolume(_inner.SegmentVolume, _service));
        }

        public async Task SetSegmentVolumeAsync(ISegmentVolume value)
        {
            // Handle null assignment
            if (value is null)
            {
                await _service.RunAsync(() => _inner.SegmentVolume = null);
                return;
            }
            // Unwrap the interface to get the Varian object
            if (value is AsyncSegmentVolume wrapper)
            {
                 await _service.RunAsync(() => _inner.SegmentVolume = wrapper._inner);
                 return;
            }
            throw new System.ArgumentException("Value must be of type AsyncSegmentVolume");
        }

        public async Task<IStructureCode> GetStructureCodeAsync()
        {
            return await _service.RunAsync(() => 
                _inner.StructureCode is null ? null : new AsyncStructureCode(_inner.StructureCode, _service));
        }

        public async Task SetStructureCodeAsync(IStructureCode value)
        {
            // Handle null assignment
            if (value is null)
            {
                await _service.RunAsync(() => _inner.StructureCode = null);
                return;
            }
            // Unwrap the interface to get the Varian object
            if (value is AsyncStructureCode wrapper)
            {
                 await _service.RunAsync(() => _inner.StructureCode = wrapper._inner);
                 return;
            }
            throw new System.ArgumentException("Value must be of type AsyncStructureCode");
        }

        public async Task<IReadOnlyList<StructureCodeInfo>> GetStructureCodeInfosAsync()
        {
            return await _service.RunAsync(() => _inner.StructureCodeInfos?.ToList());
        }


        public double Volume { get; }

        public Task RunAsync(Action<VMS.TPS.Common.Model.API.Structure> action) => _service.RunAsync(() => action(_inner));
        public Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.Structure, T> func) => _service.RunAsync(() => func(_inner));
    }
}
