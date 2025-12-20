using Esapi.Services.Runners;
using NUnit.Framework;
using System;

[SetUpFixture]
public class GlobalSetup
{
    [OneTimeSetUp]
    public void RunBeforeAnyTests()
    {
        TestRunner.Initialize();
    }

    [OneTimeTearDown]
    public void RunAfterAllTests()
    {
        TestRunner.Dispose();
    }
}