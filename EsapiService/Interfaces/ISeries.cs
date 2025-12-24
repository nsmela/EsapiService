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
        // --- Simple Properties --- //
        string FOR { get; } // simple property
        string ImagingDeviceDepartment { get; } // simple property
        string ImagingDeviceId { get; } // simple property
        string ImagingDeviceManufacturer { get; } // simple property
        string ImagingDeviceModel { get; } // simple property
        string ImagingDeviceSerialNo { get; } // simple property
        SeriesModality Modality { get; } // simple property
        string UID { get; } // simple property

        // --- Accessors --- //
        Task<IStudy> GetStudyAsync(); // read complex property

        // --- Collections --- //
        Task<IReadOnlyList<IImage>> GetImagesAsync(); // collection proeprty context

        // --- Methods --- //
        Task SetImagingDeviceAsync(string imagingDeviceId); // void method

        // --- RunAsync --- //
        /// <summary>
        /// Runs a function against the raw ESAPI VMS.TPS.Common.Model.API.Series object safely on the ESAPI thread.
        /// </summary>
        Task RunAsync(Action<VMS.TPS.Common.Model.API.Series> action);

        /// <summary>
        /// Runs a function against the raw ESAPI VMS.TPS.Common.Model.API.Series object safely on the ESAPI thread.
        /// </summary>
        Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.Series, T> func);

        /// <summary>
        /// Updated the properties from the raw Esapi VMS.TPS.Common.Model.API.Series object
        /// </summary>
        new void Refresh();
    }
}
