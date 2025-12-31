using Esapi.Interfaces;
using Esapi.Services;
using Esapi.Wrappers;
using System;
using System.Linq;
using System.Threading.Tasks;
using VMS.TPS.Common.Model.API;

namespace Esapi.Extensions
{
    public static class StructureSetExtensions
    {
        /// <summary>
        /// Removes any existing structure with the given id and creates a new one with the given DICOM type. Returns a wrapper around the new structure.
        /// </summary>
        /// <param name="ss"></param>
        /// <param name="structureId">Structure Id</param>
        /// <param name="dicomType">Dicom Type</param>
        /// <returns>AsyncStructure wrapping the new Structure</returns>
        public static async Task<IStructure> RecreateStructure(this IStructureSet ss, string structureId, string dicomType) => await ss.RunAsync(context =>
            {
                var existing = context.Structures.FirstOrDefault(s => s.Id == structureId);
                if (existing != null) context.RemoveStructure(existing);

                var newStructure = context.AddStructure(dicomType, structureId);

                var service = (ss as IEsapiWrapper<StructureSet>).Service;

                return new AsyncStructure(newStructure, service);
            });

        public static async Task<IStructure> GetStructureById(this IStructureSet ss, string structureId) => await ss.RunAsync(context =>
            {
                var existing = context.Structures.FirstOrDefault(s => s.Id == structureId);
                var service = (ss as IEsapiWrapper<StructureSet>).Service;

                return existing is null
                ? null
                : new AsyncStructure(existing, service);
            });
           
    }
}
