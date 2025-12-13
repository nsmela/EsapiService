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
    public interface ISegmentVolume : ISerializableObject
    {

        // --- Methods --- //
        Task<ISegmentVolume> AndAsync(ISegmentVolume other); // complex method
        Task<ISegmentVolume> AsymmetricMarginAsync(AxisAlignedMargins margins); // complex method
        Task<ISegmentVolume> MarginAsync(double marginInMM); // complex method
        Task<ISegmentVolume> NotAsync(); // complex method
        Task<ISegmentVolume> OrAsync(ISegmentVolume other); // complex method
        Task<ISegmentVolume> SubAsync(ISegmentVolume other); // complex method
        Task<ISegmentVolume> XorAsync(ISegmentVolume other); // complex method

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
