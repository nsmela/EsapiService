using Esapi.Interfaces;
using Esapi.Services;
using Esapi.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VMS.TPS.Common.Model.API;

namespace Esapi.Interfaces
{
    public partial interface IPatient : IApiDataObject
    {
        Task<IReadOnlyList<IPlanSetup>> GetPlansAsync();
        Task<IPlanSetup> GetPlanByIdAsync(string id);
    }
}

namespace Esapi.Wrappers
{
    public partial class AsyncPatient : AsyncApiDataObject, IPatient, IEsapiWrapper<Patient>
    {
        public async Task<IReadOnlyList<IPlanSetup>> GetPlansAsync() => await RunAsync((Patient context) =>
        {
            return context
                .Courses
                .SelectMany(c => c.PlanSetups)
                .Select(plan => new AsyncPlanSetup(plan, _service))
                .ToList();
        });

        public async Task<IPlanSetup> GetPlanByIdAsync(string id) => await RunAsync((Patient context) =>
        {
            return context
                .Courses
                .SelectMany(c => c.PlanSetups)
                .Where(p => p.Id == id)
                .Select(plan => new AsyncPlanSetup(plan, _service))
                .First();
        });

    }
}
