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
    public interface IReferencePoint : IApiDataObject
    {
        Task WriteXmlAsync(System.Xml.XmlWriter writer);
        Task<bool> AddLocationAsync(VMS.TPS.Common.Model.API.Image Image, double x, double y, double z, System.Text.StringBuilder errorHint);
        Task<bool> ChangeLocationAsync(VMS.TPS.Common.Model.API.Image Image, double x, double y, double z, System.Text.StringBuilder errorHint);
        Task<VMS.TPS.Common.Model.Types.VVector> GetReferencePointLocationAsync(VMS.TPS.Common.Model.API.Image Image);
        Task<VMS.TPS.Common.Model.Types.VVector> GetReferencePointLocationAsync(VMS.TPS.Common.Model.API.PlanSetup planSetup);
        Task<bool> HasLocationAsync(VMS.TPS.Common.Model.API.PlanSetup planSetup);
        Task<bool> RemoveLocationAsync(VMS.TPS.Common.Model.API.Image Image, System.Text.StringBuilder errorHint);
        string Id { get; }
        Task SetIdAsync(string value);
        VMS.TPS.Common.Model.Types.DoseValue DailyDoseLimit { get; }
        Task SetDailyDoseLimitAsync(VMS.TPS.Common.Model.Types.DoseValue value);
        VMS.TPS.Common.Model.Types.DoseValue SessionDoseLimit { get; }
        Task SetSessionDoseLimitAsync(VMS.TPS.Common.Model.Types.DoseValue value);
        VMS.TPS.Common.Model.Types.DoseValue TotalDoseLimit { get; }
        Task SetTotalDoseLimitAsync(VMS.TPS.Common.Model.Types.DoseValue value);

        /// <summary>
        /// Runs a function against the raw ESAPI VMS.TPS.Common.Model.API.ReferencePoint object safely on the ESAPI thread.
        /// </summary>
        Task RunAsync(Action<VMS.TPS.Common.Model.API.ReferencePoint> action);

        /// <summary>
        /// Runs a function against the raw ESAPI VMS.TPS.Common.Model.API.ReferencePoint object safely on the ESAPI thread.
        /// </summary>
        Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.ReferencePoint, T> func);
    }
}
