namespace VMS.TPS.Common.Model.API
{
    public interface IApplication : ISerializableObject
    {
        void Dispose();
        IPatient OpenPatient(VMS.TPS.Common.Model.API.PatientSummary patientSummary);
        IPatient OpenPatientById(string id);
        void ClosePatient();
        void SaveModifications();
        void WriteXml(System.Xml.XmlWriter writer);
        IUser CurrentUser { get; }
        string SiteProgramDataDir { get; }
        System.Collections.Generic.IReadOnlyList<IPatientSummary> PatientSummaries { get; }
        ICalculation Calculation { get; }
        IActiveStructureCodeDictionaries StructureCodes { get; }
        IEquipment Equipment { get; }
        IScriptEnvironment ScriptEnvironment { get; }
    }
}
