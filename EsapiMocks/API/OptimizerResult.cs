using System;
using System.Collections.Generic;
using System.Linq;
using VMS.TPS.Common.Model.Types;

namespace VMS.TPS.Common.Model.API
{
    public class OptimizerResult : CalculationResult
    {
        public OptimizerResult()
        {
        }

        public IEnumerable<OptimizerDVH> StructureDVHs { get; set; }
        public IEnumerable<OptimizerObjectiveValue> StructureObjectiveValues { get; set; }
        public double MinMUObjectiveValue { get; set; }
        public double TotalObjectiveFunctionValue { get; set; }
        public int NumberOfIMRTOptimizerIterations { get; set; }
    }
}
