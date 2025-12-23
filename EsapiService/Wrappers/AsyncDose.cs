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
            if (inner is null) throw new ArgumentNullException(nameof(inner));
            if (service is null) throw new ArgumentNullException(nameof(service));

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

        public DoseValue DoseMax3D { get; private set; }

        public VVector DoseMax3DLocation { get; private set; }

        public async Task<IReadOnlyList<IIsodose>> GetIsodosesAsync()
        {
            return await _service.PostAsync(context => 
                _inner.Isodoses?.Select(x => new AsyncIsodose(x, _service)).ToList());
        }


        public VVector Origin { get; private set; }

        public async Task<ISeries> GetSeriesAsync()
        {
            return await _service.PostAsync(context => {
                var innerResult = _inner.Series is null ? null : new AsyncSeries(_inner.Series, _service);
                return innerResult;
            });
        }

        public string SeriesUID { get; private set; }

        public string UID { get; private set; }

        public VVector XDirection { get; private set; }

        public double XRes { get; private set; }

        public int XSize { get; private set; }

        public VVector YDirection { get; private set; }

        public double YRes { get; private set; }

        public int YSize { get; private set; }

        public VVector ZDirection { get; private set; }

        public double ZRes { get; private set; }

        public int ZSize { get; private set; }

        public Task RunAsync(Action<VMS.TPS.Common.Model.API.Dose> action) => _service.PostAsync((context) => action(_inner));
        public Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.Dose, T> func) => _service.PostAsync<T>((context) => func(_inner));

        // updates simple properties that might have changed
        public new void Refresh()
        {
            base.Refresh();

            DoseMax3D = _inner.DoseMax3D;
            DoseMax3DLocation = _inner.DoseMax3DLocation;
            Origin = _inner.Origin;
            SeriesUID = _inner.SeriesUID;
            UID = _inner.UID;
            XDirection = _inner.XDirection;
            XRes = _inner.XRes;
            XSize = _inner.XSize;
            YDirection = _inner.YDirection;
            YRes = _inner.YRes;
            YSize = _inner.YSize;
            ZDirection = _inner.ZDirection;
            ZRes = _inner.ZRes;
            ZSize = _inner.ZSize;
        }

        public static implicit operator VMS.TPS.Common.Model.API.Dose(AsyncDose wrapper) => wrapper._inner;

        // Internal Explicit Implementation to expose _inner safely for covariance
        VMS.TPS.Common.Model.API.Dose IEsapiWrapper<VMS.TPS.Common.Model.API.Dose>.Inner => _inner;

        // Explicit or Implicit implementation of Service
        // Since _service is private, we expose it via the interface
        IEsapiService IEsapiWrapper<VMS.TPS.Common.Model.API.Dose>.Service => _service;
    }
}
