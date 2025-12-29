using Esapi.Services;

namespace EsapiTestAdapter
{
    public interface IEsapiTest
    {
        // The runner will set this property before the test method executes
        IEsapiAppContext Context { get; set; }
    }
}
