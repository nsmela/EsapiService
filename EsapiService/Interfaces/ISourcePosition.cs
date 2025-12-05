namespace VMS.TPS.Common.Model.API
{
    public interface ISourcePosition : IApiDataObject
    {
        void WriteXml(System.Xml.XmlWriter writer);
        double DwellTime { get; }
        System.Collections.Generic.IReadOnlyList<bool> DwellTimeLock { get; }
        double NominalDwellTime { get; }
        System.Threading.Tasks.Task SetNominalDwellTimeAsync(double value);
        IRadioactiveSource RadioactiveSource { get; }
        double[,] Transform { get; }
        VMS.TPS.Common.Model.Types.VVector Translation { get; }
    }
}
