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
    public interface IBrachyFieldReferencePoint : IApiDataObject
    {
        Task WriteXmlAsync(System.Xml.XmlWriter writer);
        VMS.TPS.Common.Model.Types.DoseValue FieldDose { get; }
        bool IsFieldDoseNominal { get; }
        bool IsPrimaryReferencePoint { get; }
        Task<IReferencePoint> GetReferencePointAsync();
        VMS.TPS.Common.Model.Types.VVector RefPointLocation { get; }

        /// <summary>
        /// Runs a function against the raw ESAPI VMS.TPS.Common.Model.API.BrachyFieldReferencePoint object safely on the ESAPI thread.
        /// </summary>
        Task RunAsync(Action<VMS.TPS.Common.Model.API.BrachyFieldReferencePoint> action);

        /// <summary>
        /// Runs a function against the raw ESAPI VMS.TPS.Common.Model.API.BrachyFieldReferencePoint object safely on the ESAPI thread.
        /// </summary>
        Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.BrachyFieldReferencePoint, T> func);
    }
}
