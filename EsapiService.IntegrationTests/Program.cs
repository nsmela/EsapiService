using System.Reflection;
using NUnit.Common;
using NUnitLite;

namespace EsapiService.Tests
{
    class Program
    {
        static int Main(string[] args)
        {
            // This executes the tests in this assembly using NUnitLite.
            // It allows you to debug tests by simply hitting "Start" (F5) in Visual Studio.
            return new AutoRun(Assembly.GetExecutingAssembly())
                .Execute(args);
        }
    }
}