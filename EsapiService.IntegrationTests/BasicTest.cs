using Esapi.IntegrationTests;
using NUnit.Framework;
using System.Threading.Tasks;

namespace Esapi.IntegrationTests
{
    [TestFixture]

    public class PlanTests : EsapiTestBase
    {
        protected override string PatientId => "CN_HN_RPPO18V2";
        protected override string PlanId => "LneckN_rpV1";

        [Test]
        public async Task Verify_Plan_IsNotNull()
        {
            // _esapi is auto-injected and context is auto-loaded
            var plan = await _esapi.GetPlanAsync();
            Assert.That(plan.Id == PlanId, "Plan Id did not match");
        }
    }
}