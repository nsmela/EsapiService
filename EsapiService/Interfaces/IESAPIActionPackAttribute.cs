namespace VMS.TPS.Common.Model.API
{
    public interface IESAPIActionPackAttribute
    {
        bool IsWriteable { get; }
        System.Threading.Tasks.Task SetIsWriteableAsync(bool value);
    }
}
