using EsapiTestAdapter;
using System;

namespace EsapiService.IntegrationTests
{

    public class PatientTests : EsapiTestBase
    {
        [EsapiTest]
        public void VerifyPatientIdIsNotNull()
        {
            // Arrange
            var patientId = "TestPatient001";

            // Act
            var patient = Context.App.OpenPatientById(patientId);

            // Assert
            if (patient is null)
            {
                throw new Exception($"Patient with ID {patientId} not found.");
            }
            if (string.IsNullOrEmpty(patient.Id))
            {
                throw new Exception("Patient ID is null or empty.");
            }
        }

    }
}
