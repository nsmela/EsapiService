using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Media;
using VMS.TPS.Common.Model.API;
using VMS.TPS.Common.Model.Types;
using Esapi.Services;
using Esapi.Interfaces;

namespace Esapi.Interfaces
{
    public interface IIonControlPoint : IControlPoint
    {
        // --- Simple Properties --- //
        double NominalBeamEnergy { get; }
        int NumberOfPaintings { get; }
        double ScanningSpotSizeX { get; }
        double ScanningSpotSizeY { get; }
        string ScanSpotTuneId { get; }
        double SnoutPosition { get; }

        // --- Accessors --- //
        Task<IIonSpotCollection> GetFinalSpotListAsync();
        Task<IIonSpotCollection> GetRawSpotListAsync();

        // --- Collections --- //
        Task<IReadOnlyList<ILateralSpreadingDeviceSettings>> GetLateralSpreadingDeviceSettingsAsync();
        Task<IReadOnlyList<IRangeModulatorSettings>> GetRangeModulatorSettingsAsync();
        Task<IReadOnlyList<IRangeShifterSettings>> GetRangeShifterSettingsAsync();

        // --- RunAsync --- //
        /// <summary>
        /// Runs a function against the raw ESAPI VMS.TPS.Common.Model.API.IonControlPoint object safely on the ESAPI thread.
        /// </summary>
        Task RunAsync(Action<VMS.TPS.Common.Model.API.IonControlPoint> action);

        /// <summary>
        /// Runs a function against the raw ESAPI VMS.TPS.Common.Model.API.IonControlPoint object safely on the ESAPI thread.
        /// </summary>
        Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.IonControlPoint, T> func);
    }
}
