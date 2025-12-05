namespace VMS.TPS.Common.Model.API
{
    public interface IRadioactiveSourceModel : IApiDataObject
    {
        void WriteXml(System.Xml.XmlWriter writer);
        VMS.TPS.Common.Model.Types.VVector ActiveSize { get; }
        double ActivityConversionFactor { get; }
        string CalculationModel { get; }
        double DoseRateConstant { get; }
        double HalfLife { get; }
        string LiteratureReference { get; }
        string Manufacturer { get; }
        string SourceType { get; }
        string Status { get; }
        System.Collections.Generic.IReadOnlyList<System.DateTime> StatusDate { get; }
        string StatusUserName { get; }
    }
}
