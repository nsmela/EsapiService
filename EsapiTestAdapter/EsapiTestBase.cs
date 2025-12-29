using Esapi.Services;
using VMS.TPS.Common.Model.API;

namespace EsapiTestAdapter
{
    public abstract class EsapiTestBase : IEsapiTest
    {
        public IEsapiAppContext Context { get; set; }

        // Helpers for cleaner syntax in tests
        protected Patient Patient => Context?.Patient;
        protected PlanSetup Plan => Context?.Plan;
        protected Application App => (Context as IEsapiAppContext)?.App;
    }
}
