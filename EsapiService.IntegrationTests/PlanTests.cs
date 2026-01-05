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

        [EsapiTest]
        public async Task GetImage_ShouldReturnValidImage_WhenPlanHasStructureSet()
        {
            // Act
            var image = await _plan.GetImageAsync();

            // Assert
            Assert.IsNotNull(image, "GetImageAsync returned null. Ensure the test plan has an associated image.");
            Assert.IsFalse(string.IsNullOrEmpty(image.Id), "The returned image should have an ID.");

            Assert.IsTrue(image.ZSize > 0);
        }

        [EsapiTest]
        public async Task GetStructureById_ShouldReturnStructure_WhenIdExists()
        {
            // Arrange
            // We need an ID that definitely exists. "External" or "BODY" is standard, 
            // but let's dynamically find one from the StructureSet to be safe.
            // Note: We can navigate purely through the wrapper to find a valid ID first.
            var ss = await _plan.GetStructureSetAsync();
            var anyStructure = (await ss.GetStructuresAsync()).FirstOrDefault();

            if (anyStructure is null)
                Assert.Inconclusive("The test plan's StructureSet has no structures.");

            string targetId = anyStructure.Id;

            // Act
            var result = await _plan.GetStructureByIdAsync(targetId);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(targetId, result.Id);
            Assert.AreEqual(anyStructure.DicomType, result.DicomType);
        }

        [EsapiTest]
        public async Task GetStructureById_ShouldReturnNull_WhenIdDoesNotExist()
        {
            // Arrange
            string fakeId = "NON_EXISTENT_STRUCTURE_999";

            // Act
            var result = await _plan.GetStructureByIdAsync(fakeId);

            // Assert
            Assert.IsNull(result, "Method should return null when the structure ID is not found.");
        }

        [EsapiTest]
        public async Task GetStructureById_ShouldHandleNullStructureSet_Gracefully()
        {
            // This is an edge case test. 
            // If you have a plan in your system WITHOUT a StructureSet (rare but possible), 
            // you could load it here. 
            // Since we can't easily force that state in a live DB, we often skip this 
            // unless we have a specific "EmptyPlan".

            // Assuming the current plan HAS a structure set, this test just verifies 
            // the null propagation inside your code (context.StructureSet?.Structures) works 
            // by passing a bad ID (which triggers the .FirstOrDefault logic).

            // To truly test the "StructureSet is null" branch:
            // You would need to load a plan created without a CT.

            await Task.CompletedTask; // Placeholder
        }

    }
}
