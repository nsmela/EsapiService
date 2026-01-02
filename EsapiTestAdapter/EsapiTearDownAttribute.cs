using System;

namespace EsapiTestAdapter
{
    [AttributeUsage(AttributeTargets.Method)]
    public class EsapiTearDownAttribute : Attribute
    {
    }
}
