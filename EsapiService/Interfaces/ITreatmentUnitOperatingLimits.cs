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
    public interface ITreatmentUnitOperatingLimits : ISerializableObject
    {
        Task WriteXmlAsync(System.Xml.XmlWriter writer);
        Task<ITreatmentUnitOperatingLimit> GetCollimatorAngleAsync();
        Task<ITreatmentUnitOperatingLimit> GetGantryAngleAsync();
        Task<ITreatmentUnitOperatingLimit> GetMUAsync();
        Task<ITreatmentUnitOperatingLimit> GetPatientSupportAngleAsync();

        /// <summary>
        /// Runs a function against the raw ESAPI VMS.TPS.Common.Model.API.TreatmentUnitOperatingLimits object safely on the ESAPI thread.
        /// </summary>
        Task RunAsync(Action<VMS.TPS.Common.Model.API.TreatmentUnitOperatingLimits> action);

        /// <summary>
        /// Runs a function against the raw ESAPI VMS.TPS.Common.Model.API.TreatmentUnitOperatingLimits object safely on the ESAPI thread.
        /// </summary>
        Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.TreatmentUnitOperatingLimits, T> func);
    }
}
