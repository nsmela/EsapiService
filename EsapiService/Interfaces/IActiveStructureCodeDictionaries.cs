namespace VMS.TPS.Common.Model.API
{
    public interface IActiveStructureCodeDictionaries
    {
        IStructureCodeDictionary Fma { get; }
        IStructureCodeDictionary RadLex { get; }
        IStructureCodeDictionary Srt { get; }
        IStructureCodeDictionary VmsStructCode { get; }
    }
}
