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
            _inner = inner;
            _service = service;

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

        public async Task<ISegmentVolume> AndAsync(ISegmentVolume other)
        {
            return await _service.PostAsync(context => 
                _inner.And(((AsyncSegmentVolume)other)._inner) is var result && result is null ? null : new AsyncSegmentVolume(result, _service));
        }


        // Simple Method
        public Task<bool> CanConvertToHighResolutionAsync() => _service.PostAsync(context => _inner.CanConvertToHighResolution());

        public async Task<(bool result, string errorMessage)> CanEditSegmentVolumeAsync()
        {
            var postResult = await _service.PostAsync(context => {
                string errorMessage_temp = default(string);
                var result = _inner.CanEditSegmentVolume(out errorMessage_temp);
                return (result, errorMessage_temp);
            });
            return (postResult);
        }

        public async Task<(bool result, string errorMessage)> CanSetAssignedHUAsync()
        {
            var postResult = await _service.PostAsync(context => {
                string errorMessage_temp = default(string);
                var result = _inner.CanSetAssignedHU(out errorMessage_temp);
                return (result, errorMessage_temp);
            });
            return (postResult);
        }

        // Simple Void Method
        public Task ClearAllContoursOnImagePlaneAsync(int z) => _service.PostAsync(context => _inner.ClearAllContoursOnImagePlane(z));

        // Simple Void Method
        public Task ConvertToHighResolutionAsync() => _service.PostAsync(context => _inner.ConvertToHighResolution());

        public async Task<(bool result, double huValue)> GetAssignedHUAsync()
        {
            var postResult = await _service.PostAsync(context => {
                double huValue_temp = default(double);
                var result = _inner.GetAssignedHU(out huValue_temp);
                return (result, huValue_temp);
            });
            return (postResult);
        }

        // Simple Method
        public Task<int> GetNumberOfSeparatePartsAsync() => _service.PostAsync(context => _inner.GetNumberOfSeparateParts());

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
        public Task<bool> ResetAssignedHUAsync() => _service.PostAsync(context => _inner.ResetAssignedHU());

        // Simple Void Method
        public Task SetAssignedHUAsync(double huValue) => _service.PostAsync(context => _inner.SetAssignedHU(huValue));

        public async Task<ISegmentVolume> SubAsync(ISegmentVolume other)
        {
            return await _service.PostAsync(context => 
                _inner.Sub(((AsyncSegmentVolume)other)._inner) is var result && result is null ? null : new AsyncSegmentVolume(result, _service));
        }


        public async Task<ISegmentVolume> XorAsync(ISegmentVolume other)
        {
            return await _service.PostAsync(context => 
                _inner.Xor(((AsyncSegmentVolume)other)._inner) is var result && result is null ? null : new AsyncSegmentVolume(result, _service));
        }


        public System.Windows.Media.Color Color { get; private set; }
        public async Task SetColorAsync(System.Windows.Media.Color value)
        {
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

        public System.Windows.Media.Media3D.MeshGeometry3D MeshGeometry { get; }

        public int ROINumber { get; }

        public async Task<ISegmentVolume> GetSegmentVolumeAsync()
        {
            return await _service.PostAsync(context => 
                _inner.SegmentVolume is null ? null : new AsyncSegmentVolume(_inner.SegmentVolume, _service));
        }

        public async Task SetSegmentVolumeAsync(ISegmentVolume value)
        {
            // Handle null assignment
            if (value is null)
            {
                await _service.PostAsync(context => _inner.SegmentVolume = null);
                return;
            }
            // Unwrap the interface to get the Varian object
            if (value is AsyncSegmentVolume wrapper)
            {
                 _service.PostAsync(context => _inner.SegmentVolume = wrapper._inner);
                 return;
            }
            throw new System.ArgumentException("Value must be of type AsyncSegmentVolume");
        }

        public async Task<IStructureCode> GetStructureCodeAsync()
        {
            return await _service.PostAsync(context => 
                _inner.StructureCode is null ? null : new AsyncStructureCode(_inner.StructureCode, _service));
        }

        public async Task SetStructureCodeAsync(IStructureCode value)
        {
            // Handle null assignment
            if (value is null)
            {
                await _service.PostAsync(context => _inner.StructureCode = null);
                return;
            }
            // Unwrap the interface to get the Varian object
            if (value is AsyncStructureCode wrapper)
            {
                 _service.PostAsync(context => _inner.StructureCode = wrapper._inner);
                 return;
            }
            throw new System.ArgumentException("Value must be of type AsyncStructureCode");
        }

        public double Volume { get; }

        public Task RunAsync(Action<VMS.TPS.Common.Model.API.Structure> action) => _service.PostAsync((context) => action(_inner));
        public Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.Structure, T> func) => _service.PostAsync<T>((context) => func(_inner));

        public static implicit operator VMS.TPS.Common.Model.API.Structure(AsyncStructure wrapper) => wrapper;

        // Internal Explicit Implementation to expose _inner safely for covariance
        VMS.TPS.Common.Model.API.Structure IEsapiWrapper<VMS.TPS.Common.Model.API.Structure>.Inner => _inner;
    }
}
