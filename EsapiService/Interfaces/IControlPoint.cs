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
    public interface IControlPoint : ISerializableObject
    {
        // --- Simple Properties --- //
        double CollimatorAngle { get; }
        double GantryAngle { get; }
        int Index { get; }
        float[,] LeafPositions { get; }
        double MetersetWeight { get; }
        double PatientSupportAngle { get; }
        double TableTopLateralPosition { get; }
        double TableTopLongitudinalPosition { get; }
        double TableTopVerticalPosition { get; }

        // --- Accessors --- //
        Task<IBeam> GetBeamAsync(); // read complex property

        // --- Collections --- //
        IReadOnlyList<double> JawPositions { get; } // simple collection property

        // --- RunAsync --- //
        /// <summary>
        /// Runs a function against the raw ESAPI VMS.TPS.Common.Model.API.ControlPoint object safely on the ESAPI thread.
        /// </summary>
        Task RunAsync(Action<VMS.TPS.Common.Model.API.ControlPoint> action);

        /// <summary>
        /// Runs a function against the raw ESAPI VMS.TPS.Common.Model.API.ControlPoint object safely on the ESAPI thread.
        /// </summary>
        Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.ControlPoint, T> func);
    }
}
