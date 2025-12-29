using EsapiTestAdapter;
using Esapi.Extensions;
using NUnit.Framework;
using System;
using System.Linq;
using System.Numerics;
using System.Threading.Tasks;
using VMS.TPS.Common.Model.API;

namespace EsapiService.IntegrationTests
{
    public class PlanTests : EsapiTestBase
    {
        private const string PatientId = "OPTIMATE_TEST_PATIENT";
        private const string PlanId = "Plan01";

        [EsapiTest]
        public async Task Verify_PlanId_IsNotNull()
        {
            Console.WriteLine("--- TEST STARTING ---"); // Should appear in Test Details
            System.Diagnostics.Trace.WriteLine("--- TRACE LOG ---"); // Should appear in Output Window -> Tests

            // Arrange
            var patient = await Esapi.OpenPatientByIdAsync(PatientId);
            Assert.That(patient != null, $"Patient with ID {PatientId} not found.");

            // Act
            var plan = await Esapi.OpenPlanByIdAsync(PlanId);

            // Assert
            Assert.That(plan != null, $"Plan with ID {PlanId} not found for patient {PatientId}.");
        }


        [EsapiTest]
        public async Task Verify_Plan_HasBeams()
        {
            // Arrange
            var patient = await Esapi.OpenPatientByIdAsync(PatientId);
            var plan = await Esapi.OpenPlanByIdAsync(PlanId);

            await Esapi.BeginModificationsAsync();

            Assert.That(patient != null, $"Patient with ID {PatientId} not found.");

            // Act
            var beams = await plan.GetBeamsAsync();

            // Assert
            Assert.That(beams.Any(), $"Plan {PlanId} has no beams.");
        }
    }
}
