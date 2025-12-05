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
        Task WriteXmlAsync(System.Xml.XmlWriter writer);
        Task<IIonSpotCollection> GetFinalSpotListAsync();
        System.Collections.Generic.IReadOnlyList<ILateralSpreadingDeviceSettings> LateralSpreadingDeviceSettings { get; }
        double NominalBeamEnergy { get; }
        int NumberOfPaintings { get; }
        System.Collections.Generic.IReadOnlyList<IRangeModulatorSettings> RangeModulatorSettings { get; }
        System.Collections.Generic.IReadOnlyList<IRangeShifterSettings> RangeShifterSettings { get; }
        Task<IIonSpotCollection> GetRawSpotListAsync();
        double ScanningSpotSizeX { get; }
        double ScanningSpotSizeY { get; }
        string ScanSpotTuneId { get; }
        double SnoutPosition { get; }

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
