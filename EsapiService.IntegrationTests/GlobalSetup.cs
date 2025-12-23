using NUnit.Framework;
using Esapi.Services.Runners;

// CRITICAL: Prevent parallel execution so we don't fight over the single Patient context
[assembly: Parallelizable(ParallelScope.None)]

namespace Esapi.IntegrationTests
{

    [SetUpFixture]
    public class GlobalSetup
    {
        [OneTimeSetUp]
        public void Init() => TestRunner.Initialize();

        [OneTimeTearDown]
        public void Cleanup() => TestRunner.Dispose();
    }
}
