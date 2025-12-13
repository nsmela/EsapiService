using System;
using System.Collections.Generic;
using System.Linq;
using VMS.TPS.Common.Model.Types;

namespace VMS.TPS.Common.Model.API
{
    public class ScriptEnvironment
    {
        public ScriptEnvironment()
        {
        }

        public void ExecuteScript(System.Reflection.Assembly scriptAssembly, ScriptContext scriptContext, System.Windows.Window window) { }
        public string ApplicationName { get; set; }
        public string VersionInfo { get; set; }
        public string ApiVersionInfo { get; set; }
        public IEnumerable<ApplicationScript> Scripts { get; set; }
        public IEnumerable<ApplicationPackage> Packages { get; set; }
    }
}
