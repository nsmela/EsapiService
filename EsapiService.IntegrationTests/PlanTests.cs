using EsapiTestAdapter;
using NUnit.Framework;
using System.Linq;
using System.Threading.Tasks;
using Esapi.Interfaces;
using Esapi.Extensions;

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

        [EsapiTest]
        public async Task Verify_Plan_HasImage()
        {
            // Act
            var image = await _plan.GetImageAsync();

            // Assert
            Assert.That(image, Is.Not.Null, $"Plan {PlanId} has no image.");
        }


        [EsapiTest]
        public async Task Plan_HasNoStructures_ReturnsNull()
        {
            // Arrange 
            var structureId = "INVALID ID";
            // Act
            var structure = await _plan.GetStructureByIdAsync(structureId);

            // Assert
            Assert.That(structure, Is.Null, $"Plan {PlanId} shouldn't have a structure with id {structureId}.");
        }
    }
}
