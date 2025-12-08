using System.Threading.Tasks;
using VMS.TPS.Common.Model.API;
using VMS.TPS.Common.Model.Types;

namespace EsapiService.Wrappers
{
    public class AsyncUser : IUser
    {
        internal readonly VMS.TPS.Common.Model.API.User _inner;

        // Store the inner ESAPI object reference
        // internal so other wrappers can access it
        // new to override any inherited _inner fields
        internal new readonly IEsapiService _service;

        public AsyncUser(VMS.TPS.Common.Model.API.User inner, IEsapiService service) : base(inner, service)
        {
            _inner = inner;
            _service = service;

            Id = inner.Id;
            IsServiceUser = inner.IsServiceUser;
            Language = inner.Language;
            Name = inner.Name;
        }

        public string Id { get; }
        public bool IsServiceUser { get; }
        public string Language { get; }
        public string Name { get; }

        public Task RunAsync(Action<VMS.TPS.Common.Model.API.User> action) => _service.RunAsync(() => action(_inner));
        public Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.User, T> func) => _service.RunAsync(() => func(_inner));
    }
}
    }
}
