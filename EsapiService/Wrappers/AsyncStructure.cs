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

        public void WriteXml(System.Xml.XmlWriter writer) => _inner.WriteXml(writer);
        public void AddContourOnImagePlane(VMS.TPS.Common.Model.Types.VVector[] contour, int z) => _inner.AddContourOnImagePlane(contour, z);
        public ISegmentVolume And(VMS.TPS.Common.Model.API.SegmentVolume other) => _inner.And(other) is var result && result is null ? null : new AsyncSegmentVolume(result, _service);
        public ISegmentVolume AsymmetricMargin(VMS.TPS.Common.Model.Types.AxisAlignedMargins margins) => _inner.AsymmetricMargin(margins) is var result && result is null ? null : new AsyncSegmentVolume(result, _service);
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
        public void ConvertDoseLevelToStructure(VMS.TPS.Common.Model.API.Dose dose, VMS.TPS.Common.Model.Types.DoseValue doseLevel) => _inner.ConvertDoseLevelToStructure(dose, doseLevel);
        public void ConvertToHighResolution() => _inner.ConvertToHighResolution();
        public async System.Threading.Tasks.Task<(bool Result, double huValue)> GetAssignedHUAsync()
        {
            double huValue_temp;
            var result = await _service.RunAsync(() => _inner.GetAssignedHU(out huValue_temp));
            return (result, huValue_temp);
        }
        public VMS.TPS.Common.Model.Types.VVector[][] GetContoursOnImagePlane(int z) => _inner.GetContoursOnImagePlane(z);
        public int GetNumberOfSeparateParts() => _inner.GetNumberOfSeparateParts();
        public VMS.TPS.Common.Model.Types.VVector[] GetReferenceLinePoints() => _inner.GetReferenceLinePoints();
        public VMS.TPS.Common.Model.Types.SegmentProfile GetSegmentProfile(VMS.TPS.Common.Model.Types.VVector start, VMS.TPS.Common.Model.Types.VVector stop, System.Collections.BitArray preallocatedBuffer) => _inner.GetSegmentProfile(start, stop, preallocatedBuffer);
        public bool IsPointInsideSegment(VMS.TPS.Common.Model.Types.VVector point) => _inner.IsPointInsideSegment(point);
        public ISegmentVolume Margin(double marginInMM) => _inner.Margin(marginInMM) is var result && result is null ? null : new AsyncSegmentVolume(result, _service);
        public ISegmentVolume Not() => _inner.Not() is var result && result is null ? null : new AsyncSegmentVolume(result, _service);
        public ISegmentVolume Or(VMS.TPS.Common.Model.API.SegmentVolume other) => _inner.Or(other) is var result && result is null ? null : new AsyncSegmentVolume(result, _service);
        public bool ResetAssignedHU() => _inner.ResetAssignedHU();
        public void SetAssignedHU(double huValue) => _inner.SetAssignedHU(huValue);
        public ISegmentVolume Sub(VMS.TPS.Common.Model.API.SegmentVolume other) => _inner.Sub(other) is var result && result is null ? null : new AsyncSegmentVolume(result, _service);
        public void SubtractContourOnImagePlane(VMS.TPS.Common.Model.Types.VVector[] contour, int z) => _inner.SubtractContourOnImagePlane(contour, z);
        public ISegmentVolume Xor(VMS.TPS.Common.Model.API.SegmentVolume other) => _inner.Xor(other) is var result && result is null ? null : new AsyncSegmentVolume(result, _service);
        public string Id => _inner.Id;
        public async Task SetIdAsync(string value) => _service.RunAsync(() => _inner.Id = value);
        public string Name => _inner.Name;
        public async Task SetNameAsync(string value) => _service.RunAsync(() => _inner.Name = value);
        public string Comment => _inner.Comment;
        public async Task SetCommentAsync(string value) => _service.RunAsync(() => _inner.Comment = value);
        public System.Collections.Generic.IReadOnlyList<VMS.TPS.Common.Model.Types.StructureApprovalHistoryEntry> ApprovalHistory => _inner.ApprovalHistory?.ToList();
        public VMS.TPS.Common.Model.Types.VVector CenterPoint { get; }
        public System.Windows.Media.Color Color => _inner.Color;
        public async Task SetColorAsync(System.Windows.Media.Color value) => _service.RunAsync(() => _inner.Color = value);
        public string DicomType { get; }
        public bool HasCalculatedPlans { get; }
        public bool HasSegment { get; }
        public bool IsApproved { get; }
        public bool IsEmpty { get; }
        public bool IsHighResolution { get; }
        public bool IsTarget { get; }
        public System.Windows.Media.Media3D.MeshGeometry3D MeshGeometry { get; }
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

        public System.Collections.Generic.IReadOnlyList<VMS.TPS.Common.Model.Types.StructureCodeInfo> StructureCodeInfos => _inner.StructureCodeInfos?.ToList();
        public double Volume { get; }

        public Task RunAsync(Action<VMS.TPS.Common.Model.API.Structure> action) => _service.RunAsync(() => action(_inner));
        public Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.Structure, T> func) => _service.RunAsync(() => func(_inner));
    }
}
    }
}
