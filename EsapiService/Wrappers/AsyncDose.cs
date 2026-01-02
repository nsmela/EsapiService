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
        }


        // Simple Method
        public Task<DoseProfile> GetDoseProfileAsync(VVector start, VVector stop, double[] preallocatedBuffer) => 
            _service.PostAsync(context => _inner.GetDoseProfile(start, stop, preallocatedBuffer));

        // Simple Method
        public Task<DoseValue> GetDoseToPointAsync(VVector at) => 
            _service.PostAsync(context => _inner.GetDoseToPoint(at));

        // Simple Void Method
        public Task GetVoxelsAsync(int planeIndex, int[,] preallocatedBuffer) 
        {
            _service.PostAsync(context => _inner.GetVoxels(planeIndex, preallocatedBuffer));
            return Task.CompletedTask;
        }

        // Simple Method
        public Task<DoseValue> VoxelToDoseValueAsync(int voxelValue) => 
            _service.PostAsync(context => _inner.VoxelToDoseValue(voxelValue));

        public DoseValue DoseMax3D =>
            _inner.DoseMax3D;


        public VVector DoseMax3DLocation =>
            _inner.DoseMax3DLocation;


        public async Task<IReadOnlyList<IIsodose>> GetIsodosesAsync()
        {
            return await _service.PostAsync(context => 
                _inner.Isodoses?.Select(x => new AsyncIsodose(x, _service)).ToList());
        }


        public VVector Origin =>
            _inner.Origin;


        public async Task<ISeries> GetSeriesAsync()
        {
            return await _service.PostAsync(context => {
                var innerResult = _inner.Series is null ? null : new AsyncSeries(_inner.Series, _service);
                return innerResult;
            });
        }

        public string SeriesUID =>
            _inner.SeriesUID;


        public string UID =>
            _inner.UID;


        public VVector XDirection =>
            _inner.XDirection;


        public double XRes =>
            _inner.XRes;


        public int XSize =>
            _inner.XSize;


        public VVector YDirection =>
            _inner.YDirection;


        public double YRes =>
            _inner.YRes;


        public int YSize =>
            _inner.YSize;


        public VVector ZDirection =>
            _inner.ZDirection;


        public double ZRes =>
            _inner.ZRes;


        public int ZSize =>
            _inner.ZSize;


        public Task RunAsync(Action<VMS.TPS.Common.Model.API.Dose> action) => _service.PostAsync((context) => action(_inner));
        public Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.Dose, T> func) => _service.PostAsync<T>((context) => func(_inner));

        // --- Validates --- //
        /// <summary>
        /// Verifies is the wrapped ESAPI object isn't null.
        /// </summary>
        public new bool IsValid() => !IsNotValid();

        /// <summary>
        /// Verifies is the wrapped ESAPI object is null.
        /// </summary>
        public new bool IsNotValid() => _inner is null;

        public static implicit operator VMS.TPS.Common.Model.API.Dose(AsyncDose wrapper) => wrapper._inner;

        // Internal Explicit Implementation to expose _inner safely for covariance
        VMS.TPS.Common.Model.API.Dose IEsapiWrapper<VMS.TPS.Common.Model.API.Dose>.Inner => _inner;

        // Explicit or Implicit implementation of Service
        // Since _service is private, we expose it via the interface
        IEsapiService IEsapiWrapper<VMS.TPS.Common.Model.API.Dose>.Service => _service;
    }
}
