using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;
using VMS.TPS.Common.Model.API;
using VMS.TPS.Common.Model.Types;

namespace EsapiService.Wrappers
{
    public class AsyncIonControlPoint : IIonControlPoint
    {
        internal readonly VMS.TPS.Common.Model.API.IonControlPoint _inner;

        // Store the inner ESAPI object reference
        // internal so other wrappers can access it
        // new to override any inherited _inner fields
        internal new readonly IEsapiService _service;

        public AsyncIonControlPoint(VMS.TPS.Common.Model.API.IonControlPoint inner, IEsapiService service) : base(inner, service)
        {
            _inner = inner;
            _service = service;

            NominalBeamEnergy = inner.NominalBeamEnergy;
            NumberOfPaintings = inner.NumberOfPaintings;
            ScanningSpotSizeX = inner.ScanningSpotSizeX;
            ScanningSpotSizeY = inner.ScanningSpotSizeY;
            ScanSpotTuneId = inner.ScanSpotTuneId;
            SnoutPosition = inner.SnoutPosition;
        }


        public async Task<IIonSpotCollection> GetFinalSpotListAsync()
        {
            return await _service.RunAsync(() => 
                _inner.FinalSpotList is null ? null : new AsyncIonSpotCollection(_inner.FinalSpotList, _service));
        }

        public async Task<IReadOnlyList<ILateralSpreadingDeviceSettings>> GetLateralSpreadingDeviceSettingsAsync()
        {
            return await _service.RunAsync(() => 
                _inner.LateralSpreadingDeviceSettings?.Select(x => new AsyncLateralSpreadingDeviceSettings(x, _service)).ToList());
        }


        public double NominalBeamEnergy { get; }

        public int NumberOfPaintings { get; }

        public async Task<IReadOnlyList<IRangeModulatorSettings>> GetRangeModulatorSettingsAsync()
        {
            return await _service.RunAsync(() => 
                _inner.RangeModulatorSettings?.Select(x => new AsyncRangeModulatorSettings(x, _service)).ToList());
        }


        public async Task<IReadOnlyList<IRangeShifterSettings>> GetRangeShifterSettingsAsync()
        {
            return await _service.RunAsync(() => 
                _inner.RangeShifterSettings?.Select(x => new AsyncRangeShifterSettings(x, _service)).ToList());
        }


        public async Task<IIonSpotCollection> GetRawSpotListAsync()
        {
            return await _service.RunAsync(() => 
                _inner.RawSpotList is null ? null : new AsyncIonSpotCollection(_inner.RawSpotList, _service));
        }

        public double ScanningSpotSizeX { get; }

        public double ScanningSpotSizeY { get; }

        public string ScanSpotTuneId { get; }

        public double SnoutPosition { get; }

        public Task RunAsync(Action<VMS.TPS.Common.Model.API.IonControlPoint> action) => _service.RunAsync(() => action(_inner));
        public Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.IonControlPoint, T> func) => _service.RunAsync(() => func(_inner));
    }
}
