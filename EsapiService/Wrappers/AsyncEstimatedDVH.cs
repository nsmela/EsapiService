namespace EsapiService.Wrappers
{
    public class AsyncEstimatedDVH : IEstimatedDVH
    {
        internal readonly VMS.TPS.Common.Model.API.EstimatedDVH _inner;

        // Store the inner ESAPI object reference
        // internal so other wrappers can access it
        // new to override any inherited _inner fields
        internal new readonly IEsapiService _service;

        public AsyncEstimatedDVH(VMS.TPS.Common.Model.API.EstimatedDVH inner, IEsapiService service) : base(inner, service)
        {
            _inner = inner;
            _service = service;

            CurveData = inner.CurveData;
            PlanSetupId = inner.PlanSetupId;
            StructureId = inner.StructureId;
            TargetDoseLevel = inner.TargetDoseLevel;
            Type = inner.Type;
        }

        public void WriteXml(System.Xml.XmlWriter writer) => _inner.WriteXml(writer);
        public VMS.TPS.Common.Model.Types.DVHPoint[] CurveData { get; }
        public IPlanSetup PlanSetup => _inner.PlanSetup is null ? null : new AsyncPlanSetup(_inner.PlanSetup, _service);

        public string PlanSetupId { get; }
        public IStructure Structure => _inner.Structure is null ? null : new AsyncStructure(_inner.Structure, _service);

        public string StructureId { get; }
        public VMS.TPS.Common.Model.Types.DoseValue TargetDoseLevel { get; }
        public VMS.TPS.Common.Model.Types.DVHEstimateType Type { get; }
    }
}
