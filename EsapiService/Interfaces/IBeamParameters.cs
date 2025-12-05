namespace VMS.TPS.Common.Model.API
{
    public interface IBeamParameters
    {
        void SetAllLeafPositions(float[,] leafPositions);
        void SetJawPositions(VMS.TPS.Common.Model.Types.VRect<double> positions);
        System.Collections.Generic.IReadOnlyList<IControlPointParameters> ControlPoints { get; }
        VMS.TPS.Common.Model.Types.GantryDirection GantryDirection { get; }
        VMS.TPS.Common.Model.Types.VVector Isocenter { get; }
        System.Threading.Tasks.Task SetIsocenterAsync(VMS.TPS.Common.Model.Types.VVector value);
        double WeightFactor { get; }
        System.Threading.Tasks.Task SetWeightFactorAsync(double value);
    }
}
