namespace VMS.TPS.Common.Model.API
{
    public interface IControlPointParameters
    {
        double CollimatorAngle { get; }
        int Index { get; }
        System.Collections.Generic.IReadOnlyList<double> JawPositions { get; }
        float[,] LeafPositions { get; }
        System.Threading.Tasks.Task SetLeafPositionsAsync(float[,] value);
        double PatientSupportAngle { get; }
        double TableTopLateralPosition { get; }
        double TableTopLongitudinalPosition { get; }
        double TableTopVerticalPosition { get; }
        double GantryAngle { get; }
        System.Threading.Tasks.Task SetGantryAngleAsync(double value);
        double MetersetWeight { get; }
        System.Threading.Tasks.Task SetMetersetWeightAsync(double value);
    }
}
