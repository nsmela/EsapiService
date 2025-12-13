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
            if (inner == null) throw new ArgumentNullException(nameof(inner));
            if (service == null) throw new ArgumentNullException(nameof(service));

            _inner = inner;
            _service = service;

            Color = inner.Color;
            Level = inner.Level;
            MeshGeometry = inner.MeshGeometry;
        }

        public System.Windows.Media.Color Color { get; }

        public DoseValue Level { get; }

        public System.Windows.Media.Media3D.MeshGeometry3D MeshGeometry { get; }

        public Task RunAsync(Action<VMS.TPS.Common.Model.API.Isodose> action) => _service.PostAsync((context) => action(_inner));
        public Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.Isodose, T> func) => _service.PostAsync<T>((context) => func(_inner));

        public static implicit operator VMS.TPS.Common.Model.API.Isodose(AsyncIsodose wrapper) => wrapper._inner;

        // Internal Explicit Implementation to expose _inner safely for covariance
        VMS.TPS.Common.Model.API.Isodose IEsapiWrapper<VMS.TPS.Common.Model.API.Isodose>.Inner => _inner;
    }
}
