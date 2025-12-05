namespace VMS.TPS.Common.Model.API
{
    public interface IOptimizerResult : ICalculationResult
    {
        System.Collections.Generic.IReadOnlyList<IOptimizerDVH> StructureDVHs { get; }
        System.Collections.Generic.IReadOnlyList<IOptimizerObjectiveValue> StructureObjectiveValues { get; }
        double MinMUObjectiveValue { get; }
        double TotalObjectiveFunctionValue { get; }
        int NumberOfIMRTOptimizerIterations { get; }
    }
}
