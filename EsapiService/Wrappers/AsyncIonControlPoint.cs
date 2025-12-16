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
    public class AsyncIonControlPoint : AsyncControlPoint, IIonControlPoint, IEsapiWrapper<VMS.TPS.Common.Model.API.IonControlPoint>
    {
        internal new readonly VMS.TPS.Common.Model.API.IonControlPoint _inner;

        // Store the inner ESAPI object reference
        // internal so other wrappers can access it
        // new to override any inherited _inner fields
        internal new readonly IEsapiService _service;

public AsyncIonControlPoint(VMS.TPS.Common.Model.API.IonControlPoint inner, IEsapiService service) : base(inner, service)
        {
            if (inner == null) throw new ArgumentNullException(nameof(inner));
            if (service == null) throw new ArgumentNullException(nameof(service));

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
            var result = await _service.PostAsync(context => 
                _inner.FinalSpotList is null ? null : new AsyncIonSpotCollection(_inner.FinalSpotList, _service));
            return result;
        }

        public async Task<IReadOnlyList<ILateralSpreadingDeviceSettings>> GetLateralSpreadingDeviceSettingsAsync()
        {
            return await _service.PostAsync(context => 
                _inner.LateralSpreadingDeviceSettings?.Select(x => new AsyncLateralSpreadingDeviceSettings(x, _service)).ToList());
        }


        public double NominalBeamEnergy { get; }

        public int NumberOfPaintings { get; }

        public async Task<IReadOnlyList<IRangeModulatorSettings>> GetRangeModulatorSettingsAsync()
        {
            return await _service.PostAsync(context => 
                _inner.RangeModulatorSettings?.Select(x => new AsyncRangeModulatorSettings(x, _service)).ToList());
        }


        public async Task<IReadOnlyList<IRangeShifterSettings>> GetRangeShifterSettingsAsync()
        {
            return await _service.PostAsync(context => 
                _inner.RangeShifterSettings?.Select(x => new AsyncRangeShifterSettings(x, _service)).ToList());
        }


        public async Task<IIonSpotCollection> GetRawSpotListAsync()
        {
            var result = await _service.PostAsync(context => 
                _inner.RawSpotList is null ? null : new AsyncIonSpotCollection(_inner.RawSpotList, _service));
            return result;
        }

        public double ScanningSpotSizeX { get; }

        public double ScanningSpotSizeY { get; }

        public string ScanSpotTuneId { get; }

        public double SnoutPosition { get; }

        public Task RunAsync(Action<VMS.TPS.Common.Model.API.IonControlPoint> action) => _service.PostAsync((context) => action(_inner));
        public Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.IonControlPoint, T> func) => _service.PostAsync<T>((context) => func(_inner));

        public static implicit operator VMS.TPS.Common.Model.API.IonControlPoint(AsyncIonControlPoint wrapper) => wrapper._inner;

        // Internal Explicit Implementation to expose _inner safely for covariance
        VMS.TPS.Common.Model.API.IonControlPoint IEsapiWrapper<VMS.TPS.Common.Model.API.IonControlPoint>.Inner => _inner;

        // Explicit or Implicit implementation of Service
        // Since _service is private, we expose it via the interface
        IEsapiService IEsapiWrapper<VMS.TPS.Common.Model.API.IonControlPoint>.Service => _service;
    }
}
