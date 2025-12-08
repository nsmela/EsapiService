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
    public interface ICatheter : IApiDataObject
    {
        // --- Simple Properties --- //
        double ApplicatorLength { get; }
        Task SetApplicatorLengthAsync(double value);
        int BrachySolidApplicatorPartID { get; }
        int ChannelNumber { get; }
        Task SetChannelNumberAsync(int value);
        System.Windows.Media.Color Color { get; }
        double DeadSpaceLength { get; }
        Task SetDeadSpaceLengthAsync(double value);
        double FirstSourcePosition { get; }
        int GroupNumber { get; }
        double LastSourcePosition { get; }
        VVector[] Shape { get; }
        Task SetShapeAsync(VVector[] value);
        double StepSize { get; }

        // --- Accessors --- //
        Task<IBrachyTreatmentUnit> GetTreatmentUnitAsync();

        // --- Collections --- //
        Task<IReadOnlyList<IBrachyFieldReferencePoint>> GetBrachyFieldReferencePointsAsync();
        Task<IReadOnlyList<ISourcePosition>> GetSourcePositionsAsync();

        // --- Methods --- //
        Task<double> GetSourcePosCenterDistanceFromTipAsync(ISourcePosition sourcePosition);
        Task<double> GetTotalDwellTimeAsync();
        Task LinkRefLineAsync(IStructure refLine);
        Task LinkRefPointAsync(IReferencePoint refPoint);
        Task<(bool Result, string message)> SetIdAsync(string id);
        Task<SetSourcePositionsResult> SetSourcePositionsAsync(double stepSize, double firstSourcePosition, double lastSourcePosition);
        Task UnlinkRefLineAsync(IStructure refLine);
        Task UnlinkRefPointAsync(IReferencePoint refPoint);

        // --- RunAsync --- //
        /// <summary>
        /// Runs a function against the raw ESAPI VMS.TPS.Common.Model.API.Catheter object safely on the ESAPI thread.
        /// </summary>
        Task RunAsync(Action<VMS.TPS.Common.Model.API.Catheter> action);

        /// <summary>
        /// Runs a function against the raw ESAPI VMS.TPS.Common.Model.API.Catheter object safely on the ESAPI thread.
        /// </summary>
        Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.Catheter, T> func);
    }
}
