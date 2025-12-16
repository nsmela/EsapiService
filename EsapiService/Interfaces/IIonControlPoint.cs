using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Media;
using VMS.TPS.Common.Model.API;
using VMS.TPS.Common.Model.Types;
using Esapi.Services;

namespace Esapi.Interfaces
{
    public interface IIonControlPoint : IControlPoint
    {
        // --- Simple Properties --- //
        double NominalBeamEnergy { get; } // simple property
        int NumberOfPaintings { get; } // simple property
        double ScanningSpotSizeX { get; } // simple property
        double ScanningSpotSizeY { get; } // simple property
        string ScanSpotTuneId { get; } // simple property
        double SnoutPosition { get; } // simple property

        // --- Accessors --- //
        Task<IIonSpotCollection> GetFinalSpotListAsync(); // read complex property
        Task<IIonSpotCollection> GetRawSpotListAsync(); // read complex property

        // --- Collections --- //
        Task<IReadOnlyList<ILateralSpreadingDeviceSettings>> GetLateralSpreadingDeviceSettingsAsync(); // collection proeprty context
        Task<IReadOnlyList<IRangeModulatorSettings>> GetRangeModulatorSettingsAsync(); // collection proeprty context
        Task<IReadOnlyList<IRangeShifterSettings>> GetRangeShifterSettingsAsync(); // collection proeprty context

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
