using Esapi.Interfaces;
using Esapi.Services;
using Esapi.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VMS.TPS.Common.Model.API;

namespace Esapi.Extensions
{
    public static class PatientExtensions
    {
        public static async Task<IReadOnlyList<IPlanSetup>> GetPlansAsync(this IPatient patient) => await patient.RunAsync(context =>
        {
            return context
                .Courses
                .SelectMany(c => c.PlanSetups)
                .Select(plan => new AsyncPlanSetup(plan, ((IEsapiWrapper<Patient>)patient).Service))
                .ToList();
        });

        public static async Task<IPlanSetup> GetPlanByIdAsync(this IPatient patient, string id) => await patient.RunAsync(context =>
        {
            return context
                .Courses
                .SelectMany(c => c.PlanSetups)
                .Where(pp => pp.Id == id)
                .Select(plan => new AsyncPlanSetup(plan, ((IEsapiWrapper<Patient>)patient).Service))
                .First();
        });

    }
}
