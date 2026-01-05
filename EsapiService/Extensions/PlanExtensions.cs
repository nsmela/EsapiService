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
    public partial interface IPlanSetup : IPlanningItem
    {
        Task<IImage> GetImageAsync();
        Task<IStructure> GetStructureByIdAsync(string structureId);
    }
}

namespace Esapi.Wrappers
{
    public partial class AsyncPlanSetup : AsyncPlanningItem, IPlanSetup, IEsapiWrapper<PlanSetup>
    {
        public async Task<IImage> GetImageAsync() => await RunAsync(context =>
            {
                var image = context.StructureSet?.Image;

                return image is null
                ? null
                : new AsyncImage(image, _service);
            });

        public async Task<IStructure> GetStructureByIdAsync(string structureId) => await RunAsync(context =>
        {
            var structure = context.StructureSet?.Structures.FirstOrDefault(s => s.Id == structureId);

            return structure is null
            ? null
            : new AsyncStructure(structure, _service);
        });
    }
}
