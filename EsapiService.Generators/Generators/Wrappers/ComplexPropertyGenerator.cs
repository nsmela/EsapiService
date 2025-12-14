using EsapiService.Generators.Contexts;
using System.Text;

namespace EsapiService.Generators.Generators.Wrappers;

public static class ComplexPropertyGenerator
{
    public static string Generate(ComplexPropertyContext member)
    {
        var sb = new StringBuilder();

        // 1. Async Getter
        sb.AppendLine($"        public async Task<{member.InterfaceName}> {NamingConvention.GetAsyncGetterName(member.Name)}()");
        sb.AppendLine($"        {{");
        sb.AppendLine($"            return await _service.PostAsync(context => ");
        sb.AppendLine($"                _inner.{member.Name} is null ? null : new {member.WrapperName}(_inner.{member.Name}, _service));");
        sb.AppendLine($"        }}");

        // 2. Async Setter
        if (!member.IsReadOnly)
        {
            sb.AppendLine();
            sb.AppendLine($"        public async Task {NamingConvention.GetAsyncSetterName(member.Name)}({member.InterfaceName} value)");
            sb.AppendLine($"        {{");
            sb.AppendLine($"            if (value is null)");
            sb.AppendLine($"            {{");
            sb.AppendLine($"                await _service.PostAsync(context => _inner.{member.Name} = null);");
            sb.AppendLine($"                return;");
            sb.AppendLine($"            }}");

            // FIX: Check against the Interface (IEsapiWrapper<T>) instead of concrete type.
            // This allows us to use the explicit 'Inner' property.
            sb.AppendLine($"            if (value is IEsapiWrapper<{member.Symbol}> wrapper)");
            sb.AppendLine($"            {{");
            sb.AppendLine($"                 await _service.PostAsync(context => _inner.{member.Name} = wrapper.Inner);");
            sb.AppendLine($"                 return;");
            sb.AppendLine($"            }}");

            sb.AppendLine($"            throw new System.ArgumentException(\"Value must be of type {member.WrapperName}\");");
            sb.AppendLine($"        }}");
        }

        return sb.ToString().TrimEnd();
    }
}