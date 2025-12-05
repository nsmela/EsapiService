namespace VMS.TPS.Common.Model.API
{
    public interface IRadioactiveSource : IApiDataObject
    {
        void WriteXml(System.Xml.XmlWriter writer);
        System.Collections.Generic.IReadOnlyList<System.DateTime> CalibrationDate { get; }
        bool NominalActivity { get; }
        IRadioactiveSourceModel RadioactiveSourceModel { get; }
        string SerialNumber { get; }
        double Strength { get; }
    }
}
