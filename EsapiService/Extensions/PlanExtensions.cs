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
    public static class PlanExtensions
    {
        public static async Task<IImage> GetImageAsync(this IPlanSetup plan) => await plan.RunAsync(context =>
            {
                var image = context.StructureSet?.Image;

                return image is null
                ? null
                : new AsyncImage(image, (plan as IEsapiWrapper<PlanSetup>).Service);
            });

        public static async Task<IStructure> GetStructureByIdAsync(this IPlanSetup plan, string structureId) => await plan.RunAsync(context =>
        {
            var structure = context.StructureSet?.Structures.FirstOrDefault(s => s.Id == structureId);

            return structure is null
            ? null
            : new AsyncStructure(structure, (plan as IEsapiWrapper<PlanSetup>).Service);
        });
    }
}
