namespace VMS.TPS.Common.Model.API
{
    public interface IIonPlanSetup : IPlanSetup
    {
        IIonPlanSetup CreateDectVerificationPlan(VMS.TPS.Common.Model.API.Image rhoImage, VMS.TPS.Common.Model.API.Image zImage);
        ICalculationResult CalculateBeamLine();
        ICalculationResult CalculateDose();
        ICalculationResult CalculatePlanUncertaintyDoses();
        IOptimizerResult OptimizeIMPT(VMS.TPS.Common.Model.Types.OptimizationOptionsIMPT options);
        ICalculationResult PostProcessAndCalculateDose();
        ICalculationResult CalculateDoseWithoutPostProcessing();
        ICalculationResult CalculateBeamDeliveryDynamics();
        System.Collections.Generic.IReadOnlyList<string> GetModelsForCalculationType(VMS.TPS.Common.Model.Types.CalculationType calculationType);
        ICalculationResult CalculateDVHEstimates(string modelId, System.Collections.Generic.Dictionary<string, VMS.TPS.Common.Model.Types.DoseValue> targetDoseLevels, System.Collections.Generic.Dictionary<string, string> structureMatches);
        void WriteXml(System.Xml.XmlWriter writer);
        IBeam AddModulatedScanningBeam(VMS.TPS.Common.Model.Types.ProtonBeamMachineParameters machineParameters, string snoutId, double snoutPosition, double gantryAngle, double patientSupportAngle, VMS.TPS.Common.Model.Types.VVector isocenter);
        IEvaluationDose CopyEvaluationDose(VMS.TPS.Common.Model.API.Dose existing);
        IEvaluationDose CreateEvaluationDose();
        VMS.TPS.Common.Model.Types.IonPlanOptimizationMode GetOptimizationMode();
        void SetNormalization(VMS.TPS.Common.Model.Types.IonPlanNormalizationParameters normalizationParameters);
        void SetOptimizationMode(VMS.TPS.Common.Model.Types.IonPlanOptimizationMode mode);
        bool IsPostProcessingNeeded { get; }
        System.Threading.Tasks.Task SetIsPostProcessingNeededAsync(bool value);
        IEvaluationDose DoseAsEvaluationDose { get; }
        System.Collections.Generic.IReadOnlyList<IIonBeam> IonBeams { get; }
    }
}
