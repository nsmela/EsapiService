namespace VMS.TPS.Common.Model.API
{
    public interface IIonControlPointPair
    {
        void ResizeFinalSpotList(int count);
        void ResizeRawSpotList(int count);
        IIonControlPointParameters EndControlPoint { get; }
        IIonSpotParametersCollection FinalSpotList { get; }
        double NominalBeamEnergy { get; }
        IIonSpotParametersCollection RawSpotList { get; }
        IIonControlPointParameters StartControlPoint { get; }
        int StartIndex { get; }
    }
}
