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
    public interface IBeamParameters
    {
        // --- Simple Properties --- //
        double WeightFactor { get; }
        Task SetWeightFactorAsync(double value);

        // --- Collections --- //
        Task<IReadOnlyList<IControlPointParameters>> GetControlPointsAsync(); // collection proeprty context

        // --- Methods --- //
        Task SetAllLeafPositionsAsync(float[,] leafPositions); // void method
        Task SetJawPositionsAsync(VRect<double> positions); // void method

        // --- RunAsync --- //
        /// <summary>
        /// Runs a function against the raw ESAPI VMS.TPS.Common.Model.API.BeamParameters object safely on the ESAPI thread.
        /// </summary>
        Task RunAsync(Action<VMS.TPS.Common.Model.API.BeamParameters> action);

        /// <summary>
        /// Runs a function against the raw ESAPI VMS.TPS.Common.Model.API.BeamParameters object safely on the ESAPI thread.
        /// </summary>
        Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.BeamParameters, T> func);
    }
}
