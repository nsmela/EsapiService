namespace EsapiService.Wrappers
{
    public class AsyncEquipment : IEquipment
    {
        internal readonly VMS.TPS.Common.Model.API.Equipment _inner;

        // Store the inner ESAPI object reference
        // internal so other wrappers can access it
        // new to override any inherited _inner fields
        internal readonly IEsapiService _service;

        public AsyncEquipment(VMS.TPS.Common.Model.API.Equipment inner, IEsapiService service)
        {
            _inner = inner;
            _service = service;

        }

        public System.Collections.Generic.IReadOnlyList<IBrachyTreatmentUnit> GetBrachyTreatmentUnits() => _inner.GetBrachyTreatmentUnits()?.Select(x => new AsyncBrachyTreatmentUnit(x, _service)).ToList();
        public System.Collections.Generic.IReadOnlyList<IExternalBeamTreatmentUnit> GetExternalBeamTreatmentUnits() => _inner.GetExternalBeamTreatmentUnits()?.Select(x => new AsyncExternalBeamTreatmentUnit(x, _service)).ToList();
    }
}
