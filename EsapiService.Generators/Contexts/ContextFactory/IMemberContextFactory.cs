using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EsapiService.Generators.Contexts.ContextFactory;

public interface IMemberContextFactory {
    // Returns 0 or 1 item. 
    // This effectively acts as "Maybe I can create a context"
    IEnumerable<IMemberContext> Create(ISymbol symbol, CompilationSettings settings);
}

