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
    public interface IBeamParameters
    {
        // --- Simple Properties --- //
        GantryDirection GantryDirection { get; }
        VVector Isocenter { get; }
        Task SetIsocenterAsync(VVector value);
        double WeightFactor { get; }
        Task SetWeightFactorAsync(double value);

        // --- Collections --- //
        Task<IReadOnlyList<IControlPointParameters>> GetControlPointsAsync();

        // --- Methods --- //
        Task SetAllLeafPositionsAsync(float[,] leafPositions);
        Task SetJawPositionsAsync(VRect<double> positions);

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
