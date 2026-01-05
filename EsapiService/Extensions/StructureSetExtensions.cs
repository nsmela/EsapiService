using Esapi.Interfaces;
using Esapi.Services;
using Esapi.Wrappers;
using System;
using System.Linq;
using System.Threading.Tasks;
using VMS.TPS.Common.Model.API;

namespace Esapi.Interfaces
{
    public partial interface IStructureSet : IApiDataObject
    {
        Task<IStructure> RecreateStructureAsync(string structureId, string dicomType);
        Task<IStructure> GetStructureByIdAsync(string structureId);
    }
}

namespace Esapi.Wrappers
{
    public partial class AsyncStructureSet : AsyncApiDataObject, IStructureSet, IEsapiWrapper<StructureSet>
    {
        /// <summary>
        /// Removes any existing structure with the given id and creates a new one with the given DICOM type. Returns a wrapper around the new structure.
        /// </summary>
        /// <param name="ss"></param>
        /// <param name="structureId">Structure Id</param>
        /// <param name="dicomType">Dicom Type</param>
        /// <returns>AsyncStructure wrapping the new Structure</returns>
        public async Task<IStructure> RecreateStructureAsync(string structureId, string dicomType) => await RunAsync(context =>
            {
                var existing = context.Structures.FirstOrDefault(s => s.Id == structureId);
                if (existing != null) context.RemoveStructure(existing);

                var newStructure = context.AddStructure(dicomType, structureId);

                return newStructure is null 
                ? null
                : new AsyncStructure(newStructure, _service);
            });

        public async Task<IStructure> GetStructureByIdAsync(string structureId) => await RunAsync(context =>
            {
                var existing = context.Structures.FirstOrDefault(s => s.Id == structureId);

                return existing is null
                ? null
                : new AsyncStructure(existing, _service);
            });
           
    }
}
