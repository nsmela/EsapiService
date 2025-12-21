using Esapi.Services.Runners;
using NUnit.Framework;
using System.Threading.Tasks;

[SetUpFixture]
public class GlobalConfig
{

    [OneTimeSetUp]
    public void Init()
    {
        // Initializes the ESAPI global thread
        TestRunner.Initialize();
    }

    [OneTimeTearDown]
    public void Cleanup()
    {
        TestRunner.Dispose();
    }
}

[TestFixture]
public class PlanTests
{
    [Test]
    public async Task Verify_Plan_IsNotNull()
    {
        // Use "StandaloneRunner-like" to run the test
        await IntegrationTestRunner.RunAsync(
            patientId: "CN_HN_RPPO18V2",
            planId: "LneckN_rpV1",
            testBody: async (service) =>
            {
                // This code runs on a background thread.
                // 'service' sends messages to the TestRunner thread.
                var plan = await service.GetPlanAsync();

                Assert.That(plan != null, "Plan should not be null");
            });
    }
}