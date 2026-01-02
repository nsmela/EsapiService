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
        }


        // Simple Void Method
        public Task CalculateDectProtonStoppingPowersAsync(IImage rhoImage, IImage zImage, int planeIndex, double[,] preallocatedBuffer) 
        {
            _service.PostAsync(context => _inner.CalculateDectProtonStoppingPowers(((AsyncImage)rhoImage)._inner, ((AsyncImage)zImage)._inner, planeIndex, preallocatedBuffer));
            return Task.CompletedTask;
        }

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
        public Task GetVoxelsAsync(int planeIndex, int[,] preallocatedBuffer) 
        {
            _service.PostAsync(context => _inner.GetVoxels(planeIndex, preallocatedBuffer));
            return Task.CompletedTask;
        }

        // Simple Method
        public Task<VVector> UserToDicomAsync(VVector user, IPlanSetup planSetup) => 
            _service.PostAsync(context => _inner.UserToDicom(user, ((AsyncPlanSetup)planSetup)._inner));

        // Simple Method
        public Task<double> VoxelToDisplayValueAsync(int voxelValue) => 
            _service.PostAsync(context => _inner.VoxelToDisplayValue(voxelValue));

        public new string Id
        {
            get => _inner.Id;
            set => _inner.Id = value;
        }


        public string ContrastBolusAgentIngredientName =>
            _inner.ContrastBolusAgentIngredientName;


        public DateTime? CreationDateTime =>
            _inner.CreationDateTime;


        public string DisplayUnit =>
            _inner.DisplayUnit;


        public string FOR =>
            _inner.FOR;


        public bool HasUserOrigin =>
            _inner.HasUserOrigin;


        public string ImageType =>
            _inner.ImageType;


        public string ImagingDeviceId =>
            _inner.ImagingDeviceId;


        public PatientOrientation ImagingOrientation =>
            _inner.ImagingOrientation;


        public string ImagingOrientationAsString =>
            _inner.ImagingOrientationAsString;


        public bool IsProcessed =>
            _inner.IsProcessed;


        public int Level =>
            _inner.Level;


        public SeriesModality Modality =>
            _inner.Modality;


        public VVector Origin =>
            _inner.Origin;


        public async Task<ISeries> GetSeriesAsync()
        {
            return await _service.PostAsync(context => {
                var innerResult = _inner.Series is null ? null : new AsyncSeries(_inner.Series, _service);
                return innerResult;
            });
        }

        public string UID =>
            _inner.UID;


        public VVector UserOrigin
        {
            get => _inner.UserOrigin;
            set => _inner.UserOrigin = value;
        }


        public string UserOriginComments =>
            _inner.UserOriginComments;


        public int Window =>
            _inner.Window;


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


        public Task RunAsync(Action<VMS.TPS.Common.Model.API.Image> action) => _service.PostAsync((context) => action(_inner));
        public Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.Image, T> func) => _service.PostAsync<T>((context) => func(_inner));

        // --- Validates --- //
        /// <summary>
        /// Verifies is the wrapped ESAPI object isn't null.
        /// </summary>
        public new bool IsValid() => !IsNotValid();

        /// <summary>
        /// Verifies is the wrapped ESAPI object is null.
        /// </summary>
        public new bool IsNotValid() => _inner is null;

        public static implicit operator VMS.TPS.Common.Model.API.Image(AsyncImage wrapper) => wrapper._inner;

        // Internal Explicit Implementation to expose _inner safely for covariance
        VMS.TPS.Common.Model.API.Image IEsapiWrapper<VMS.TPS.Common.Model.API.Image>.Inner => _inner;

        // Explicit or Implicit implementation of Service
        // Since _service is private, we expose it via the interface
        IEsapiService IEsapiWrapper<VMS.TPS.Common.Model.API.Image>.Service => _service;

        /* --- Skipped Members (Not generated) ---
           - ApprovalHistory: No matching factory found (Not Implemented)
        */
    }
}
