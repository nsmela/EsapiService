namespace EsapiService.Wrappers
{
    using System.Linq;
    using System.Collections.Generic;
    public class AsyncDose : IDose
    {
        internal readonly VMS.TPS.Common.Model.API.Dose _inner;

        // Store the inner ESAPI object reference
        // internal so other wrappers can access it
        // new to override any inherited _inner fields
        internal new readonly IEsapiService _service;

        public AsyncDose(VMS.TPS.Common.Model.API.Dose inner, IEsapiService service) : base(inner, service)
        {
            _inner = inner;
            _service = service;

            DoseMax3D = inner.DoseMax3D;
            DoseMax3DLocation = inner.DoseMax3DLocation;
            Origin = inner.Origin;
            SeriesUID = inner.SeriesUID;
            UID = inner.UID;
            XDirection = inner.XDirection;
            XRes = inner.XRes;
            XSize = inner.XSize;
            YDirection = inner.YDirection;
            YRes = inner.YRes;
            YSize = inner.YSize;
            ZDirection = inner.ZDirection;
            ZRes = inner.ZRes;
            ZSize = inner.ZSize;
        }

        public void WriteXml(System.Xml.XmlWriter writer) => _inner.WriteXml(writer);
        public VMS.TPS.Common.Model.Types.DoseProfile GetDoseProfile(VMS.TPS.Common.Model.Types.VVector start, VMS.TPS.Common.Model.Types.VVector stop, double[] preallocatedBuffer) => _inner.GetDoseProfile(start, stop, preallocatedBuffer);
        public VMS.TPS.Common.Model.Types.DoseValue GetDoseToPoint(VMS.TPS.Common.Model.Types.VVector at) => _inner.GetDoseToPoint(at);
        public void GetVoxels(int planeIndex, int[,] preallocatedBuffer) => _inner.GetVoxels(planeIndex, preallocatedBuffer);
        public VMS.TPS.Common.Model.Types.DoseValue VoxelToDoseValue(int voxelValue) => _inner.VoxelToDoseValue(voxelValue);
        public VMS.TPS.Common.Model.Types.DoseValue DoseMax3D { get; }
        public VMS.TPS.Common.Model.Types.VVector DoseMax3DLocation { get; }
        public System.Collections.Generic.IReadOnlyList<IIsodose> Isodoses => _inner.Isodoses?.Select(x => new AsyncIsodose(x, _service)).ToList();
        public VMS.TPS.Common.Model.Types.VVector Origin { get; }
        public ISeries Series => _inner.Series is null ? null : new AsyncSeries(_inner.Series, _service);

        public string SeriesUID { get; }
        public string UID { get; }
        public VMS.TPS.Common.Model.Types.VVector XDirection { get; }
        public double XRes { get; }
        public int XSize { get; }
        public VMS.TPS.Common.Model.Types.VVector YDirection { get; }
        public double YRes { get; }
        public int YSize { get; }
        public VMS.TPS.Common.Model.Types.VVector ZDirection { get; }
        public double ZRes { get; }
        public int ZSize { get; }
    }
}
