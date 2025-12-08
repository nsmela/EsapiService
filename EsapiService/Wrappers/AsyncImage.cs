using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;
using VMS.TPS.Common.Model.API;
using VMS.TPS.Common.Model.Types;
using Esapi.Interfaces;
using Esapi.Services;

namespace Esapi.Wrappers
{
    public class AsyncImage : AsyncApiDataObject, IImage
    {
        internal readonly VMS.TPS.Common.Model.API.Image _inner;

        // Store the inner ESAPI object reference
        // internal so other wrappers can access it
        // new to override any inherited _inner fields
        internal new readonly IEsapiService _service;

        public AsyncImage(VMS.TPS.Common.Model.API.Image inner, IEsapiService service) : base(inner, service)
        {
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


        public Task CalculateDectProtonStoppingPowersAsync(IImage rhoImage, IImage zImage, int planeIndex, double[,] preallocatedBuffer) => _service.RunAsync(() => _inner.CalculateDectProtonStoppingPowers(rhoImage, zImage, planeIndex, preallocatedBuffer));

        public async Task<IStructureSet> CreateNewStructureSetAsync()
        {
            return await _service.RunAsync(() => 
                _inner.CreateNewStructureSet() is var result && result is null ? null : new AsyncStructureSet(result, _service));
        }


        public Task<VVector> DicomToUserAsync(VVector dicom, IPlanSetup planSetup) => _service.RunAsync(() => _inner.DicomToUser(dicom, planSetup));

        public Task<ImageProfile> GetImageProfileAsync(VVector start, VVector stop, double[] preallocatedBuffer) => _service.RunAsync(() => _inner.GetImageProfile(start, stop, preallocatedBuffer));

        public Task<bool> GetProtonStoppingPowerCurveAsync(SortedList<double, double> protonStoppingPowerCurve) => _service.RunAsync(() => _inner.GetProtonStoppingPowerCurve(protonStoppingPowerCurve));

        public Task GetVoxelsAsync(int planeIndex, int[,] preallocatedBuffer) => _service.RunAsync(() => _inner.GetVoxels(planeIndex, preallocatedBuffer));

        public Task<VVector> UserToDicomAsync(VVector user, IPlanSetup planSetup) => _service.RunAsync(() => _inner.UserToDicom(user, planSetup));

        public Task<double> VoxelToDisplayValueAsync(int voxelValue) => _service.RunAsync(() => _inner.VoxelToDisplayValue(voxelValue));

        public async Task<IReadOnlyList<ImageApprovalHistoryEntry>> GetApprovalHistoryAsync()
        {
            return await _service.RunAsync(() => _inner.ApprovalHistory?.ToList());
        }


        public string ContrastBolusAgentIngredientName { get; }

        public DateTime? CreationDateTime { get; }

        public string DisplayUnit { get; }

        public string FOR { get; }

        public bool HasUserOrigin { get; }

        public string ImageType { get; }

        public string ImagingDeviceId { get; }

        public PatientOrientation ImagingOrientation { get; }

        public string ImagingOrientationAsString { get; }

        public bool IsProcessed { get; }

        public int Level { get; }

        public SeriesModality Modality { get; }

        public VVector Origin { get; }

        public async Task<ISeries> GetSeriesAsync()
        {
            return await _service.RunAsync(() => 
                _inner.Series is null ? null : new AsyncSeries(_inner.Series, _service));
        }

        public string UID { get; }

        public VVector UserOrigin { get; private set; }
        public async Task SetUserOriginAsync(VVector value)
        {
            UserOrigin = await _service.RunAsync(() =>
            {
                _inner.UserOrigin = value;
                return _inner.UserOrigin;
            });
        }

        public string UserOriginComments { get; }

        public int Window { get; }

        public VVector XDirection { get; }

        public double XRes { get; }

        public int XSize { get; }

        public VVector YDirection { get; }

        public double YRes { get; }

        public int YSize { get; }

        public VVector ZDirection { get; }

        public double ZRes { get; }

        public int ZSize { get; }

        public Task RunAsync(Action<VMS.TPS.Common.Model.API.Image> action) => _service.RunAsync(() => action(_inner));
        public Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.Image, T> func) => _service.RunAsync(() => func(_inner));
    }
}
