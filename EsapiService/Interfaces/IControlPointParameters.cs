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
    public interface IControlPointParameters
    {
        // --- Simple Properties --- //
        double CollimatorAngle { get; } // simple property
        int Index { get; } // simple property
        VRect<double> JawPositions { get; } // simple property
        Task SetJawPositionsAsync(VRect<double> value);
        float[,] LeafPositions { get; } // simple property
        Task SetLeafPositionsAsync(float[,] value);
        double PatientSupportAngle { get; } // simple property
        double TableTopLateralPosition { get; } // simple property
        double TableTopLongitudinalPosition { get; } // simple property
        double TableTopVerticalPosition { get; } // simple property
        double GantryAngle { get; } // simple property
        Task SetGantryAngleAsync(double value);
        double MetersetWeight { get; } // simple property
        Task SetMetersetWeightAsync(double value);

        // --- RunAsync --- //
        /// <summary>
        /// Runs a function against the raw ESAPI VMS.TPS.Common.Model.API.ControlPointParameters object safely on the ESAPI thread.
        /// </summary>
        Task RunAsync(Action<VMS.TPS.Common.Model.API.ControlPointParameters> action);

        /// <summary>
        /// Runs a function against the raw ESAPI VMS.TPS.Common.Model.API.ControlPointParameters object safely on the ESAPI thread.
        /// </summary>
        Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.ControlPointParameters, T> func);
    }
}
