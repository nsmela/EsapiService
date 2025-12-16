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
    public class AsyncDose : AsyncApiDataObject, IDose, IEsapiWrapper<VMS.TPS.Common.Model.API.Dose>
    {
        internal new readonly VMS.TPS.Common.Model.API.Dose _inner;

        // Store the inner ESAPI object reference
        // internal so other wrappers can access it
        // new to override any inherited _inner fields
        internal new readonly IEsapiService _service;

public AsyncDose(VMS.TPS.Common.Model.API.Dose inner, IEsapiService service) : base(inner, service)
        {
            if (inner == null) throw new ArgumentNullException(nameof(inner));
            if (service == null) throw new ArgumentNullException(nameof(service));

            _inner = inner;
            _service = service;

            DoseMax3D = inner.DoseMax3D;
            DoseMax3DLocation = inner.DoseMax3DLocation;
            Origin = inner.Origin;
            SeriesUID = inner.SeriesUID;
            UID = inner.UID;
            XDirection = inner.XDirection;
            XRes = inner.XRes;
            XSize = inner.XSize;
            YDirection = inner.YDirection;
            YRes = inner.YRes;
            YSize = inner.YSize;
            ZDirection = inner.ZDirection;
            ZRes = inner.ZRes;
            ZSize = inner.ZSize;
        }

        // Simple Method
        public Task<DoseProfile> GetDoseProfileAsync(VVector start, VVector stop, double[] preallocatedBuffer) => 
            _service.PostAsync(context => _inner.GetDoseProfile(start, stop, preallocatedBuffer));

        // Simple Method
        public Task<DoseValue> GetDoseToPointAsync(VVector at) => 
            _service.PostAsync(context => _inner.GetDoseToPoint(at));

        // Simple Void Method
        public Task GetVoxelsAsync(int planeIndex, int[,] preallocatedBuffer) =>
            _service.PostAsync(context => _inner.GetVoxels(planeIndex, preallocatedBuffer));

        // Simple Method
        public Task<DoseValue> VoxelToDoseValueAsync(int voxelValue) => 
            _service.PostAsync(context => _inner.VoxelToDoseValue(voxelValue));

        public DoseValue DoseMax3D { get; }

        public VVector DoseMax3DLocation { get; }

        public async Task<IReadOnlyList<IIsodose>> GetIsodosesAsync()
        {
            return await _service.PostAsync(context => 
                _inner.Isodoses?.Select(x => new AsyncIsodose(x, _service)).ToList());
        }


        public VVector Origin { get; }

        public async Task<ISeries> GetSeriesAsync()
        {
            return await _service.PostAsync(context => {
                var innerResult = _inner.Series is null ? null : new AsyncSeries(_inner.Series, _service);
                return innerResult;
            });
        }

        public string SeriesUID { get; }

        public string UID { get; }

        public VVector XDirection { get; }

        public double XRes { get; }

        public int XSize { get; }

        public VVector YDirection { get; }

        public double YRes { get; }

        public int YSize { get; }

        public VVector ZDirection { get; }

        public double ZRes { get; }

        public int ZSize { get; }

        public Task RunAsync(Action<VMS.TPS.Common.Model.API.Dose> action) => _service.PostAsync((context) => action(_inner));
        public Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.Dose, T> func) => _service.PostAsync<T>((context) => func(_inner));

        public static implicit operator VMS.TPS.Common.Model.API.Dose(AsyncDose wrapper) => wrapper._inner;

        // Internal Explicit Implementation to expose _inner safely for covariance
        VMS.TPS.Common.Model.API.Dose IEsapiWrapper<VMS.TPS.Common.Model.API.Dose>.Inner => _inner;

        // Explicit or Implicit implementation of Service
        // Since _service is private, we expose it via the interface
        IEsapiService IEsapiWrapper<VMS.TPS.Common.Model.API.Dose>.Service => _service;
    }
}
