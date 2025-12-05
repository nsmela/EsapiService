namespace VMS.TPS.Common.Model.API
{
    public interface IOptimizerDVH
    {
        VMS.TPS.Common.Model.Types.DVHPoint[] CurveData { get; }
        IStructure Structure { get; }
    }
}
