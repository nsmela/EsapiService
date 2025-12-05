namespace VMS.TPS.Common.Model.API
{
    public interface IPlanSum : IPlanningItem
    {
        void WriteXml(System.Xml.XmlWriter writer);
        void AddItem(VMS.TPS.Common.Model.API.PlanningItem pi);
        void AddItem(VMS.TPS.Common.Model.API.PlanningItem pi, VMS.TPS.Common.Model.Types.PlanSumOperation operation, double planWeight);
        VMS.TPS.Common.Model.Types.PlanSumOperation GetPlanSumOperation(VMS.TPS.Common.Model.API.PlanSetup planSetupInPlanSum);
        double GetPlanWeight(VMS.TPS.Common.Model.API.PlanSetup planSetupInPlanSum);
        void RemoveItem(VMS.TPS.Common.Model.API.PlanningItem pi);
        void SetPlanSumOperation(VMS.TPS.Common.Model.API.PlanSetup planSetupInPlanSum, VMS.TPS.Common.Model.Types.PlanSumOperation operation);
        void SetPlanWeight(VMS.TPS.Common.Model.API.PlanSetup planSetupInPlanSum, double weight);
        System.Collections.Generic.IReadOnlyList<IPlanSumComponent> PlanSumComponents { get; }
        string Id { get; }
        System.Threading.Tasks.Task SetIdAsync(string value);
        string Name { get; }
        System.Threading.Tasks.Task SetNameAsync(string value);
        System.Collections.Generic.IReadOnlyList<IPlanSetup> PlanSetups { get; }
    }
}
