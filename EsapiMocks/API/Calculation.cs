using System;
using System.Collections.Generic;
using System.Linq;
using VMS.TPS.Common.Model.Types;

namespace VMS.TPS.Common.Model.API
{
    public class Calculation
    {
        public Calculation()
        {
        }

        public IEnumerable<DVHEstimationModelStructure> GetDvhEstimationModelStructures(System.Guid modelId) => new List<DVHEstimationModelStructure>();
        public IEnumerable<DVHEstimationModelSummary> GetDvhEstimationModelSummaries() => new List<DVHEstimationModelSummary>();
        public string AlgorithmsRootPath { get; set; }

        // --- Nested Types ---
        public class Algorithm
        {
            public Algorithm()
            {
            }

            public string Name { get; set; }
            public string Version { get; set; }
        }
        public class CalculationModel
        {
            public CalculationModel()
            {
            }

            public string ModelName { get; set; }
            public string AlgorithmName { get; set; }
            public string AlgorithmVersion { get; set; }
            public string BeamDataDirectory { get; set; }
            public string DefaultOptionsFilePath { get; set; }
            public bool EnabledFlag { get; set; }
        }
    }
}
