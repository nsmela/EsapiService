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
        Task WriteXmlAsync(System.Xml.XmlWriter writer);
        Task<ISegmentVolume> AndAsync(VMS.TPS.Common.Model.API.SegmentVolume other);
        Task<ISegmentVolume> AsymmetricMarginAsync(VMS.TPS.Common.Model.Types.AxisAlignedMargins margins);
        Task<ISegmentVolume> MarginAsync(double marginInMM);
        Task<ISegmentVolume> NotAsync();
        Task<ISegmentVolume> OrAsync(VMS.TPS.Common.Model.API.SegmentVolume other);
        Task<ISegmentVolume> SubAsync(VMS.TPS.Common.Model.API.SegmentVolume other);
        Task<ISegmentVolume> XorAsync(VMS.TPS.Common.Model.API.SegmentVolume other);

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
