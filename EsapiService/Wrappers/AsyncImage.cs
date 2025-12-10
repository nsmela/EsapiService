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
    public class AsyncImage : AsyncApiDataObject, IImage
    {
        internal new readonly VMS.TPS.Common.Model.API.Image _inner;

        // Store the inner ESAPI object reference
        // internal so other wrappers can access it
        // new to override any inherited _inner fields
        internal new readonly IEsapiService _service;

        public AsyncImage(VMS.TPS.Common.Model.API.Image inner, IEsapiService service) : base(inner, service)
        {
            _inner = inner;
            _service = service;

            CalibrationProtocolDateTime = inner.CalibrationProtocolDateTime;
            CalibrationProtocolDescription = inner.CalibrationProtocolDescription;
            CalibrationProtocolId = inner.CalibrationProtocolId;
            CalibrationProtocolImageMatchWarning = inner.CalibrationProtocolImageMatchWarning;
            CalibrationProtocolLastModifiedDateTime = inner.CalibrationProtocolLastModifiedDateTime;
            ContrastBolusAgentIngredientName = inner.ContrastBolusAgentIngredientName;
            CreationDateTime = inner.CreationDateTime;
            DisplayUnit = inner.DisplayUnit;
            FOR = inner.FOR;
            HasUserOrigin = inner.HasUserOrigin;
            ImageType = inner.ImageType;
            ImagingDeviceId = inner.ImagingDeviceId;
            ImagingOrientationAsString = inner.ImagingOrientationAsString;
            IsProcessed = inner.IsProcessed;
            Level = inner.Level;
            UID = inner.UID;
            UserOriginComments = inner.UserOriginComments;
            Window = inner.Window;
            XRes = inner.XRes;
            XSize = inner.XSize;
            YRes = inner.YRes;
            YSize = inner.YSize;
            ZRes = inner.ZRes;
            ZSize = inner.ZSize;
        }


        public Task CalculateDectProtonStoppingPowersAsync(IImage rhoImage, IImage zImage, int planeIndex, double[,] preallocatedBuffer) => _service.PostAsync(context => _inner.CalculateDectProtonStoppingPowers(((AsyncImage)rhoImage)._inner, ((AsyncImage)zImage)._inner, planeIndex, preallocatedBuffer));

        public async Task<IStructureSet> CreateNewStructureSetAsync()
        {
            return await _service.PostAsync(context => 
                _inner.CreateNewStructureSet() is var result && result is null ? null : new AsyncStructureSet(result, _service));
        }


        public Task<bool> GetProtonStoppingPowerCurveAsync(SortedList<double, double> protonStoppingPowerCurve) => _service.PostAsync(context => _inner.GetProtonStoppingPowerCurve(protonStoppingPowerCurve));

        public Task GetVoxelsAsync(int planeIndex, int[,] preallocatedBuffer) => _service.PostAsync(context => _inner.GetVoxels(planeIndex, preallocatedBuffer));

        public Task<double> VoxelToDisplayValueAsync(int voxelValue) => _service.PostAsync(context => _inner.VoxelToDisplayValue(voxelValue));

        public DateTime? CalibrationProtocolDateTime { get; }

        public string CalibrationProtocolDescription { get; }

        public string CalibrationProtocolId { get; }

        public string CalibrationProtocolImageMatchWarning { get; }

        public DateTime? CalibrationProtocolLastModifiedDateTime { get; }

        public string ContrastBolusAgentIngredientName { get; }

        public DateTime? CreationDateTime { get; }

        public string DisplayUnit { get; }

        public string FOR { get; }

        public bool HasUserOrigin { get; }

        public string ImageType { get; }

        public string ImagingDeviceId { get; }

        public string ImagingOrientationAsString { get; }

        public bool IsProcessed { get; }

        public int Level { get; }

        public async Task<ISeries> GetSeriesAsync()
        {
            return await _service.PostAsync(context => 
                _inner.Series is null ? null : new AsyncSeries(_inner.Series, _service));
        }

        public string UID { get; }

        public string UserOriginComments { get; }

        public int Window { get; }

        public double XRes { get; }

        public int XSize { get; }

        public double YRes { get; }

        public int YSize { get; }

        public double ZRes { get; }

        public int ZSize { get; }

        public Task RunAsync(Action<VMS.TPS.Common.Model.API.Image> action) => _service.PostAsync((context) => action(_inner));
        public Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.Image, T> func) => _service.PostAsync<T>((context) => func(_inner));
    }
}
