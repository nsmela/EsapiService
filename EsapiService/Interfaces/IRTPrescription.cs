namespace VMS.TPS.Common.Model.API
{
    public interface IRTPrescription : IApiDataObject
    {
        void WriteXml(System.Xml.XmlWriter writer);
        string BolusFrequency { get; }
        string BolusThickness { get; }
        System.Collections.Generic.IReadOnlyList<string> Energies { get; }
        System.Collections.Generic.IReadOnlyList<string> EnergyModes { get; }
        string Gating { get; }
        IRTPrescription LatestRevision { get; }
        string Notes { get; }
        System.Collections.Generic.IReadOnlyList<int> NumberOfFractions { get; }
        System.Collections.Generic.IReadOnlyList<IRTPrescriptionOrganAtRisk> OrgansAtRisk { get; }
        string PhaseType { get; }
        IRTPrescription PredecessorPrescription { get; }
        int RevisionNumber { get; }
        System.Collections.Generic.IReadOnlyList<bool> SimulationNeeded { get; }
        string Site { get; }
        string Status { get; }
        System.Collections.Generic.IReadOnlyList<IRTPrescriptionTargetConstraints> TargetConstraintsWithoutTargetLevel { get; }
        System.Collections.Generic.IReadOnlyList<IRTPrescriptionTarget> Targets { get; }
        string Technique { get; }
    }
}
