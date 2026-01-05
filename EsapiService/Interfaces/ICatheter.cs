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
    public partial interface ICatheter : IApiDataObject
    {
        // --- Simple Properties --- //
        double ApplicatorLength { get; set; } // simple property
        int BrachySolidApplicatorPartID { get; } // simple property
        int ChannelNumber { get; set; } // simple property
        System.Windows.Media.Color Color { get; } // simple property
        double DeadSpaceLength { get; set; } // simple property
        double FirstSourcePosition { get; } // simple property
        int GroupNumber { get; } // simple property
        double LastSourcePosition { get; } // simple property
        VVector[] Shape { get; set; } // simple property
        double StepSize { get; } // simple property

        // --- Accessors --- //
        Task<IBrachyTreatmentUnit> GetTreatmentUnitAsync(); // read complex property

        // --- Collections --- //
        Task<IReadOnlyList<IBrachyFieldReferencePoint>> GetBrachyFieldReferencePointsAsync(); // collection property context
        Task<IReadOnlyList<ISourcePosition>> GetSourcePositionsAsync(); // collection property context

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
