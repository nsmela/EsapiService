using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using Microsoft.VisualStudio.TestPlatform.ObjectModel.Adapter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace EsapiTestAdapter
{
    [ExtensionUri(ExecutorUri)]
    public class EsapiTestExecutor : ITestExecutor
    {
        public const string ExecutorUri = "executor://EsapiTestExecutor";

        // 1. Run from TestCases (The primary entry point)
        public void RunTests(IEnumerable<TestCase> tests, IRunContext runContext, IFrameworkHandle frameworkHandle)
        {
            //System.Diagnostics.Debugger.Launch();
            DebugLog.Write("1. RunTests (TestCases) called."); 
            try
            {
                AdapterTestRunner.InitializeSession();

                foreach (var test in tests)
                {
                    if (runContext.KeepAlive == false && runContext.IsBeingDebugged == false)
                    {
                        // Handle Cancel
                    }

                    frameworkHandle.RecordStart(test);

                    // Instantiate the test class
                    // Note: We create a NEW instance for every test method (Standard Unit Test behavior)
                    var assembly = Assembly.LoadFrom(test.Source);
                    var typeName = test.FullyQualifiedName.Substring(0, test.FullyQualifiedName.LastIndexOf('.'));
                    var type = assembly.GetType(typeName);

                    if (type is null)
                    {
                        frameworkHandle.RecordEnd(test, TestOutcome.NotFound);
                        continue;
                    }

                    var instance = Activator.CreateInstance(type);

                    // Execute
                    var result = AdapterTestRunner.ExecuteTest(test, instance);

                    frameworkHandle.RecordEnd(test, result.Outcome);
                    frameworkHandle.RecordResult(result);
                }
            }
            catch (Exception ex)
            {
                // This catches crashes in the Adapter itself
                frameworkHandle.SendMessage(Microsoft.VisualStudio.TestPlatform.ObjectModel.Logging.TestMessageLevel.Error, $"Adapter Failure: {ex}");
            }
            finally
            {
                // For a persistent ESAPI session, you might choose NOT to shutdown if KeepAlive is true.
                // However, Varian App objects are fragile. Shutting down is safer.
                AdapterTestRunner.Shutdown();
            }
        }

        // 2. Run from Sources (Run All)
        public void RunTests(IEnumerable<string> sources, IRunContext runContext, IFrameworkHandle frameworkHandle)
        {
            // We reuse the discoverer to find the tests, then run them.
            var tests = new List<TestCase>();
            var discoverer = new EsapiTestDiscoverer();

            // A simple sink to capture the discovered tests
            var sink = new ListDiscoverySink(tests);

            discoverer.DiscoverTests(sources, null, null, sink);

            RunTests(tests, runContext, frameworkHandle);
        }

        public void Cancel()
        {
            AdapterTestRunner.Shutdown();
        }
    }

    // Helper sink for the "Run All" scenario
    class ListDiscoverySink : ITestCaseDiscoverySink
    {
        private List<TestCase> _tests;
        public ListDiscoverySink(List<TestCase> list) => _tests = list;
        public void SendTestCase(TestCase discoveredTest) => _tests.Add(discoveredTest);
    }
}