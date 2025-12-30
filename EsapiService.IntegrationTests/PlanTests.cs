using EsapiTestAdapter;
using NUnit.Framework;
using System.Linq;
using System.Threading.Tasks;
using Esapi.Interfaces;

namespace EsapiService.IntegrationTests
{
    public class PlanTests : EsapiTestBase
    {
        private const string PatientId = "OPTIMATE_TEST_PATIENT";
        private const string PlanId = "Plan01";

        private IPatient _patient;
        private IPlanSetup _plan;

        [EsapiSetup]
        public async Task Setup()
        {
            _patient = await Esapi.OpenPatientByIdAsync(PatientId);
            _plan = await Esapi.OpenPlanByIdAsync(PlanId);
        }

        [EsapiTest]
        public async Task Verify_PlanId_IsNotNull()
        {
            // Assert
            Assert.That(_plan != null, $"Plan with ID {PlanId} not found for patient {PatientId}.");
            Assert.That(_patient != null, $"Patient with ID {PatientId} not found.");
        }

        [EsapiTest]
        public async Task Verify_Plan_HasBeams()
        {
            // Act
            var beams = await _plan.GetBeamsAsync();

            // Assert
            Assert.That(_patient != null, $"Patient with ID {PatientId} not found.");
            Assert.That(beams.Any(), $"Plan {PlanId} has no beams.");
        }
    }
}
