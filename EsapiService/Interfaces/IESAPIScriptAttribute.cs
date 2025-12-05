namespace VMS.TPS.Common.Model.API
{
    public interface IESAPIScriptAttribute
    {
        bool IsWriteable { get; }
        System.Threading.Tasks.Task SetIsWriteableAsync(bool value);
    }
}
