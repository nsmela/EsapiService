using Esapi.Interfaces;
using Esapi.Services;
using System;
using System.Threading.Tasks;
using VMS.TPS.Common.Model.API;

namespace Esapi.Extensions
{
    public static class StructureExtensions
    {
        public static async Task<IStructureSet> GetStructureSet(this IStructure structure)
        {
            if (structure is null) throw new ArgumentNullException(nameof(structure));

            var service = (structure as IEsapiWrapper<Structure>).Service;
            if (service is null) throw new ArgumentNullException(nameof(service));

            var plan = await service.GetPlanAsync();
            if (plan is null) throw new NullReferenceException($"The plan for {structure.Id} is null! How did we end up here?");

            return await plan.GetStructureSetAsync();
        }

        public static async Task<IPlanSetup> GetPlan(this IStructure structure)
        {
            if (structure is null) throw new ArgumentNullException(nameof(structure));

            var service = (structure as IEsapiWrapper<Structure>).Service;
            if (service is null) throw new ArgumentNullException(nameof(service));

            return await service.GetPlanAsync();
        }
    }
}
