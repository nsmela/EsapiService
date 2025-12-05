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
    public interface IFieldReferencePoint : IApiDataObject
    {
        Task WriteXmlAsync(System.Xml.XmlWriter writer);
        double EffectiveDepth { get; }
        VMS.TPS.Common.Model.Types.DoseValue FieldDose { get; }
        bool IsFieldDoseNominal { get; }
        bool IsPrimaryReferencePoint { get; }
        Task<IReferencePoint> GetReferencePointAsync();
        VMS.TPS.Common.Model.Types.VVector RefPointLocation { get; }
        double SSD { get; }

        /// <summary>
        /// Runs a function against the raw ESAPI VMS.TPS.Common.Model.API.FieldReferencePoint object safely on the ESAPI thread.
        /// </summary>
        Task RunAsync(Action<VMS.TPS.Common.Model.API.FieldReferencePoint> action);

        /// <summary>
        /// Runs a function against the raw ESAPI VMS.TPS.Common.Model.API.FieldReferencePoint object safely on the ESAPI thread.
        /// </summary>
        Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.FieldReferencePoint, T> func);
    }
}
