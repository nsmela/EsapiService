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
        double CollimatorAngle { get; }
        int Index { get; }
        VRect<double> JawPositions { get; }
        Task SetJawPositionsAsync(VRect<double> value);
        float[,] LeafPositions { get; }
        Task SetLeafPositionsAsync(float[,] value);
        double PatientSupportAngle { get; }
        double TableTopLateralPosition { get; }
        double TableTopLongitudinalPosition { get; }
        double TableTopVerticalPosition { get; }
        double GantryAngle { get; }
        Task SetGantryAngleAsync(double value);
        double MetersetWeight { get; }
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
