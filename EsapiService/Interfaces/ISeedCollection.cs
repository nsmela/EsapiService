namespace VMS.TPS.Common.Model.API
{
    public interface ISeedCollection : IApiDataObject
    {
        void WriteXml(System.Xml.XmlWriter writer);
        System.Collections.Generic.IReadOnlyList<IBrachyFieldReferencePoint> BrachyFieldReferencePoints { get; }
        System.Windows.Media.Color Color { get; }
        System.Collections.Generic.IReadOnlyList<ISourcePosition> SourcePositions { get; }
    }
}
