using System;
using VMS.TPS.Common.Model.API;

namespace EsapiMocks.ExampleProject.Tests
{
    [TestFixture]
    public class PatientReadoutTests
    {
        [Test]
        public void GetPatientSummary_FormatsDataCorrectly()
        {
            // Arrange - Create the Mock Patient (using your generated auto-properties)
            var patient = new Patient
            {
                FirstName = "John",
                LastName = "Doe",
                Id = "123-456",
                DateOfBirth = new DateTime(1980, 1, 1)
            };

            // Act - Call your production code
            string result = PatientReadout.GetPatientSummary(patient);

            // Assert
            Assert.That(result, Does.Contain("Name: John Doe"));
            Assert.That(result, Does.Contain("ID: 123-456"));
            // Note: DateOfBirth.ToString() format depends on system culture, 
            // usually "1/1/1980 12:00:00 AM" or similar.
            Assert.That(result, Does.Contain("1980"));
        }

        [Test]
        public void GetPatientSummary_HandlesEmptyData()
        {
            // Arrange
            var patient = new Patient
            {
                Id = "Unknown",
                // Leave names null/empty to test robustness if needed
            };

            // Act
            string result = PatientReadout.GetPatientSummary(patient);

            // Assert
            Assert.That(result, Does.Contain("ID: Unknown"));
        }
    }
}