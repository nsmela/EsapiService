using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EsapiService.Generators.Contexts;

// Examples of concern:
//     BrachyPlanSetup: public ChangeBrachyTreatmentUnitResult ChangeTreatmentUnit(BrachyTreatmentUnit treatmentUnit, bool keepDoseIntact, out List<string> messages)
//     Catheter: public bool SetId(string id, out string message)
//     Patient: public bool CanAddEmptyPhantom(out string errorMessage)

public record ParameterContext(
    string Name,
    string Type,             // The inner type (e.g. "Varian.BrachyTreatmentUnit")
    string InterfaceType,    // The public type (e.g. "IBrachyTreatmentUnit")
    string WrapperType,      // The wrapper type (e.g. "AsyncBrachyTreatmentUnit")
    bool IsWrappable,        // True if it's a Varian object we are wrapping
    bool IsOut,              // True if 'out'
    bool IsRef,               // True if 'ref'
    bool IsCollection = false, // is a Enumerable, List, etc
    string InnerType = "" // if a collection, what is the inner type
);
