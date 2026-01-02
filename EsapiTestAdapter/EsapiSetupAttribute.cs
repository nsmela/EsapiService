using System;

namespace EsapiTestAdapter
{
    [AttributeUsage(AttributeTargets.Method, Inherited = true, AllowMultiple = false)]
    public class EsapiSetupAttribute : Attribute
    {
    }
}
