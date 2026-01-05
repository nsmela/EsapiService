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
    public partial interface IControlPoint : ISerializableObject
    {
        // --- Simple Properties --- //
        double CollimatorAngle { get; } // simple property
        double GantryAngle { get; } // simple property
        int Index { get; } // simple property
        VRect<double> JawPositions { get; } // simple property
        float[,] LeafPositions { get; } // simple property
        double MetersetWeight { get; } // simple property
        double PatientSupportAngle { get; } // simple property
        double TableTopLateralPosition { get; } // simple property
        double TableTopLongitudinalPosition { get; } // simple property
        double TableTopVerticalPosition { get; } // simple property

        // --- Accessors --- //
        Task<IBeam> GetBeamAsync(); // read complex property

        // --- RunAsync --- //
        /// <summary>
        /// Runs a function against the raw ESAPI VMS.TPS.Common.Model.API.ControlPoint object safely on the ESAPI thread.
        /// </summary>
        Task RunAsync(Action<VMS.TPS.Common.Model.API.ControlPoint> action);

        /// <summary>
        /// Runs a function against the raw ESAPI VMS.TPS.Common.Model.API.ControlPoint object safely on the ESAPI thread.
        /// </summary>
        Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.ControlPoint, T> func);

        // --- Validates --- //
        /// <summary>
        /// Verifies is the wrapped ESAPI object isn't null.
        /// </summary>
        new bool IsValid();

        /// <summary>
        /// Verifies is the wrapped ESAPI object is null.
        /// </summary>
        new bool IsNotValid();
    }
}
