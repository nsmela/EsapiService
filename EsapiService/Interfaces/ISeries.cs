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
        string FOR { get; }
        IEnumerable<Image> Images { get; }
        string ImagingDeviceDepartment { get; }
        string ImagingDeviceId { get; }
        string ImagingDeviceManufacturer { get; }
        string ImagingDeviceModel { get; }
        string ImagingDeviceSerialNo { get; }
        string UID { get; }

        // --- Accessors --- //
        Task<IStudy> GetStudyAsync(); // read complex property

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
    }
}
