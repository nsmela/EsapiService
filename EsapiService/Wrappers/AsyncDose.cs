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
            _inner = inner;
            _service = service;

            SeriesUID = inner.SeriesUID;
            UID = inner.UID;
            XRes = inner.XRes;
            XSize = inner.XSize;
            YRes = inner.YRes;
            YSize = inner.YSize;
            ZRes = inner.ZRes;
            ZSize = inner.ZSize;
        }

        // Simple Void Method
        public Task GetVoxelsAsync(int planeIndex, int[,] preallocatedBuffer) => _service.PostAsync(context => _inner.GetVoxels(planeIndex, preallocatedBuffer));

        public async Task<IReadOnlyList<IIsodose>> GetIsodosesAsync()
        {
            return await _service.PostAsync(context => 
                _inner.Isodoses?.Select(x => new AsyncIsodose(x, _service)).ToList());
        }


        public async Task<ISeries> GetSeriesAsync()
        {
            return await _service.PostAsync(context => 
                _inner.Series is null ? null : new AsyncSeries(_inner.Series, _service));
        }

        public string SeriesUID { get; }

        public string UID { get; }

        public double XRes { get; }

        public int XSize { get; }

        public double YRes { get; }

        public int YSize { get; }

        public double ZRes { get; }

        public int ZSize { get; }

        public Task RunAsync(Action<VMS.TPS.Common.Model.API.Dose> action) => _service.PostAsync((context) => action(_inner));
        public Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.Dose, T> func) => _service.PostAsync<T>((context) => func(_inner));

        public static implicit operator VMS.TPS.Common.Model.API.Dose(AsyncDose wrapper) => wrapper._inner;
        // Internal Explicit Implementation to expose _inner safely
        VMS.TPS.Common.Model.API.Dose IEsapiWrapper<VMS.TPS.Common.Model.API.Dose>.Inner => _inner;
    }
}
