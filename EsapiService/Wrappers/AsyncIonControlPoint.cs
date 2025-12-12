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

            LateralSpreadingDeviceSettings = inner.LateralSpreadingDeviceSettings;
            NominalBeamEnergy = inner.NominalBeamEnergy;
            NumberOfPaintings = inner.NumberOfPaintings;
            RangeModulatorSettings = inner.RangeModulatorSettings;
            RangeShifterSettings = inner.RangeShifterSettings;
            ScanningSpotSizeX = inner.ScanningSpotSizeX;
            ScanningSpotSizeY = inner.ScanningSpotSizeY;
            ScanSpotTuneId = inner.ScanSpotTuneId;
            SnoutPosition = inner.SnoutPosition;
        }

        public async Task<IIonSpotCollection> GetFinalSpotListAsync()
        {
            return await _service.PostAsync(context => 
                _inner.FinalSpotList is null ? null : new AsyncIonSpotCollection(_inner.FinalSpotList, _service));
        }

        public IEnumerable<LateralSpreadingDeviceSettings> LateralSpreadingDeviceSettings { get; }

        public double NominalBeamEnergy { get; }

        public int NumberOfPaintings { get; }

        public IEnumerable<RangeModulatorSettings> RangeModulatorSettings { get; }

        public IEnumerable<RangeShifterSettings> RangeShifterSettings { get; }

        public async Task<IIonSpotCollection> GetRawSpotListAsync()
        {
            return await _service.PostAsync(context => 
                _inner.RawSpotList is null ? null : new AsyncIonSpotCollection(_inner.RawSpotList, _service));
        }

        public double ScanningSpotSizeX { get; }

        public double ScanningSpotSizeY { get; }

        public string ScanSpotTuneId { get; }

        public double SnoutPosition { get; }

        public Task RunAsync(Action<VMS.TPS.Common.Model.API.IonControlPoint> action) => _service.PostAsync((context) => action(_inner));
        public Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.IonControlPoint, T> func) => _service.PostAsync<T>((context) => func(_inner));

        public static implicit operator VMS.TPS.Common.Model.API.IonControlPoint(AsyncIonControlPoint wrapper) => wrapper;

        // Internal Explicit Implementation to expose _inner safely for covariance
        VMS.TPS.Common.Model.API.IonControlPoint IEsapiWrapper<VMS.TPS.Common.Model.API.IonControlPoint>.Inner => _inner;
    }
}
