using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using Microsoft.VisualStudio.TestPlatform.ObjectModel.Adapter;
using Microsoft.VisualStudio.TestPlatform.ObjectModel.Logging;
using System.Collections.Generic;

[FileExtension(".dll")]
[DefaultExecutorUri("executor://EsapiTestExecutor")]
public class EsapiTestDiscoverer : ITestDiscoverer
{
    public void DiscoverTests(IEnumerable<string> sources, IDiscoveryContext discoveryContext, IMessageLogger logger, ITestCaseDiscoverySink discoverySink)
    {
        foreach (var dllPath in sources)
        {
            // Inspect the DLL (using Reflection or Mono.Cecil) 
            // Find all methods marked with [EsapiTest]
            // Create a 'TestCase' object for each and send it to discoverySink
        }
    }
}