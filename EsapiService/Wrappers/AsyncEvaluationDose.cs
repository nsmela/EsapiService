using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;
using VMS.TPS.Common.Model.API;
using VMS.TPS.Common.Model.Types;
using Esapi.Interfaces;
using Esapi.Services;

namespace Esapi.Wrappers
{
    public class AsyncEvaluationDose : AsyncDose, IEvaluationDose
    {
        internal readonly VMS.TPS.Common.Model.API.EvaluationDose _inner;

        // Store the inner ESAPI object reference
        // internal so other wrappers can access it
        // new to override any inherited _inner fields
        internal new readonly IEsapiService _service;

        public AsyncEvaluationDose(VMS.TPS.Common.Model.API.EvaluationDose inner, IEsapiService service) : base(inner, service)
        {
            _inner = inner;
            _service = service;

        }


        public Task<int> DoseValueToVoxelAsync(DoseValue doseValue) => _service.RunAsync(() => _inner.DoseValueToVoxel(doseValue));

        public Task SetVoxelsAsync(int planeIndex, int[,] values) => _service.RunAsync(() => _inner.SetVoxels(planeIndex, values));

        public Task RunAsync(Action<VMS.TPS.Common.Model.API.EvaluationDose> action) => _service.RunAsync(() => action(_inner));
        public Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.EvaluationDose, T> func) => _service.RunAsync(() => func(_inner));
    }
}
