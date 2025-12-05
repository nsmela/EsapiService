namespace EsapiService.Wrappers
{
    public class AsyncEvaluationDose : IEvaluationDose
    {
        internal readonly VMS.TPS.Common.Model.API.EvaluationDose _inner;

        // Store the inner ESAPI object reference
        // internal so other wrappers can access it
        // new to override any inherited _inner fields
        internal new readonly IEsapiService _service;

        public AsyncEvaluationDose(VMS.TPS.Common.Model.API.EvaluationDose inner, IEsapiService service) : base(inner, service)
        {
            _inner = inner;
            _service = service;

        }

        public void WriteXml(System.Xml.XmlWriter writer) => _inner.WriteXml(writer);
        public int DoseValueToVoxel(VMS.TPS.Common.Model.Types.DoseValue doseValue) => _inner.DoseValueToVoxel(doseValue);
        public void SetVoxels(int planeIndex, int[,] values) => _inner.SetVoxels(planeIndex, values);
    }
}
