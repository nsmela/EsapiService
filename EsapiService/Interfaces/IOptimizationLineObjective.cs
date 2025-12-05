namespace VMS.TPS.Common.Model.API
{
    public interface IOptimizationLineObjective : IOptimizationObjective
    {
        void WriteXml(System.Xml.XmlWriter writer);
        VMS.TPS.Common.Model.Types.DVHPoint[] CurveData { get; }
    }
}
