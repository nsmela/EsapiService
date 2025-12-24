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
        // --- Simple Properties --- //
        string PreSelectedRangeShifter1Id { get; } // simple property
        Task SetPreSelectedRangeShifter1IdAsync(string value);
        string PreSelectedRangeShifter1Setting { get; } // simple property
        Task SetPreSelectedRangeShifter1SettingAsync(string value);
        string PreSelectedRangeShifter2Id { get; } // simple property
        Task SetPreSelectedRangeShifter2IdAsync(string value);
        string PreSelectedRangeShifter2Setting { get; } // simple property
        Task SetPreSelectedRangeShifter2SettingAsync(string value);
        string SnoutId { get; } // simple property
        double SnoutPosition { get; } // simple property

        // --- Accessors --- //
        Task<IIonControlPointPairCollection> GetIonControlPointPairsAsync(); // read complex property
        Task<IStructure> GetTargetStructureAsync(); // read complex property
        Task SetTargetStructureAsync(IStructure value); // write complex property

        // --- RunAsync --- //
        /// <summary>
        /// Runs a function against the raw ESAPI VMS.TPS.Common.Model.API.IonBeamParameters object safely on the ESAPI thread.
        /// </summary>
        Task RunAsync(Action<VMS.TPS.Common.Model.API.IonBeamParameters> action);

        /// <summary>
        /// Runs a function against the raw ESAPI VMS.TPS.Common.Model.API.IonBeamParameters object safely on the ESAPI thread.
        /// </summary>
        Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.IonBeamParameters, T> func);

        /// <summary>
        /// Updated the properties from the raw Esapi VMS.TPS.Common.Model.API.IonBeamParameters object
        /// </summary>
        new void Refresh();

        /* --- Skipped Members (Not generated) ---
           - ControlPoints: Shadows base member in wrapped base class
        */
    }
}
