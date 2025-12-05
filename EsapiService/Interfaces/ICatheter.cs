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
        Task WriteXmlAsync(System.Xml.XmlWriter writer);
        Task<double> GetSourcePosCenterDistanceFromTipAsync(VMS.TPS.Common.Model.API.SourcePosition sourcePosition);
        Task<double> GetTotalDwellTimeAsync();
        Task LinkRefLineAsync(VMS.TPS.Common.Model.API.Structure refLine);
        Task LinkRefPointAsync(VMS.TPS.Common.Model.API.ReferencePoint refPoint);
        Task<(bool Result, string message)> SetIdAsync(string id);
        Task<VMS.TPS.Common.Model.Types.SetSourcePositionsResult> SetSourcePositionsAsync(double stepSize, double firstSourcePosition, double lastSourcePosition);
        Task UnlinkRefLineAsync(VMS.TPS.Common.Model.API.Structure refLine);
        Task UnlinkRefPointAsync(VMS.TPS.Common.Model.API.ReferencePoint refPoint);
        double ApplicatorLength { get; }
        Task SetApplicatorLengthAsync(double value);
        System.Collections.Generic.IReadOnlyList<IBrachyFieldReferencePoint> BrachyFieldReferencePoints { get; }
        int BrachySolidApplicatorPartID { get; }
        int ChannelNumber { get; }
        Task SetChannelNumberAsync(int value);
        System.Windows.Media.Color Color { get; }
        double DeadSpaceLength { get; }
        Task SetDeadSpaceLengthAsync(double value);
        double FirstSourcePosition { get; }
        int GroupNumber { get; }
        double LastSourcePosition { get; }
        VMS.TPS.Common.Model.Types.VVector[] Shape { get; }
        Task SetShapeAsync(VMS.TPS.Common.Model.Types.VVector[] value);
        System.Collections.Generic.IReadOnlyList<ISourcePosition> SourcePositions { get; }
        double StepSize { get; }
        Task<IBrachyTreatmentUnit> GetTreatmentUnitAsync();

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
