namespace VMS.TPS.Common.Model.API
{
    public interface IScriptEnvironment
    {
        void ExecuteScript(System.Reflection.Assembly scriptAssembly, VMS.TPS.Common.Model.API.ScriptContext scriptContext, System.Windows.Window window);
        string ApplicationName { get; }
        string VersionInfo { get; }
        string ApiVersionInfo { get; }
        System.Collections.Generic.IReadOnlyList<IApplicationScript> Scripts { get; }
        System.Collections.Generic.IReadOnlyList<IApplicationPackage> Packages { get; }
    }
}
