namespace VMS.TPS.Common.Model.API
{
    public interface IOptimizerObjectiveValue
    {
        IStructure Structure { get; }
        double Value { get; }
    }
}
