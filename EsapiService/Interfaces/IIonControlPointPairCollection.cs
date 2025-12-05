namespace VMS.TPS.Common.Model.API
{
    public interface IIonControlPointPairCollection
    {
        System.Collections.Generic.IReadOnlyList<IIonControlPointPair> GetEnumerator();
        IIonControlPointPair this[] { get; }
        int Count { get; }
    }
}
