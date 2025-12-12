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
    public interface ICatheter : IApiDataObject
    {
        // --- Simple Properties --- //
        double ApplicatorLength { get; }
        Task SetApplicatorLengthAsync(double value);
        IEnumerable<BrachyFieldReferencePoint> BrachyFieldReferencePoints { get; }
        int BrachySolidApplicatorPartID { get; }
        int ChannelNumber { get; }
        Task SetChannelNumberAsync(int value);
        System.Windows.Media.Color Color { get; }
        double DeadSpaceLength { get; }
        Task SetDeadSpaceLengthAsync(double value);
        double FirstSourcePosition { get; }
        int GroupNumber { get; }
        double LastSourcePosition { get; }
        IEnumerable<SourcePosition> SourcePositions { get; }
        double StepSize { get; }

        // --- Accessors --- //
        Task<IBrachyTreatmentUnit> GetTreatmentUnitAsync(); // read complex property

        // --- Methods --- //
        Task<double> GetSourcePosCenterDistanceFromTipAsync(ISourcePosition sourcePosition); // simple method
        Task<double> GetTotalDwellTimeAsync(); // simple method
        Task LinkRefLineAsync(IStructure refLine); // void method
        Task LinkRefPointAsync(IReferencePoint refPoint); // void method
        Task<(bool result, string message)> SetIdAsync(string id); // out/ref parameter method
        Task UnlinkRefLineAsync(IStructure refLine); // void method
        Task UnlinkRefPointAsync(IReferencePoint refPoint); // void method

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
