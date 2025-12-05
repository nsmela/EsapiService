namespace VMS.TPS.Common.Model.API
{
    public interface IOptimizationNormalTissueParameter : IOptimizationParameter
    {
        void WriteXml(System.Xml.XmlWriter writer);
        double DistanceFromTargetBorderInMM { get; }
        double EndDosePercentage { get; }
        double FallOff { get; }
        bool IsAutomatic { get; }
        bool IsAutomaticSbrt { get; }
        bool IsAutomaticSrs { get; }
        double Priority { get; }
        double StartDosePercentage { get; }
    }
}
