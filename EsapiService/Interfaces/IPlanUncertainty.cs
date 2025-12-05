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
    public interface IPlanUncertainty : IApiDataObject
    {
        Task WriteXmlAsync(System.Xml.XmlWriter writer);
        Task<IDVHData> GetDVHCumulativeDataAsync(VMS.TPS.Common.Model.API.Structure structure, VMS.TPS.Common.Model.Types.DoseValuePresentation dosePresentation, VMS.TPS.Common.Model.Types.VolumePresentation volumePresentation, double binWidth);
        System.Collections.Generic.IReadOnlyList<IBeamUncertainty> BeamUncertainties { get; }
        double CalibrationCurveError { get; }
        string DisplayName { get; }
        Task<IDose> GetDoseAsync();
        VMS.TPS.Common.Model.Types.VVector IsocenterShift { get; }
        VMS.TPS.Common.Model.Types.PlanUncertaintyType UncertaintyType { get; }

        /// <summary>
        /// Runs a function against the raw ESAPI VMS.TPS.Common.Model.API.PlanUncertainty object safely on the ESAPI thread.
        /// </summary>
        Task RunAsync(Action<VMS.TPS.Common.Model.API.PlanUncertainty> action);

        /// <summary>
        /// Runs a function against the raw ESAPI VMS.TPS.Common.Model.API.PlanUncertainty object safely on the ESAPI thread.
        /// </summary>
        Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.PlanUncertainty, T> func);
    }
}
