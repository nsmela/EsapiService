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
    public interface IIonBeamParameters : IBeamParameters
    {
        System.Collections.Generic.IReadOnlyList<IIonControlPointParameters> ControlPoints { get; }
        string PreSelectedRangeShifter1Id { get; }
        Task SetPreSelectedRangeShifter1IdAsync(string value);
        string PreSelectedRangeShifter1Setting { get; }
        Task SetPreSelectedRangeShifter1SettingAsync(string value);
        string PreSelectedRangeShifter2Id { get; }
        Task SetPreSelectedRangeShifter2IdAsync(string value);
        string PreSelectedRangeShifter2Setting { get; }
        Task SetPreSelectedRangeShifter2SettingAsync(string value);
        Task<IIonControlPointPairCollection> GetIonControlPointPairsAsync();
        string SnoutId { get; }
        double SnoutPosition { get; }
        Task<IStructure> GetTargetStructureAsync();
        Task SetTargetStructureAsync(IStructure value);

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
