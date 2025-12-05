namespace VMS.TPS.Common.Model.API
{
    public interface IOptimizationIMRTBeamParameter : IOptimizationParameter
    {
        void WriteXml(System.Xml.XmlWriter writer);
        IBeam Beam { get; }
        string BeamId { get; }
        bool Excluded { get; }
        bool FixedJaws { get; }
        double SmoothX { get; }
        double SmoothY { get; }
    }
}
