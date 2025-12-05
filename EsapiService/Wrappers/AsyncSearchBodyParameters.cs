    using System.Threading.Tasks;
namespace EsapiService.Wrappers
{
    public class AsyncSearchBodyParameters : ISearchBodyParameters
    {
        internal readonly VMS.TPS.Common.Model.API.SearchBodyParameters _inner;

        // Store the inner ESAPI object reference
        // internal so other wrappers can access it
        // new to override any inherited _inner fields
        internal new readonly IEsapiService _service;

        public AsyncSearchBodyParameters(VMS.TPS.Common.Model.API.SearchBodyParameters inner, IEsapiService service) : base(inner, service)
        {
            _inner = inner;
            _service = service;

        }

        public void WriteXml(System.Xml.XmlWriter writer) => _inner.WriteXml(writer);
        public void LoadDefaults() => _inner.LoadDefaults();
        public bool FillAllCavities => _inner.FillAllCavities;
        public async Task SetFillAllCavitiesAsync(bool value) => _service.RunAsync(() => _inner.FillAllCavities = value);
        public bool KeepLargestParts => _inner.KeepLargestParts;
        public async Task SetKeepLargestPartsAsync(bool value) => _service.RunAsync(() => _inner.KeepLargestParts = value);
        public int LowerHUThreshold => _inner.LowerHUThreshold;
        public async Task SetLowerHUThresholdAsync(int value) => _service.RunAsync(() => _inner.LowerHUThreshold = value);
        public int MREdgeThresholdHigh => _inner.MREdgeThresholdHigh;
        public async Task SetMREdgeThresholdHighAsync(int value) => _service.RunAsync(() => _inner.MREdgeThresholdHigh = value);
        public int MREdgeThresholdLow => _inner.MREdgeThresholdLow;
        public async Task SetMREdgeThresholdLowAsync(int value) => _service.RunAsync(() => _inner.MREdgeThresholdLow = value);
        public int NumberOfLargestPartsToKeep => _inner.NumberOfLargestPartsToKeep;
        public async Task SetNumberOfLargestPartsToKeepAsync(int value) => _service.RunAsync(() => _inner.NumberOfLargestPartsToKeep = value);
        public bool PreCloseOpenings => _inner.PreCloseOpenings;
        public async Task SetPreCloseOpeningsAsync(bool value) => _service.RunAsync(() => _inner.PreCloseOpenings = value);
        public double PreCloseOpeningsRadius => _inner.PreCloseOpeningsRadius;
        public async Task SetPreCloseOpeningsRadiusAsync(double value) => _service.RunAsync(() => _inner.PreCloseOpeningsRadius = value);
        public bool PreDisconnect => _inner.PreDisconnect;
        public async Task SetPreDisconnectAsync(bool value) => _service.RunAsync(() => _inner.PreDisconnect = value);
        public double PreDisconnectRadius => _inner.PreDisconnectRadius;
        public async Task SetPreDisconnectRadiusAsync(double value) => _service.RunAsync(() => _inner.PreDisconnectRadius = value);
        public bool Smoothing => _inner.Smoothing;
        public async Task SetSmoothingAsync(bool value) => _service.RunAsync(() => _inner.Smoothing = value);
        public int SmoothingLevel => _inner.SmoothingLevel;
        public async Task SetSmoothingLevelAsync(int value) => _service.RunAsync(() => _inner.SmoothingLevel = value);
    }
}
