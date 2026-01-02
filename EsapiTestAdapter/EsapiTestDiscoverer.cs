using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using Microsoft.VisualStudio.TestPlatform.ObjectModel.Adapter;
using Microsoft.VisualStudio.TestPlatform.ObjectModel.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace EsapiTestAdapter // Ensure namespace matches
{
    [FileExtension(".dll")]
    [DefaultExecutorUri(EsapiTestExecutor.ExecutorUri)]
    public class EsapiTestDiscoverer : ITestDiscoverer
    {
        public void DiscoverTests(IEnumerable<string> sources, IDiscoveryContext discoveryContext, IMessageLogger logger, ITestCaseDiscoverySink discoverySink)
        {
            foreach (var dllPath in sources)
            {
                if (!File.Exists(dllPath)) continue;

                try
                {
                    // FIX 1: Use LoadFrom. 
                    // This sets the "Load Context" to the folder containing the DLL, 
                    // allowing it to find neighbor DLLs (like EsapiTestAdapter.dll or Varian DLLs).
                    var assembly = Assembly.LoadFrom(dllPath);

                    foreach (var type in assembly.GetTypes())
                    {
                        foreach (var method in type.GetMethods())
                        {
                            // Check attribute by name to avoid version conflicts
                            var isEsapiTest = method.GetCustomAttributes(false)
                                                    .Any(a => a.GetType().Name == "EsapiTestAttribute");

                            if (isEsapiTest)
                            {
                                var testCase = new TestCase(
                                    $"{type.FullName}.{method.Name}",
                                    new Uri(EsapiTestExecutor.ExecutorUri),
                                    dllPath
                                )
                                {
                                    DisplayName = method.Name,
                                    CodeFilePath = null,
                                    LineNumber = 0
                                };

                                discoverySink.SendTestCase(testCase);
                            }
                        }
                    }
                }
                // FIX 2: Catch ReflectionTypeLoadException specifically.
                // This happens when the Runner (x86) tries to load 64-bit ESAPI, 
                // or when Varian DLLs are missing from the folder.
                catch (ReflectionTypeLoadException rtle)
                {
                    var loaderMessages = string.Join("\n", rtle.LoaderExceptions.Select(e => e.Message).Distinct());
                    logger.SendMessage(TestMessageLevel.Error,
                        $"[EsapiTestAdapter] Type Load Error in {Path.GetFileName(dllPath)}:\n{loaderMessages}");
                }
                catch (Exception ex)
                {
                    logger.SendMessage(TestMessageLevel.Error,
                        $"[EsapiTestAdapter] Discovery Error in {Path.GetFileName(dllPath)}: {ex.Message}");
                }
            }
        }
    }
}