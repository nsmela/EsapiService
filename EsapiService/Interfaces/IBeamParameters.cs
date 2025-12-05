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
        Task SetAllLeafPositionsAsync(float[,] leafPositions);
        Task SetJawPositionsAsync(VMS.TPS.Common.Model.Types.VRect<double> positions);
        System.Collections.Generic.IReadOnlyList<IControlPointParameters> ControlPoints { get; }
        VMS.TPS.Common.Model.Types.GantryDirection GantryDirection { get; }
        VMS.TPS.Common.Model.Types.VVector Isocenter { get; }
        Task SetIsocenterAsync(VMS.TPS.Common.Model.Types.VVector value);
        double WeightFactor { get; }
        Task SetWeightFactorAsync(double value);

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
