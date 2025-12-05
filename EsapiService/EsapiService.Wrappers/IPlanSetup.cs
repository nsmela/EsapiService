namespace VMS.TPS.Common.Model.API
{
    public interface IPlanSetup
    {
        string Id { get; }
        System.Threading.Tasks.Task SetIdAsync(string value);
        ICourse Course { get; }
        System.Collections.Generic.IReadOnlyList<IStructure> Structures { get; }
        void Calculate();
    }
}
