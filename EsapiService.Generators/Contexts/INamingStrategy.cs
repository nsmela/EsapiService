using Microsoft.CodeAnalysis;

namespace EsapiService.Generators.Contexts;

public interface INamingStrategy {
    SymbolDisplayFormat DisplayFormat { get; }
    string GetInterfaceNameSpace();
    string GetWrapperNameSpace();
    string GetInterfaceName(string typeName);
    string GetWrapperName(string typeName);
    string GetCollectionInterfaceName(string innerInterfaceName);
    string GetCollectionWrapperName(string innerWrapperName);
    string GetMethodName(string methodName);
    string GetAsyncGetterName(string propertyName);
    string GetAsyncSetterName(string propertyName);

}

public class DefaultNamingStrategy(string wrapperNamespace = "Esapi.Wrappers") : INamingStrategy {
    private readonly string _wrapperNamespace = wrapperNamespace;

    public SymbolDisplayFormat DisplayFormat =>
        SymbolDisplayFormat.FullyQualifiedFormat.WithGlobalNamespaceStyle(SymbolDisplayGlobalNamespaceStyle.Omitted);

    public string GetAsyncGetterName(string propertyName) => $"Get{propertyName}Async";

    public string GetAsyncSetterName(string propertyName) => $"Set{propertyName}Async";

    public string GetCollectionInterfaceName(string innerInterfaceName) => 
        $"IReadOnlyList<{innerInterfaceName}>";

    public string GetCollectionWrapperName(string innerWrapperName) => 
        $"IReadOnlyList<{innerWrapperName}>";

    public string GetInterfaceName(string typeName) => $"I{typeName}";

    public string GetInterfaceNameSpace() => "Esapi.Interfaces";

    public string GetMethodName(string methodName) => $"{methodName}Async";

    public string GetWrapperName(string typeName) => $"Async{typeName}";

    public string GetWrapperNameSpace() => _wrapperNamespace;
}