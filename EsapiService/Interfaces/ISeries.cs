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
    public interface ISeries : IApiDataObject
    {
        Task WriteXmlAsync(System.Xml.XmlWriter writer);
        Task SetImagingDeviceAsync(string imagingDeviceId);
        string FOR { get; }
        System.Collections.Generic.IReadOnlyList<IImage> Images { get; }
        string ImagingDeviceDepartment { get; }
        string ImagingDeviceId { get; }
        string ImagingDeviceManufacturer { get; }
        string ImagingDeviceModel { get; }
        string ImagingDeviceSerialNo { get; }
        VMS.TPS.Common.Model.Types.SeriesModality Modality { get; }
        Task<IStudy> GetStudyAsync();
        string UID { get; }

        /// <summary>
        /// Runs a function against the raw ESAPI VMS.TPS.Common.Model.API.Series object safely on the ESAPI thread.
        /// </summary>
        Task RunAsync(Action<VMS.TPS.Common.Model.API.Series> action);

        /// <summary>
        /// Runs a function against the raw ESAPI VMS.TPS.Common.Model.API.Series object safely on the ESAPI thread.
        /// </summary>
        Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.Series, T> func);
    }
}
