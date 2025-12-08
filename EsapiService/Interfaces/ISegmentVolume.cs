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
    public interface ISegmentVolume : ISerializableObject
    {

        // --- Methods --- //
        Task<ISegmentVolume> AndAsync(ISegmentVolume other);
        Task<ISegmentVolume> AsymmetricMarginAsync(AxisAlignedMargins margins);
        Task<ISegmentVolume> MarginAsync(double marginInMM);
        Task<ISegmentVolume> NotAsync();
        Task<ISegmentVolume> OrAsync(ISegmentVolume other);
        Task<ISegmentVolume> SubAsync(ISegmentVolume other);
        Task<ISegmentVolume> XorAsync(ISegmentVolume other);

        // --- RunAsync --- //
        /// <summary>
        /// Runs a function against the raw ESAPI VMS.TPS.Common.Model.API.SegmentVolume object safely on the ESAPI thread.
        /// </summary>
        Task RunAsync(Action<VMS.TPS.Common.Model.API.SegmentVolume> action);

        /// <summary>
        /// Runs a function against the raw ESAPI VMS.TPS.Common.Model.API.SegmentVolume object safely on the ESAPI thread.
        /// </summary>
        Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.SegmentVolume, T> func);
    }
}
