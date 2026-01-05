using Esapi.Interfaces;
using EsapiTestAdapter;
using NUnit.Framework;
using System.Threading.Tasks;

namespace EsapiService.IntegrationTests
{

    public class StructureSetTests : EsapiTestBase
    {
        private const string PatientId = "OPTIMATE_TEST_PATIENT";
        private const string PlanId = "Plan01";

        private IPatient _patient;
        private IPlanSetup _plan;
        private IStructureSet _structureSet;

        [EsapiSetup]
        public async Task Setup()
        {
            // 1. Open a valid Patient/Plan
            _patient = await Esapi.OpenPatientByIdAsync(PatientId);
            _plan = await Esapi.OpenPlanByIdAsync(PlanId);

            // 3. Get the StructureSet for testing
            // We use .Result here because Setup is void, but in the test methods we use await.
            _structureSet = await _plan.GetStructureSetAsync();
        }

        [EsapiTest]
        public async Task RecreateStructure_ShouldCreateNew_WhenStructureDoesNotExist()
        {
            string testId = "Test_New";
            string type = "CONTROL";
            await _patient.BeginModificationsAsync();

            // Act
            // Ensure clean slate
            var existing = await _structureSet.GetStructureByIdAsync(testId);
            if (existing != null)
            {
                // We can't easily delete in setup without logic, 
                // but RecreateStructure handles deletion, so we just run it.
                Assert.Warn($"{testId} exists!");
            }

            var result = await _structureSet.RecreateStructureAsync(testId, type);

            // Assert
            Assert.IsNotNull(result, "The created structure should not be null.");
            Assert.AreEqual(testId, result.Id);
            Assert.AreEqual(type, result.DicomType);
        }

        [EsapiTest]
        public async Task RecreateStructure_ShouldOverwrite_WhenStructureAlreadyExists()
        {
            // Arrange
            string testId = "Test_Overwrite";
            string type = "PTV"; // Different type to verify change
            await _patient.BeginModificationsAsync();

            // 1. Create it first manually to ensure it exists
            await _structureSet.RecreateStructureAsync(testId, "CONTROL");

            // Verify it exists as CONTROL
            var temp = await _structureSet.GetStructureByIdAsync(testId);
            Assert.AreEqual("CONTROL", temp.DicomType);

            // Act
            // 2. Recreate it with a NEW type. 
            // If the delete logic fails, Varian throws an exception saying "ID already exists".
            var result = await _structureSet.RecreateStructureAsync(testId, type);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(testId, result.Id);
            Assert.AreEqual(type, result.DicomType, "The structure should have been replaced with the new DICOM type.");
        }

        [EsapiTest]
        public async Task GetStructureById_ShouldReturnStructure_WhenExists()
        {
            // Arrange
            // Pick a structure you know exists, or create one
            string targetId = "External"; // Usually exists
            // Or create one to be safe:
            await _patient.BeginModificationsAsync();
            await _structureSet.RecreateStructureAsync("MyTestStruct", "CONTROL");

            // Act
            var result = await _structureSet.GetStructureByIdAsync("MyTestStruct");

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("MyTestStruct", result.Id);
        }

        [EsapiTest]
        public async Task GetStructureById_ShouldReturnNull_WhenDoesNotExist()
        {
            // Act
            var result = await _structureSet.GetStructureByIdAsync("NON_EXISTENT_ID");

            // Assert
            Assert.IsNull(result, "Should return null for missing structures.");
        }
    }
}