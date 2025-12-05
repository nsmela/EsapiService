    using System.Threading.Tasks;
namespace EsapiService.Wrappers
{
    public class AsyncImage : IImage
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

            CalibrationProtocolDescription = inner.CalibrationProtocolDescription;
            CalibrationProtocolId = inner.CalibrationProtocolId;
            CalibrationProtocolImageMatchWarning = inner.CalibrationProtocolImageMatchWarning;
            CalibrationProtocolStatus = inner.CalibrationProtocolStatus;
            CalibrationProtocolUser = inner.CalibrationProtocolUser;
            ContrastBolusAgentIngredientName = inner.ContrastBolusAgentIngredientName;
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

        public void CalculateDectProtonStoppingPowers(IImage rhoImage, IImage zImage, int planeIndex, double[,] preallocatedBuffer) => _inner.CalculateDectProtonStoppingPowers(rhoImage, zImage, planeIndex, preallocatedBuffer);
        public IStructureSet CreateNewStructureSet() => _inner.CreateNewStructureSet() is var result && result is null ? null : new AsyncStructureSet(result, _service);
        public VVector DicomToUser(VVector dicom, IPlanSetup planSetup) => _inner.DicomToUser(dicom, planSetup);
        public ImageProfile GetImageProfile(VVector start, VVector stop, double[] preallocatedBuffer) => _inner.GetImageProfile(start, stop, preallocatedBuffer);
        public bool GetProtonStoppingPowerCurve(SortedList<double, double> protonStoppingPowerCurve) => _inner.GetProtonStoppingPowerCurve(protonStoppingPowerCurve);
        public void GetVoxels(int planeIndex, int[,] preallocatedBuffer) => _inner.GetVoxels(planeIndex, preallocatedBuffer);
        public VVector UserToDicom(VVector user, IPlanSetup planSetup) => _inner.UserToDicom(user, planSetup);
        public double VoxelToDisplayValue(int voxelValue) => _inner.VoxelToDisplayValue(voxelValue);
        public IReadOnlyList<ImageApprovalHistoryEntry> ApprovalHistory => _inner.ApprovalHistory?.ToList();
        public IReadOnlyList<DateTime> CalibrationProtocolDateTime => _inner.CalibrationProtocolDateTime?.ToList();
        public string CalibrationProtocolDescription { get; }
        public string CalibrationProtocolId { get; }
        public string CalibrationProtocolImageMatchWarning { get; }
        public IReadOnlyList<DateTime> CalibrationProtocolLastModifiedDateTime => _inner.CalibrationProtocolLastModifiedDateTime?.ToList();
        public VMS.TPS.Common.Model.CalibrationProtocolStatus CalibrationProtocolStatus { get; }
        public VMS.TPS.Common.Model.UserInfo CalibrationProtocolUser { get; }
        public string ContrastBolusAgentIngredientName { get; }
        public IReadOnlyList<DateTime> CreationDateTime => _inner.CreationDateTime?.ToList();
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
        public ISeries Series => _inner.Series is null ? null : new AsyncSeries(_inner.Series, _service);

        public string UID { get; }
        public VVector UserOrigin => _inner.UserOrigin;
        public async Task SetUserOriginAsync(VVector value) => _service.RunAsync(() => _inner.UserOrigin = value);
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
    }
}
