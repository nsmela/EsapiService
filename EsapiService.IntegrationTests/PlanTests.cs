using EsapiTestAdapter;
using NUnit.Framework;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace EsapiService.IntegrationTests
{
    public class PlanTests : EsapiTestBase
    {
        private const string TestPatientId = "OPTIMATE_TEST_PATIENT";
        private const string TestPlanId = "Plan01";

        [EsapiTest]
        public async Task Verify_PlanId_IsNotNull()
        {
            Console.WriteLine("--- TEST STARTING ---"); // Should appear in Test Details
            System.Diagnostics.Trace.WriteLine("--- TRACE LOG ---"); // Should appear in Output Window -> Tests

            // Arrange
            var patient = await Esapi.OpenPatientByIdAsync(TestPatientId);
            Assert.That(patient != null, $"Patient with ID {TestPatientId} not found.");

            // Act
            var courses = await patient.GetCoursesAsync();
            var plans = await courses[0].GetPlanSetupsAsync();

            var plan = plans.FirstOrDefault(p => p.Id == TestPlanId);

            // Assert
            Assert.That(plans.Any(), $"Patient {TestPatientId} has no plans");
            Assert.That(plan != null, $"Plan with ID {TestPlanId} not found for patient {TestPatientId}.");
        }

    }
}
