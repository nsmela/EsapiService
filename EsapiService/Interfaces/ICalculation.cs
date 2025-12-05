namespace VMS.TPS.Common.Model.API
{
    public interface ICalculation
    {
        System.Collections.Generic.IReadOnlyList<VMS.TPS.Common.Model.API.Calculation.Algorithm> GetInstalledAlgorithms();
        System.Collections.Generic.IReadOnlyList<VMS.TPS.Common.Model.API.Calculation.CalculationModel> GetCalculationModels();
        System.Collections.Generic.IReadOnlyList<IDVHEstimationModelStructure> GetDvhEstimationModelStructures(System.Guid modelId);
        System.Collections.Generic.IReadOnlyList<IDVHEstimationModelSummary> GetDvhEstimationModelSummaries();
        string AlgorithmsRootPath { get; }
    }
}
