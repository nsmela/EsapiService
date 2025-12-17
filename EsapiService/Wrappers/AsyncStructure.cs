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
    public class AsyncStructure : AsyncApiDataObject, IStructure, IEsapiWrapper<VMS.TPS.Common.Model.API.Structure>
    {
        internal new readonly VMS.TPS.Common.Model.API.Structure _inner;

        // Store the inner ESAPI object reference
        // internal so other wrappers can access it
        // new to override any inherited _inner fields
        internal new readonly IEsapiService _service;

public AsyncStructure(VMS.TPS.Common.Model.API.Structure inner, IEsapiService service) : base(inner, service)
        {
            if (inner is null) throw new ArgumentNullException(nameof(inner));
            if (service is null) throw new ArgumentNullException(nameof(service));

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
            ROINumber = inner.ROINumber;
            Volume = inner.Volume;
        }

        // Simple Void Method
        public Task AddContourOnImagePlaneAsync(VVector[] contour, int z) =>
            _service.PostAsync(context => _inner.AddContourOnImagePlane(contour, z));

        public async Task<ISegmentVolume> AndAsync(ISegmentVolume other)
        {
            return await _service.PostAsync(context => 
                _inner.And(((AsyncSegmentVolume)other)._inner) is var result && result is null ? null : new AsyncSegmentVolume(result, _service));
        }


        public async Task<ISegmentVolume> AsymmetricMarginAsync(AxisAlignedMargins margins)
        {
            return await _service.PostAsync(context => 
                _inner.AsymmetricMargin(margins) is var result && result is null ? null : new AsyncSegmentVolume(result, _service));
        }


        // Simple Method
        public Task<bool> CanConvertToHighResolutionAsync() => 
            _service.PostAsync(context => _inner.CanConvertToHighResolution());

        public async Task<(bool result, string errorMessage)> CanEditSegmentVolumeAsync()
        {
            var postResult = await _service.PostAsync(context => {
                string errorMessage_temp = default(string);
                var result = _inner.CanEditSegmentVolume(out errorMessage_temp);
                return (result, errorMessage_temp);
            });
            return (postResult.Item1,
                    postResult.Item2);
        }


        public async Task<(bool result, string errorMessage)> CanSetAssignedHUAsync()
        {
            var postResult = await _service.PostAsync(context => {
                string errorMessage_temp = default(string);
                var result = _inner.CanSetAssignedHU(out errorMessage_temp);
                return (result, errorMessage_temp);
            });
            return (postResult.Item1,
                    postResult.Item2);
        }


        // Simple Void Method
        public Task ClearAllContoursOnImagePlaneAsync(int z) =>
            _service.PostAsync(context => _inner.ClearAllContoursOnImagePlane(z));

        // Simple Void Method
        public Task ConvertDoseLevelToStructureAsync(IDose dose, DoseValue doseLevel) =>
            _service.PostAsync(context => _inner.ConvertDoseLevelToStructure(((AsyncDose)dose)._inner, doseLevel));

        // Simple Void Method
        public Task ConvertToHighResolutionAsync() =>
            _service.PostAsync(context => _inner.ConvertToHighResolution());

        public async Task<(bool result, double huValue)> GetAssignedHUAsync()
        {
            var postResult = await _service.PostAsync(context => {
                double huValue_temp = default(double);
                var result = _inner.GetAssignedHU(out huValue_temp);
                return (result, huValue_temp);
            });
            return (postResult.Item1,
                    postResult.Item2);
        }


        // Simple Method
        public Task<VVector[][]> GetContoursOnImagePlaneAsync(int z) => 
            _service.PostAsync(context => _inner.GetContoursOnImagePlane(z));

        // Simple Method
        public Task<int> GetNumberOfSeparatePartsAsync() => 
            _service.PostAsync(context => _inner.GetNumberOfSeparateParts());

        // Simple Method
        public Task<VVector[]> GetReferenceLinePointsAsync() => 
            _service.PostAsync(context => _inner.GetReferenceLinePoints());

        // Simple Method
        public Task<SegmentProfile> GetSegmentProfileAsync(VVector start, VVector stop, System.Collections.BitArray preallocatedBuffer) => 
            _service.PostAsync(context => _inner.GetSegmentProfile(start, stop, preallocatedBuffer));

        // Simple Method
        public Task<bool> IsPointInsideSegmentAsync(VVector point) => 
            _service.PostAsync(context => _inner.IsPointInsideSegment(point));

        public async Task<ISegmentVolume> MarginAsync(double marginInMM)
        {
            return await _service.PostAsync(context => 
                _inner.Margin(marginInMM) is var result && result is null ? null : new AsyncSegmentVolume(result, _service));
        }


        public async Task<ISegmentVolume> NotAsync()
        {
            return await _service.PostAsync(context => 
                _inner.Not() is var result && result is null ? null : new AsyncSegmentVolume(result, _service));
        }


        public async Task<ISegmentVolume> OrAsync(ISegmentVolume other)
        {
            return await _service.PostAsync(context => 
                _inner.Or(((AsyncSegmentVolume)other)._inner) is var result && result is null ? null : new AsyncSegmentVolume(result, _service));
        }


        // Simple Method
        public Task<bool> ResetAssignedHUAsync() => 
            _service.PostAsync(context => _inner.ResetAssignedHU());

        // Simple Void Method
        public Task SetAssignedHUAsync(double huValue) =>
            _service.PostAsync(context => _inner.SetAssignedHU(huValue));

        public async Task<ISegmentVolume> SubAsync(ISegmentVolume other)
        {
            return await _service.PostAsync(context => 
                _inner.Sub(((AsyncSegmentVolume)other)._inner) is var result && result is null ? null : new AsyncSegmentVolume(result, _service));
        }


        // Simple Void Method
        public Task SubtractContourOnImagePlaneAsync(VVector[] contour, int z) =>
            _service.PostAsync(context => _inner.SubtractContourOnImagePlane(contour, z));

        public async Task<ISegmentVolume> XorAsync(ISegmentVolume other)
        {
            return await _service.PostAsync(context => 
                _inner.Xor(((AsyncSegmentVolume)other)._inner) is var result && result is null ? null : new AsyncSegmentVolume(result, _service));
        }


        public VVector CenterPoint { get; }

        public System.Windows.Media.Color Color { get; private set; }
        public async Task SetColorAsync(System.Windows.Media.Color value)
        {
            await _service.BeginModificationsAsync();
            Color = await _service.PostAsync(context => 
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

        public async Task<System.Windows.Media.Media3D.MeshGeometry3D> GetMeshGeometryAsync()
        {
            return await _service.PostAsync(context => {
                var innerResult = _inner.MeshGeometry;
                if (innerResult != null && innerResult.CanFreeze) { innerResult.Freeze(); }
                return innerResult;
            });
        }

        public int ROINumber { get; }

        public async Task<ISegmentVolume> GetSegmentVolumeAsync()
        {
            return await _service.PostAsync(context => {
                var innerResult = _inner.SegmentVolume is null ? null : new AsyncSegmentVolume(_inner.SegmentVolume, _service);
                return innerResult;
            });
        }

        public async Task SetSegmentVolumeAsync(ISegmentVolume value)
        {
            if (value is null)
            {
                await _service.PostAsync(context => _inner.SegmentVolume = null);
                return;
            }
            if (value is IEsapiWrapper<SegmentVolume> wrapper)
            {
                 await _service.PostAsync(context => _inner.SegmentVolume = wrapper.Inner);
                 return;
            }
            throw new System.ArgumentException("Value must be of type AsyncSegmentVolume");
        }

        public async Task<IStructureCode> GetStructureCodeAsync()
        {
            return await _service.PostAsync(context => {
                var innerResult = _inner.StructureCode is null ? null : new AsyncStructureCode(_inner.StructureCode, _service);
                return innerResult;
            });
        }

        public async Task SetStructureCodeAsync(IStructureCode value)
        {
            if (value is null)
            {
                await _service.PostAsync(context => _inner.StructureCode = null);
                return;
            }
            if (value is IEsapiWrapper<StructureCode> wrapper)
            {
                 await _service.PostAsync(context => _inner.StructureCode = wrapper.Inner);
                 return;
            }
            throw new System.ArgumentException("Value must be of type AsyncStructureCode");
        }

        public double Volume { get; }

        public Task RunAsync(Action<VMS.TPS.Common.Model.API.Structure> action) => _service.PostAsync((context) => action(_inner));
        public Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.Structure, T> func) => _service.PostAsync<T>((context) => func(_inner));

        public static implicit operator VMS.TPS.Common.Model.API.Structure(AsyncStructure wrapper) => wrapper._inner;

        // Internal Explicit Implementation to expose _inner safely for covariance
        VMS.TPS.Common.Model.API.Structure IEsapiWrapper<VMS.TPS.Common.Model.API.Structure>.Inner => _inner;

        // Explicit or Implicit implementation of Service
        // Since _service is private, we expose it via the interface
        IEsapiService IEsapiWrapper<VMS.TPS.Common.Model.API.Structure>.Service => _service;

        /* --- Skipped Members (Not generated) ---
           - Id: Shadows member in wrapped base class
           - Name: Shadows member in wrapped base class
           - Comment: Shadows member in wrapped base class
           - ApprovalHistory: No matching factory found (Not Implemented)
           - StructureCodeInfos: No matching factory found (Not Implemented)
        */
    }
}
