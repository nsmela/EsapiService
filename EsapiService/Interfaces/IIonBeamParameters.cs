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
    public interface IIonBeamParameters : IBeamParameters
    {
        // --- Simple Properties --- //
        string PreSelectedRangeShifter1Id { get; }
        Task SetPreSelectedRangeShifter1IdAsync(string value);
        string PreSelectedRangeShifter1Setting { get; }
        Task SetPreSelectedRangeShifter1SettingAsync(string value);
        string PreSelectedRangeShifter2Id { get; }
        Task SetPreSelectedRangeShifter2IdAsync(string value);
        string PreSelectedRangeShifter2Setting { get; }
        Task SetPreSelectedRangeShifter2SettingAsync(string value);
        string SnoutId { get; }
        double SnoutPosition { get; }

        // --- Accessors --- //
        Task<IIonControlPointPairCollection> GetIonControlPointPairsAsync();
        Task<IStructure> GetTargetStructureAsync();
        Task SetTargetStructureAsync(IStructure value);

        // --- RunAsync --- //
        /// <summary>
        /// Runs a function against the raw ESAPI VMS.TPS.Common.Model.API.IonBeamParameters object safely on the ESAPI thread.
        /// </summary>
        Task RunAsync(Action<VMS.TPS.Common.Model.API.IonBeamParameters> action);

        /// <summary>
        /// Runs a function against the raw ESAPI VMS.TPS.Common.Model.API.IonBeamParameters object safely on the ESAPI thread.
        /// </summary>
        Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.IonBeamParameters, T> func);
    }
}
