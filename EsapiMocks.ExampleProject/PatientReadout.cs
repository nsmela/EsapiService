using System.Text;
using VMS.TPS.Common.Model.API;

namespace EsapiMocks.ExampleProject
{
    public static class PatientReadout
    {
        public static string GetPatientSummary(Patient patient)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Patient Summary:");
            sb.AppendLine($"Name: {patient.FirstName} {patient.LastName}");
            sb.AppendLine($"ID: {patient.Id}");
            sb.AppendLine($"DOB: {patient.DateOfBirth.ToString()}");
            return sb.ToString();
        }
    }
}
