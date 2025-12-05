namespace VMS.TPS.Common.Model.API
{
    public interface IScriptContext
    {
        IUser CurrentUser { get; }
        ICourse Course { get; }
        IImage Image { get; }
        IStructureSet StructureSet { get; }
        ICalculation Calculation { get; }
        IActiveStructureCodeDictionaries StructureCodes { get; }
        System.Threading.Tasks.Task SetStructureCodesAsync(IActiveStructureCodeDictionaries value);
        IEquipment Equipment { get; }
        System.Threading.Tasks.Task SetEquipmentAsync(IEquipment value);
        IPatient Patient { get; }
        IPlanSetup PlanSetup { get; }
        IExternalPlanSetup ExternalPlanSetup { get; }
        IBrachyPlanSetup BrachyPlanSetup { get; }
        IIonPlanSetup IonPlanSetup { get; }
        System.Collections.Generic.IReadOnlyList<IPlanSetup> PlansInScope { get; }
        System.Collections.Generic.IReadOnlyList<IExternalPlanSetup> ExternalPlansInScope { get; }
        System.Collections.Generic.IReadOnlyList<IBrachyPlanSetup> BrachyPlansInScope { get; }
        System.Collections.Generic.IReadOnlyList<IIonPlanSetup> IonPlansInScope { get; }
        System.Collections.Generic.IReadOnlyList<IPlanSum> PlanSumsInScope { get; }
        IPlanSum PlanSum { get; }
        string ApplicationName { get; }
        string VersionInfo { get; }
    }
}
