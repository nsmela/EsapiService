using Esapi.Interfaces;
using Esapi.Wrappers;
using System;
using System.Linq;
using System.Threading.Tasks;

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
        public static async Task<IStructure> RecreateStructure(this IStructureSet ss, string structureId, string dicomType)
        {
            var existing = (await ss.GetStructuresAsync()).FirstOrDefault(s => s.Id.Equals(structureId, StringComparison.OrdinalIgnoreCase));
            if (existing != null) await ss.RemoveStructureAsync(existing);
            var result = await ss.AddStructureAsync(dicomType, structureId);
            return result;
        }

        public static async Task<IStructure> GetStructureById(this IStructureSet ss, string structureId) =>
            (await ss.GetStructuresAsync()).FirstOrDefault(s => s.Id.Equals(structureId, StringComparison.OrdinalIgnoreCase));

    }
}
