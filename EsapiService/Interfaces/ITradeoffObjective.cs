namespace VMS.TPS.Common.Model.API
{
    public interface ITradeoffObjective
    {
        int Id { get; }
        System.Collections.Generic.IReadOnlyList<IOptimizationObjective> OptimizationObjectives { get; }
        IStructure Structure { get; }
    }
}
