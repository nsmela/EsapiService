
namespace EsapiService.Generators.Contexts;
    public static class NamingConvention {
    /// <summary>
    /// Transforms "PlanSetup" -> "IPlanSetup"
    /// </summary>
    public static string GetInterfaceName(string typeName) => $"I{typeName}";

    /// <summary>
    /// Transforms "PlanSetup" -> "AsyncPlanSetup"
    /// </summary>
    public static string GetWrapperName(string typeName) => $"Async{typeName}";

    /// <summary>
    /// Standardizes "System.Collections.Generic.IEnumerable<T>" to "IReadOnlyList<T>"
    /// </summary>
    public static string GetCollectionInterfaceName(string innerInterfaceName)
        => $"IReadOnlyList<{innerInterfaceName}>";

    public static string GetCollectionWrapperName(string innerWrapperName)
        => $"IReadOnlyList<{innerWrapperName}>";

    public static string GetWrapperNameSpace() => "Esapi.Wrappers";
    public static string GetInterfaceNameSpace() => "Esapi.Interfaces";

    /// <summary>
    /// e.g. "Calculate" -> "CalculateAsync"
    /// </summary>
    public static string GetMethodName(string methodName) => $"{methodName}Async";

    /// <summary>
    /// e.g. "Plan" -> "GetPlanAsync"
    /// </summary>
    public static string GetAsyncGetterName(string propertyName) => $"Get{propertyName}Async";

    /// <summary>
    /// e.g. "Plan" -> "SetPlanAsync"
    /// </summary>
    public static string GetAsyncSetterName(string propertyName) => $"Set{propertyName}Async";
}

