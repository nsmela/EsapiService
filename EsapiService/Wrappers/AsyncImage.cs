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
    public class AsyncImage : AsyncApiDataObject, IImage, IEsapiWrapper<VMS.TPS.Common.Model.API.Image>
    {
        internal new readonly VMS.TPS.Common.Model.API.Image _inner;

        // Store the inner ESAPI object reference
        // internal so other wrappers can access it
        // new to override any inherited _inner fields
        internal new readonly IEsapiService _service;

        public AsyncImage(VMS.TPS.Common.Model.API.Image inner, IEsapiService service) : base(inner, service)
        {
            if (inner is null) throw new ArgumentNullException(nameof(inner));
            if (service is null) throw new ArgumentNullException(nameof(service));

            _inner = inner;
            _service = service;

            ContrastBolusAgentIngredientName = inner.ContrastBolusAgentIngredientName;
            CreationDateTime = inner.CreationDateTime;
            DisplayUnit = inner.DisplayUnit;
            FOR = inner.FOR;
            HasUserOrigin = inner.HasUserOrigin;
            ImageType = inner.ImageType;
            ImagingDeviceId = inner.ImagingDeviceId;
            ImagingOrientation = inner.ImagingOrientation;
            ImagingOrientationAsString = inner.ImagingOrientationAsString;
            IsProcessed = inner.IsProcessed;
            Level = inner.Level;
            Modality = inner.Modality;
            Origin = inner.Origin;
            UID = inner.UID;
            UserOrigin = inner.UserOrigin;
            UserOriginComments = inner.UserOriginComments;
            Window = inner.Window;
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


        // Simple Void Method
        public Task CalculateDectProtonStoppingPowersAsync(IImage rhoImage, IImage zImage, int planeIndex, double[,] preallocatedBuffer) =>
            _service.PostAsync(context => _inner.CalculateDectProtonStoppingPowers(((AsyncImage)rhoImage)._inner, ((AsyncImage)zImage)._inner, planeIndex, preallocatedBuffer));

        public async Task<IStructureSet> CreateNewStructureSetAsync()
        {
            return await _service.PostAsync(context => 
                _inner.CreateNewStructureSet() is var result && result is null ? null : new AsyncStructureSet(result, _service));
        }


        // Simple Method
        public Task<VVector> DicomToUserAsync(VVector dicom, IPlanSetup planSetup) => 
            _service.PostAsync(context => _inner.DicomToUser(dicom, ((AsyncPlanSetup)planSetup)._inner));

        // Simple Method
        public Task<ImageProfile> GetImageProfileAsync(VVector start, VVector stop, double[] preallocatedBuffer) => 
            _service.PostAsync(context => _inner.GetImageProfile(start, stop, preallocatedBuffer));

        // Simple Method
        public Task<bool> GetProtonStoppingPowerCurveAsync(SortedList<double, double> protonStoppingPowerCurve) => 
            _service.PostAsync(context => _inner.GetProtonStoppingPowerCurve(protonStoppingPowerCurve));

        // Simple Void Method
        public Task GetVoxelsAsync(int planeIndex, int[,] preallocatedBuffer) =>
            _service.PostAsync(context => _inner.GetVoxels(planeIndex, preallocatedBuffer));

        // Simple Method
        public Task<VVector> UserToDicomAsync(VVector user, IPlanSetup planSetup) => 
            _service.PostAsync(context => _inner.UserToDicom(user, ((AsyncPlanSetup)planSetup)._inner));

        // Simple Method
        public Task<double> VoxelToDisplayValueAsync(int voxelValue) => 
            _service.PostAsync(context => _inner.VoxelToDisplayValue(voxelValue));

        public string ContrastBolusAgentIngredientName { get; private set; }

        public DateTime? CreationDateTime { get; private set; }

        public string DisplayUnit { get; private set; }

        public string FOR { get; private set; }

        public bool HasUserOrigin { get; private set; }

        public string ImageType { get; private set; }

        public string ImagingDeviceId { get; private set; }

        public PatientOrientation ImagingOrientation { get; private set; }

        public string ImagingOrientationAsString { get; private set; }

        public bool IsProcessed { get; private set; }

        public int Level { get; private set; }

        public SeriesModality Modality { get; private set; }

        public VVector Origin { get; private set; }

        public async Task<ISeries> GetSeriesAsync()
        {
            return await _service.PostAsync(context => {
                var innerResult = _inner.Series is null ? null : new AsyncSeries(_inner.Series, _service);
                return innerResult;
            });
        }

        public string UID { get; private set; }

        public VVector UserOrigin { get; private set; }
        public async Task SetUserOriginAsync(VVector value)
        {
            UserOrigin = await _service.PostAsync(context => 
            {
                _inner.UserOrigin = value;
                return _inner.UserOrigin;
            });
        }

        public string UserOriginComments { get; private set; }

        public int Window { get; private set; }

        public VVector XDirection { get; private set; }

        public double XRes { get; private set; }

        public int XSize { get; private set; }

        public VVector YDirection { get; private set; }

        public double YRes { get; private set; }

        public int YSize { get; private set; }

        public VVector ZDirection { get; private set; }

        public double ZRes { get; private set; }

        public int ZSize { get; private set; }

        public Task RunAsync(Action<VMS.TPS.Common.Model.API.Image> action) => _service.PostAsync((context) => action(_inner));
        public Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.Image, T> func) => _service.PostAsync<T>((context) => func(_inner));

        // updates simple properties that might have changed
        public new void Refresh()
        {
            base.Refresh();

            ContrastBolusAgentIngredientName = _inner.ContrastBolusAgentIngredientName;
            CreationDateTime = _inner.CreationDateTime;
            DisplayUnit = _inner.DisplayUnit;
            FOR = _inner.FOR;
            HasUserOrigin = _inner.HasUserOrigin;
            ImageType = _inner.ImageType;
            ImagingDeviceId = _inner.ImagingDeviceId;
            ImagingOrientation = _inner.ImagingOrientation;
            ImagingOrientationAsString = _inner.ImagingOrientationAsString;
            IsProcessed = _inner.IsProcessed;
            Level = _inner.Level;
            Modality = _inner.Modality;
            Origin = _inner.Origin;
            UID = _inner.UID;
            UserOrigin = _inner.UserOrigin;
            UserOriginComments = _inner.UserOriginComments;
            Window = _inner.Window;
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

        public static implicit operator VMS.TPS.Common.Model.API.Image(AsyncImage wrapper) => wrapper._inner;

        // Internal Explicit Implementation to expose _inner safely for covariance
        VMS.TPS.Common.Model.API.Image IEsapiWrapper<VMS.TPS.Common.Model.API.Image>.Inner => _inner;

        // Explicit or Implicit implementation of Service
        // Since _service is private, we expose it via the interface
        IEsapiService IEsapiWrapper<VMS.TPS.Common.Model.API.Image>.Service => _service;

        /* --- Skipped Members (Not generated) ---
           - Id: Shadows base member in wrapped base class
           - ApprovalHistory: No matching factory found (Not Implemented)
        */
    }
}
