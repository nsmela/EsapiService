using System.Threading.Tasks;
using VMS.TPS.Common.Model.API;
using VMS.TPS.Common.Model.Types;

namespace EsapiService.Wrappers
{
    public class AsyncApiDataObject : IApiDataObject
    {
        internal readonly VMS.TPS.Common.Model.API.ApiDataObject _inner;

        // Store the inner ESAPI object reference
        // internal so other wrappers can access it
        // new to override any inherited _inner fields
        internal new readonly IEsapiService _service;

        public AsyncApiDataObject(VMS.TPS.Common.Model.API.ApiDataObject inner, IEsapiService service) : base(inner, service)
        {
            _inner = inner;
            _service = service;

            Id = inner.Id;
            Name = inner.Name;
            Comment = inner.Comment;
            HistoryUserName = inner.HistoryUserName;
            HistoryUserDisplayName = inner.HistoryUserDisplayName;
            HistoryDateTime = inner.HistoryDateTime;
        }

        public string Id { get; }
        public string Name { get; }
        public string Comment { get; }
        public string HistoryUserName { get; }
        public string HistoryUserDisplayName { get; }
        public DateTime HistoryDateTime { get; }

        public Task RunAsync(Action<VMS.TPS.Common.Model.API.ApiDataObject> action) => _service.RunAsync(() => action(_inner));
        public Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.ApiDataObject, T> func) => _service.RunAsync(() => func(_inner));
    }
}
    }
}
