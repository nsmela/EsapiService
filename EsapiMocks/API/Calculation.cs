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

        public List<Calculation.Algorithm> GetInstalledAlgorithms() => new List<Calculation.Algorithm>();
        public List<Calculation.CalculationModel> GetCalculationModels() => new List<Calculation.CalculationModel>();
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

        /* --- Skipped Members (Not generated) ---
           - .ctor: Explicitly ignored by name
           - Algorithm: No matching factory found (Not Implemented)
           - CalculationModel: No matching factory found (Not Implemented)
        */
    }
}
