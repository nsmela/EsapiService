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
    public interface IDose : IApiDataObject
    {
        Task WriteXmlAsync(System.Xml.XmlWriter writer);
        Task<VMS.TPS.Common.Model.Types.DoseProfile> GetDoseProfileAsync(VMS.TPS.Common.Model.Types.VVector start, VMS.TPS.Common.Model.Types.VVector stop, double[] preallocatedBuffer);
        Task<VMS.TPS.Common.Model.Types.DoseValue> GetDoseToPointAsync(VMS.TPS.Common.Model.Types.VVector at);
        Task GetVoxelsAsync(int planeIndex, int[,] preallocatedBuffer);
        Task<VMS.TPS.Common.Model.Types.DoseValue> VoxelToDoseValueAsync(int voxelValue);
        VMS.TPS.Common.Model.Types.DoseValue DoseMax3D { get; }
        VMS.TPS.Common.Model.Types.VVector DoseMax3DLocation { get; }
        System.Collections.Generic.IReadOnlyList<IIsodose> Isodoses { get; }
        VMS.TPS.Common.Model.Types.VVector Origin { get; }
        Task<ISeries> GetSeriesAsync();
        string SeriesUID { get; }
        string UID { get; }
        VMS.TPS.Common.Model.Types.VVector XDirection { get; }
        double XRes { get; }
        int XSize { get; }
        VMS.TPS.Common.Model.Types.VVector YDirection { get; }
        double YRes { get; }
        int YSize { get; }
        VMS.TPS.Common.Model.Types.VVector ZDirection { get; }
        double ZRes { get; }
        int ZSize { get; }

        /// <summary>
        /// Runs a function against the raw ESAPI VMS.TPS.Common.Model.API.Dose object safely on the ESAPI thread.
        /// </summary>
        Task RunAsync(Action<VMS.TPS.Common.Model.API.Dose> action);

        /// <summary>
        /// Runs a function against the raw ESAPI VMS.TPS.Common.Model.API.Dose object safely on the ESAPI thread.
        /// </summary>
        Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.Dose, T> func);
    }
}
