using EsapiService.Generators.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EsapiService.Generators.Generators.Wrappers;

public static class WrapperTools {
    internal static string BuildCallArguments(IEnumerable<ParameterContext> parameters) {
        // Transforms:
        // (IBolus bolus) -> ((AsyncBolus)bolus)._inner
        // (int x)        -> x

        var args = parameters.Select(p => {
            if (!p.IsWrappable) { return p.Name; }

            if (p.IsCollection) {
                // (orderedBeams.Select(x => ((AsyncBeam)x)._inner)))
                return $"({p.Name}.Select(x => (({p.InnerType})x)._inner))";
            }

            // Cast to the concrete Wrapper type and access _inner
            return $"(({p.WrapperType}){p.Name})._inner";
        });

        return string.Join(", ", args);
    }
}

