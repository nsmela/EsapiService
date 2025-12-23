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
        double ApplicatorLength { get; } // simple property
        Task SetApplicatorLengthAsync(double value);
        int BrachySolidApplicatorPartID { get; } // simple property
        int ChannelNumber { get; } // simple property
        Task SetChannelNumberAsync(int value);
        System.Windows.Media.Color Color { get; } // simple property
        double DeadSpaceLength { get; } // simple property
        Task SetDeadSpaceLengthAsync(double value);
        double FirstSourcePosition { get; } // simple property
        int GroupNumber { get; } // simple property
        double LastSourcePosition { get; } // simple property
        VVector[] Shape { get; } // simple property
        Task SetShapeAsync(VVector[] value);
        double StepSize { get; } // simple property

        // --- Accessors --- //
        Task<IBrachyTreatmentUnit> GetTreatmentUnitAsync(); // read complex property

        // --- Collections --- //
        Task<IReadOnlyList<IBrachyFieldReferencePoint>> GetBrachyFieldReferencePointsAsync(); // collection proeprty context
        Task<IReadOnlyList<ISourcePosition>> GetSourcePositionsAsync(); // collection proeprty context

        // --- Methods --- //
        Task<double> GetSourcePosCenterDistanceFromTipAsync(ISourcePosition sourcePosition); // simple method
        Task<double> GetTotalDwellTimeAsync(); // simple method
        Task LinkRefLineAsync(IStructure refLine); // void method
        Task LinkRefPointAsync(IReferencePoint refPoint); // void method
        Task<(bool result, string message)> SetIdAsync(string id); // out/ref parameter method
        Task<SetSourcePositionsResult> SetSourcePositionsAsync(double stepSize, double firstSourcePosition, double lastSourcePosition); // simple method
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
