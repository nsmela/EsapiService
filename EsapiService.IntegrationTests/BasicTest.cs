using Esapi.Services.Runners;
using NUnit.Framework;
using System.Threading.Tasks;

namespace Esapi.IntegrationTests
{
    [TestFixture]
    public class PatientDataTests
    {
        [Test]
        public async Task LoadPatient_ShouldReturnCorrectId()
        {
            // The lambda passed here runs on a Background Thread.
            // The 'service' passed in is already bound to the STA thread.
            await TestRunner.RunAsync(async (service) =>
            {
                // Act
                // This call will internally marshal to the STA thread 
                // via your EsapiService implementation
                var patient = await service.GetPatientAsync();

                // Assert
                Assert.IsNotNull(patient);
                Assert.AreEqual("TestPatient001", patient.Id);
            });
        }
    }
}