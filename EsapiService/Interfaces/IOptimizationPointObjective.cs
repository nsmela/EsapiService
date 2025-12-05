namespace VMS.TPS.Common.Model.API
{
    public interface IOptimizationPointObjective : IOptimizationObjective
    {
        void WriteXml(System.Xml.XmlWriter writer);
        VMS.TPS.Common.Model.Types.DoseValue Dose { get; }
        bool IsRobustObjective { get; }
        System.Threading.Tasks.Task SetIsRobustObjectiveAsync(bool value);
        double Volume { get; }
    }
}
