using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VMS.TPS.Common.Model.API;
using VMS.TPS.Common.Model.Types;
using Esapi.Interfaces;
using Esapi.Services;

namespace Esapi.Wrappers
{
    public class AsyncIsodose : AsyncSerializableObject, IIsodose, IEsapiWrapper<VMS.TPS.Common.Model.API.Isodose>
    {
        internal new readonly VMS.TPS.Common.Model.API.Isodose _inner;

        // Store the inner ESAPI object reference
        // internal so other wrappers can access it
        // new to override any inherited _inner fields
        internal new readonly IEsapiService _service;

public AsyncIsodose(VMS.TPS.Common.Model.API.Isodose inner, IEsapiService service) : base(inner, service)
        {
            if (inner is null) throw new ArgumentNullException(nameof(inner));
            if (service is null) throw new ArgumentNullException(nameof(service));

            _inner = inner;
            _service = service;

            Color = inner.Color;
            Level = inner.Level;
        }

        public System.Windows.Media.Color Color { get; }

        public DoseValue Level { get; }

        public async Task<System.Windows.Media.Media3D.MeshGeometry3D> GetMeshGeometryAsync()
        {
            return await _service.PostAsync(context => {
                var innerResult = _inner.MeshGeometry;
                if (innerResult != null && innerResult.CanFreeze) { innerResult.Freeze(); }
                return innerResult;
            });
        }

        public Task RunAsync(Action<VMS.TPS.Common.Model.API.Isodose> action) => _service.PostAsync((context) => action(_inner));
        public Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.Isodose, T> func) => _service.PostAsync<T>((context) => func(_inner));

        public static implicit operator VMS.TPS.Common.Model.API.Isodose(AsyncIsodose wrapper) => wrapper._inner;

        // Internal Explicit Implementation to expose _inner safely for covariance
        VMS.TPS.Common.Model.API.Isodose IEsapiWrapper<VMS.TPS.Common.Model.API.Isodose>.Inner => _inner;

        // Explicit or Implicit implementation of Service
        // Since _service is private, we expose it via the interface
        IEsapiService IEsapiWrapper<VMS.TPS.Common.Model.API.Isodose>.Service => _service;
    }
}
