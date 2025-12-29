using Esapi.Services;
using VMS.TPS.Common.Model.API;

namespace EsapiTestAdapter
{
    public abstract class EsapiTestBase : IEsapiTest
    {
        public IEsapiAppContext Context { get; set; }
        protected IEsapiService Esapi => new TestEsapiService(Context);    


        [EsapiTearDown]
        public void ResetState()
        {
            // If the test opened a patient and didn't close it, force it closed here.
            if (Context?.Patient != null)
            {
                Context.App.ClosePatient();
            }
        }
    }
}