namespace VMS.TPS.Common.Model.API
{
    public interface ITreatmentUnitOperatingLimit : IApiDataObject
    {
        void WriteXml(System.Xml.XmlWriter writer);
        string Label { get; }
        double MaxValue { get; }
        double MinValue { get; }
        System.Collections.Generic.IReadOnlyList<int> Precision { get; }
        string UnitString { get; }
    }
}
