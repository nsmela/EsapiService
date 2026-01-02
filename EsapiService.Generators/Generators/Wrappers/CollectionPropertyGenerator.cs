using EsapiService.Generators.Contexts;
using System.Text;

namespace EsapiService.Generators.Generators.Wrappers;
public static class CollectionPropertyGenerator {
    public static string Generate(CollectionPropertyContext member) {
        var sb = new StringBuilder();

        var newMod = member.IsShadowing ? "new " : string.Empty;

        sb.AppendLine($"        public {newMod}async Task<{member.InterfaceName}> {NamingConvention.GetAsyncGetterName(member.Name)}()");
        sb.AppendLine($"        {{");
        sb.AppendLine($"            return await _service.PostAsync(context => ");
        // Note: We cast to Interface explicitly if needed, but List<Wrapper> : List<Interface> isn't covariant in .NET Framework lists easily.
        // However, IReadOnlyList<Wrapper> IS covariant to IReadOnlyList<Interface>.
        sb.AppendLine($"                _inner.{member.Name}?.Select(x => new {member.WrapperItemName}(x, _service)).ToList());");
        sb.AppendLine($"        }}");
        return sb.ToString();
    }
}

