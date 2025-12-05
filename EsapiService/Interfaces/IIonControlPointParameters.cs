namespace VMS.TPS.Common.Model.API
{
    public interface IIonControlPointParameters : IControlPointParameters
    {
        IIonSpotParametersCollection FinalSpotList { get; }
        IIonSpotParametersCollection RawSpotList { get; }
        double SnoutPosition { get; }
        System.Threading.Tasks.Task SetSnoutPositionAsync(double value);
    }
}
