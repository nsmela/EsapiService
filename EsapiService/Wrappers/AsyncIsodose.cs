namespace EsapiService.Wrappers
{
    public class AsyncIsodose : IIsodose
    {
        internal readonly VMS.TPS.Common.Model.API.Isodose _inner;

        // Store the inner ESAPI object reference
        // internal so other wrappers can access it
        // new to override any inherited _inner fields
        internal new readonly IEsapiService _service;

        public AsyncIsodose(VMS.TPS.Common.Model.API.Isodose inner, IEsapiService service) : base(inner, service)
        {
            _inner = inner;
            _service = service;

            Color = inner.Color;
            Level = inner.Level;
            MeshGeometry = inner.MeshGeometry;
        }

        public Windows.Media.Color Color { get; }
        public DoseValue Level { get; }
        public Windows.Media.Media3D.MeshGeometry3D MeshGeometry { get; }

        public Task RunAsync(Action<VMS.TPS.Common.Model.API.Isodose> action) => _service.RunAsync(() => action(_inner));
        public Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.Isodose, T> func) => _service.RunAsync(() => func(_inner));
    }
}
    }
}
