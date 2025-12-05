namespace VMS.TPS.Common.Model.API
{
    public interface IExternalPlanSetup : IPlanSetup
    {
        ICalculationResult CalculateDoseWithPresetValues(System.Collections.Generic.List<System.Collections.Generic.KeyValuePair<string, VMS.TPS.Common.Model.Types.MetersetValue>> presetValues);
        ICalculationResult CalculateDose();
        ICalculationResult CalculatePlanUncertaintyDoses();
        ICalculationResult CalculateLeafMotionsAndDose();
        ICalculationResult CalculateLeafMotions();
        ICalculationResult CalculateLeafMotions(VMS.TPS.Common.Model.Types.LMCVOptions options);
        ICalculationResult CalculateLeafMotions(VMS.TPS.Common.Model.Types.SmartLMCOptions options);
        ICalculationResult CalculateLeafMotions(VMS.TPS.Common.Model.Types.LMCMSSOptions options);
        System.Collections.Generic.IReadOnlyList<string> GetModelsForCalculationType(VMS.TPS.Common.Model.Types.CalculationType calculationType);
        IOptimizerResult Optimize(int maxIterations);
        IOptimizerResult Optimize(int maxIterations, VMS.TPS.Common.Model.Types.OptimizationOption optimizationOption);
        IOptimizerResult Optimize(int maxIterations, VMS.TPS.Common.Model.Types.OptimizationOption optimizationOption, string mlcId);
        IOptimizerResult Optimize();
        IOptimizerResult Optimize(VMS.TPS.Common.Model.Types.OptimizationOptionsIMRT options);
        IOptimizerResult OptimizeVMAT(string mlcId);
        IOptimizerResult OptimizeVMAT();
        IOptimizerResult OptimizeVMAT(VMS.TPS.Common.Model.Types.OptimizationOptionsVMAT options);
        ICalculationResult CalculateDVHEstimates(string modelId, System.Collections.Generic.Dictionary<string, VMS.TPS.Common.Model.Types.DoseValue> targetDoseLevels, System.Collections.Generic.Dictionary<string, string> structureMatches);
        void WriteXml(System.Xml.XmlWriter writer);
        IBeam AddArcBeam(VMS.TPS.Common.Model.Types.ExternalBeamMachineParameters machineParameters, VMS.TPS.Common.Model.Types.VRect<double> jawPositions, double collimatorAngle, double gantryAngle, double gantryStop, VMS.TPS.Common.Model.Types.GantryDirection gantryDirection, double patientSupportAngle, VMS.TPS.Common.Model.Types.VVector isocenter);
        IBeam AddConformalArcBeam(VMS.TPS.Common.Model.Types.ExternalBeamMachineParameters machineParameters, double collimatorAngle, int controlPointCount, double gantryAngle, double gantryStop, VMS.TPS.Common.Model.Types.GantryDirection gantryDirection, double patientSupportAngle, VMS.TPS.Common.Model.Types.VVector isocenter);
        IBeam AddFixedSequenceBeam(VMS.TPS.Common.Model.Types.ExternalBeamMachineParameters machineParameters, double collimatorAngle, double gantryAngle, VMS.TPS.Common.Model.Types.VVector isocenter);
        bool AddImagingSetup(VMS.TPS.Common.Model.Types.ExternalBeamMachineParameters machineParameters, VMS.TPS.Common.Model.Types.ImagingBeamSetupParameters setupParameters, VMS.TPS.Common.Model.API.Structure targetStructure);
        IBeam AddMLCArcBeam(VMS.TPS.Common.Model.Types.ExternalBeamMachineParameters machineParameters, float[,] leafPositions, VMS.TPS.Common.Model.Types.VRect<double> jawPositions, double collimatorAngle, double gantryAngle, double gantryStop, VMS.TPS.Common.Model.Types.GantryDirection gantryDirection, double patientSupportAngle, VMS.TPS.Common.Model.Types.VVector isocenter);
        IBeam AddMLCBeam(VMS.TPS.Common.Model.Types.ExternalBeamMachineParameters machineParameters, float[,] leafPositions, VMS.TPS.Common.Model.Types.VRect<double> jawPositions, double collimatorAngle, double gantryAngle, double patientSupportAngle, VMS.TPS.Common.Model.Types.VVector isocenter);
        IBeam AddMLCSetupBeam(VMS.TPS.Common.Model.Types.ExternalBeamMachineParameters machineParameters, float[,] leafPositions, VMS.TPS.Common.Model.Types.VRect<double> jawPositions, double collimatorAngle, double gantryAngle, double patientSupportAngle, VMS.TPS.Common.Model.Types.VVector isocenter);
        IBeam AddMultipleStaticSegmentBeam(VMS.TPS.Common.Model.Types.ExternalBeamMachineParameters machineParameters, System.Collections.Generic.IEnumerable<double> metersetWeights, double collimatorAngle, double gantryAngle, double patientSupportAngle, VMS.TPS.Common.Model.Types.VVector isocenter);
        IBeam AddSetupBeam(VMS.TPS.Common.Model.Types.ExternalBeamMachineParameters machineParameters, VMS.TPS.Common.Model.Types.VRect<double> jawPositions, double collimatorAngle, double gantryAngle, double patientSupportAngle, VMS.TPS.Common.Model.Types.VVector isocenter);
        IBeam AddSlidingWindowBeam(VMS.TPS.Common.Model.Types.ExternalBeamMachineParameters machineParameters, System.Collections.Generic.IEnumerable<double> metersetWeights, double collimatorAngle, double gantryAngle, double patientSupportAngle, VMS.TPS.Common.Model.Types.VVector isocenter);
        IBeam AddSlidingWindowBeamForFixedJaws(VMS.TPS.Common.Model.Types.ExternalBeamMachineParameters machineParameters, System.Collections.Generic.IEnumerable<double> metersetWeights, double collimatorAngle, double gantryAngle, double patientSupportAngle, VMS.TPS.Common.Model.Types.VVector isocenter);
        IBeam AddStaticBeam(VMS.TPS.Common.Model.Types.ExternalBeamMachineParameters machineParameters, VMS.TPS.Common.Model.Types.VRect<double> jawPositions, double collimatorAngle, double gantryAngle, double patientSupportAngle, VMS.TPS.Common.Model.Types.VVector isocenter);
        IBeam AddVMATBeam(VMS.TPS.Common.Model.Types.ExternalBeamMachineParameters machineParameters, System.Collections.Generic.IEnumerable<double> metersetWeights, double collimatorAngle, double gantryAngle, double gantryStop, VMS.TPS.Common.Model.Types.GantryDirection gantryDirection, double patientSupportAngle, VMS.TPS.Common.Model.Types.VVector isocenter);
        IBeam AddVMATBeamForFixedJaws(VMS.TPS.Common.Model.Types.ExternalBeamMachineParameters machineParameters, System.Collections.Generic.IEnumerable<double> metersetWeights, double collimatorAngle, double gantryStartAngle, double gantryStopAngle, VMS.TPS.Common.Model.Types.GantryDirection gantryDir, double patientSupportAngle, VMS.TPS.Common.Model.Types.VVector isocenter);
        IEvaluationDose CopyEvaluationDose(VMS.TPS.Common.Model.API.Dose existing);
        IEvaluationDose CreateEvaluationDose();
        void RemoveBeam(VMS.TPS.Common.Model.API.Beam beam);
        ITradeoffExplorationContext TradeoffExplorationContext { get; }
        IEvaluationDose DoseAsEvaluationDose { get; }
    }
}
