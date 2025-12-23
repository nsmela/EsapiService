using NUnit.Framework;
using System.Threading.Tasks;
using Esapi.Services;
using Esapi.Services.Runners;

namespace Esapi.IntegrationTests
{
    public abstract class EsapiTestBase
    {
        protected IEsapiService _esapi;

        // Override these in your test classes
        protected virtual string PatientId => null;
        protected virtual string PlanId => null;

        [SetUp]
        public async Task Setup()
        {
            _esapi = TestRunner.Service;

            // If the test defines a specific Patient/Plan, load it now
            if (PatientId != null)
            {
                await TestRunner.LoadContext(PatientId, PlanId);
            }
        }
    }
}