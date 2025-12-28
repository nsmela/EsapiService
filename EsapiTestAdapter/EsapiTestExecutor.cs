using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using Microsoft.VisualStudio.TestPlatform.ObjectModel.Adapter;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace EsapiTestAdapter
{
    [ExtensionUri(ExecutorUri)]
    public class EsapiTestExecutor : ITestExecutor
    {
        // This URI must match what you put in the Discoverer
        public const string ExecutorUri = "executor://EsapiTestExecutor";

        // Called when user clicks "Run Selected Tests" or "Run All"
        public void RunTests(IEnumerable<TestCase> tests, IRunContext runContext, IFrameworkHandle frameworkHandle)
        {
            try
            {
                // 1. Ensure Varian is loaded and ready
                AdapterTestRunner.InitializeSession();

                foreach (var test in tests)
                {
                    if (runContext.KeepAlive == false && runContext.IsBeingDebugged == false)
                    {
                        // Check for cancel
                    }

                    frameworkHandle.RecordStart(test);

                    // 2. Instantiate the User's Test Class
                    // We do this here (on a ThreadPool thread usually) or inside the Runner.
                    // Ideally, we instantiate inside the runner to be safe, but let's try here.
                    var assembly = Assembly.LoadFrom(test.Source);
                    // Use the FullClassName property we set during Discovery
                    var type = assembly.GetType(test.FullyQualifiedName.Substring(0, test.FullyQualifiedName.LastIndexOf('.')));
                    var instance = Activator.CreateInstance(type);

                    // 3. Execute
                    var result = AdapterTestRunner.ExecuteTest(test, instance);

                    frameworkHandle.RecordEnd(test, result.Outcome);
                    frameworkHandle.RecordResult(result);
                }
            }
            catch (Exception ex)
            {
                // Log generic framework crashes
                // In a real adapter, you'd log to frameworkHandle.SendMessage
            }
            finally
            {
                // 4. Cleanup
                // If KeepAlive is true (standard VS behavior), we might want to skip Shutdown 
                // to make the next run faster. But for safety with Varian, simpler to Shutdown.
                AdapterTestRunner.Shutdown();
            }
        }

        // Called when user clicks "Run All" (Raw sources)
        public void RunTests(IEnumerable<string> sources, IRunContext runContext, IFrameworkHandle frameworkHandle)
        {
            // Simple implementation: re-discover and run
            // In a real implementation, you'd reuse the discoverer code to get TestCases
        }

        public void Cancel()
        {
            AdapterTestRunner.Shutdown();
        }
    }
}