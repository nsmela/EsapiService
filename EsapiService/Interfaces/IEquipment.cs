namespace VMS.TPS.Common.Model.API
{
    public interface IEquipment
    {
        System.Collections.Generic.IReadOnlyList<IBrachyTreatmentUnit> GetBrachyTreatmentUnits();
        System.Collections.Generic.IReadOnlyList<IExternalBeamTreatmentUnit> GetExternalBeamTreatmentUnits();
    }
}
