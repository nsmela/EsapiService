using Esapi.Interfaces;
using Esapi.Services;
using Esapi.Wrappers;
using System;
using System.Threading.Tasks;
using VMS.TPS.Common.Model.API;

namespace Esapi.Interfaces
{
    public partial interface IStructure : IApiDataObject
    {
        Task<IStructureSet> GetStructureSet();
        Task<IPlanSetup> GetPlan();
    }
}

namespace Esapi.Wrappers { 
    public partial class AsyncStructure : AsyncApiDataObject, IStructure, IEsapiWrapper<VMS.TPS.Common.Model.API.Structure>
    {
        public async Task<IStructureSet> GetStructureSet()
        {
            if (_inner is null) throw new ArgumentNullException(nameof(_inner));
            if (_service is null) throw new ArgumentNullException(nameof(_service));

            var plan = await _service.GetPlanAsync();
            if (plan is null) throw new NullReferenceException($"The plan for {this.Id} is null! How did we end up here?");
        
            return await plan.GetStructureSetAsync();
        }
        
        public async Task<IPlanSetup> GetPlan()
        {
            if (_inner is null) throw new ArgumentNullException(nameof(structure));
        
            var service = (structure as IEsapiWrapper<Structure>).Service;
            if (service is null) throw new ArgumentNullException(nameof(service));
        
            return await service.GetPlanAsync();
        }

        public async Task<IPlanSetup> GetPlan()
        {
            throw new NotImplementedException();
        }
    }
}
