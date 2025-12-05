namespace VMS.TPS.Common.Model.API
{
    public interface ICalculateBrachy3DDoseResult : ISerializableObject
    {
        void WriteXml(System.Xml.XmlWriter writer);
        System.Collections.Generic.IReadOnlyList<string> Errors { get; }
        double RoundedDwellTimeAdjustRatio { get; }
        bool Success { get; }
    }
}
