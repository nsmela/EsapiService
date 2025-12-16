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
        public static async Task<IReadOnlyList<IPlanSetup>> GetPlansAsync(this IPatient patient)
        {
            var service = ((IEsapiWrapper<Patient>)patient).Service;

            Func<Patient, IReadOnlyList<IPlanSetup>> func = (p =>
                p.Courses
                    .SelectMany(c => c.PlanSetups)
                    .Select(plan => new AsyncPlanSetup(plan, service))
                    .ToList());

            return await patient.RunAsync<IReadOnlyList<IPlanSetup>>(func);
        }

        public static async Task<IPlanSetup> GetPlanByIdAsync(this IPatient patient, string id)
        {
            var service = ((IEsapiWrapper<Patient>)patient).Service;

            Func<Patient, IPlanSetup> func = (p =>
                p.Courses
                    .SelectMany(c => c.PlanSetups)
                    .Where(pp => pp.Id == id)
                    .Select(plan => new AsyncPlanSetup(plan, service))
                    .First());

            return await patient.RunAsync<IPlanSetup>(func);

        }
    }
}
