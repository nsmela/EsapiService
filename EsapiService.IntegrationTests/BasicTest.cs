using Esapi.Services.Runners;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

[SetUpFixture]
public class GlobalConfig
{

    [OneTimeSetUp]
    public void Init()
    {
        // Initializes the "UiRunner-like" global thread
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
    public async Task Verify_Beam_Count()
    {
        // Use "StandaloneRunner-like" to run the test
        await IntegrationTestRunner.RunAsync(
            patientId: "TestPatient01",
            planId: "Plan1",
            testBody: async (service) =>
            {
                // This code runs on a background thread.
                // 'service' sends messages to the TestRunner thread.
                var plan = await service.GetPlanAsync();

                Assert.That(plan != null, "Plan should not be null");
            });
    }
}