using EsapiTestAdapter;
using NUnit.Framework;
using System.Threading.Tasks;

namespace EsapiService.IntegrationTests
{

    public class PatientTests : EsapiTestBase
    {
        private const string TestPatientId = "TEST_PATIENT_ESAPISERVICE";

        [EsapiTest]
        public async Task Verify_PatientId_IsNotNull()
        {
            // Arrange

            // Act
            var patient = await Esapi.OpenPatientByIdAsync(TestPatientId);

            // Assert
            Assert.That(patient != null, $"Patient with ID {TestPatientId} not found.");
            Assert.That(!string.IsNullOrEmpty(patient.Id), "Patient ID is null or empty.");
        }

    }
}
