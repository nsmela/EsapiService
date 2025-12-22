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
        int BrachySolidApplicatorPartID { get; } // simple property
        int ChannelNumber { get; } // simple property
        System.Windows.Media.Color Color { get; } // simple property
        double DeadSpaceLength { get; } // simple property
        VVector[] Shape { get; } // simple property
        double StepSize { get; } // simple property

        // --- Accessors --- //
        Task<IBrachyTreatmentUnit> GetTreatmentUnitAsync(); // read complex property

        // --- Collections --- //
        Task<IReadOnlyList<IBrachyFieldReferencePoint>> GetBrachyFieldReferencePointsAsync(); // collection proeprty context
        Task<IReadOnlyList<ISourcePosition>> GetSourcePositionsAsync(); // collection proeprty context

        // --- Methods --- //
        Task<double> GetSourcePosCenterDistanceFromTipAsync(ISourcePosition sourcePosition); // simple method
        Task<double> GetTotalDwellTimeAsync(); // simple method

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
