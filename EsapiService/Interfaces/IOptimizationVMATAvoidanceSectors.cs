namespace VMS.TPS.Common.Model.API
{
    public interface IOptimizationVMATAvoidanceSectors : IOptimizationParameter
    {
        void WriteXml(System.Xml.XmlWriter writer);
        VMS.TPS.Common.Model.Types.OptimizationAvoidanceSector AvoidanceSector1 { get; }
        VMS.TPS.Common.Model.Types.OptimizationAvoidanceSector AvoidanceSector2 { get; }
        IBeam Beam { get; }
        bool IsValid { get; }
        string ValidationError { get; }
    }
}
