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
    public interface ISeries : IApiDataObject
    {
        // --- Simple Properties --- //
        string FOR { get; }
        string ImagingDeviceDepartment { get; }
        string ImagingDeviceId { get; }
        string ImagingDeviceManufacturer { get; }
        string ImagingDeviceModel { get; }
        string ImagingDeviceSerialNo { get; }
        SeriesModality Modality { get; }
        string UID { get; }

        // --- Accessors --- //
        Task<IStudy> GetStudyAsync();

        // --- Collections --- //
        Task<IReadOnlyList<IImage>> GetImagesAsync();

        // --- Methods --- //
        Task SetImagingDeviceAsync(string imagingDeviceId);

        // --- RunAsync --- //
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
