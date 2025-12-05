namespace VMS.TPS.Common.Model.API
{
    public interface IBrachyPlanSetup : IPlanSetup
    {
        void WriteXml(System.Xml.XmlWriter writer);
        ICatheter AddCatheter(string catheterId, VMS.TPS.Common.Model.API.BrachyTreatmentUnit treatmentUnit, System.Text.StringBuilder outputDiagnostics, bool appendChannelNumToId, int channelNum);
        void AddLocationToExistingReferencePoint(VMS.TPS.Common.Model.Types.VVector location, VMS.TPS.Common.Model.API.ReferencePoint referencePoint);
        IReferencePoint AddReferencePoint(bool target, string id);
        VMS.TPS.Common.Model.Types.DoseProfile CalculateAccurateTG43DoseProfile(VMS.TPS.Common.Model.Types.VVector start, VMS.TPS.Common.Model.Types.VVector stop, double[] preallocatedBuffer);

        ICalculateBrachy3DDoseResult CalculateTG43Dose();
        string ApplicationSetupType { get; }
        VMS.TPS.Common.Model.Types.BrachyTreatmentTechniqueType BrachyTreatmentTechnique { get; }
        System.Threading.Tasks.Task SetBrachyTreatmentTechniqueAsync(VMS.TPS.Common.Model.Types.BrachyTreatmentTechniqueType value);
        System.Collections.Generic.IReadOnlyList<ICatheter> Catheters { get; }
        System.Collections.Generic.IReadOnlyList<int> NumberOfPdrPulses { get; }
        System.Collections.Generic.IReadOnlyList<double> PdrPulseInterval { get; }
        System.Collections.Generic.IReadOnlyList<IStructure> ReferenceLines { get; }
        System.Collections.Generic.IReadOnlyList<ISeedCollection> SeedCollections { get; }
        System.Collections.Generic.IReadOnlyList<IBrachySolidApplicator> SolidApplicators { get; }
        string TreatmentTechnique { get; }
        System.Collections.Generic.IReadOnlyList<System.DateTime> TreatmentDateTime { get; }
    }
}
