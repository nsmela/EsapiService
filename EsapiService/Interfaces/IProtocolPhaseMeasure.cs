namespace VMS.TPS.Common.Model.API
{
    public interface IProtocolPhaseMeasure : ISerializableObject
    {
        void WriteXml(System.Xml.XmlWriter writer);
        double TargetValue { get; }
        double ActualValue { get; }
        System.Collections.Generic.IReadOnlyList<bool> TargetIsMet { get; }
        VMS.TPS.Common.Model.Types.MeasureModifier Modifier { get; }
        string StructureId { get; }
        VMS.TPS.Common.Model.Types.MeasureType Type { get; }
        string TypeText { get; }
    }
}
