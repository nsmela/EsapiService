using System.Threading.Tasks;
using VMS.TPS.Common.Model.API;
using VMS.TPS.Common.Model.Types;

namespace EsapiService.Wrappers
{
    public class AsyncApplicationScript : IApplicationScript
    {
        internal readonly VMS.TPS.Common.Model.API.ApplicationScript _inner;

        // Store the inner ESAPI object reference
        // internal so other wrappers can access it
        // new to override any inherited _inner fields
        internal new readonly IEsapiService _service;

        public AsyncApplicationScript(VMS.TPS.Common.Model.API.ApplicationScript inner, IEsapiService service) : base(inner, service)
        {
            _inner = inner;
            _service = service;

            ApprovalStatus = inner.ApprovalStatus;
            ApprovalStatusDisplayText = inner.ApprovalStatusDisplayText;
            AssemblyName = inner.AssemblyName;
            IsReadOnlyScript = inner.IsReadOnlyScript;
            IsWriteableScript = inner.IsWriteableScript;
            PublisherName = inner.PublisherName;
            ScriptType = inner.ScriptType;
            StatusUserIdentity = inner.StatusUserIdentity;
        }

        public ApplicationScriptApprovalStatus ApprovalStatus { get; }
        public string ApprovalStatusDisplayText { get; }
        public Reflection.AssemblyName AssemblyName { get; }
        public async Task<IReadOnlyList<DateTime>> GetExpirationDateAsync()
        {
            return await _service.RunAsync(() => _inner.ExpirationDate?.ToList());
        }

        public bool IsReadOnlyScript { get; }
        public bool IsWriteableScript { get; }
        public string PublisherName { get; }
        public ApplicationScriptType ScriptType { get; }
        public async Task<IReadOnlyList<DateTime>> GetStatusDateAsync()
        {
            return await _service.RunAsync(() => _inner.StatusDate?.ToList());
        }

        public UserIdentity StatusUserIdentity { get; }

        public Task RunAsync(Action<VMS.TPS.Common.Model.API.ApplicationScript> action) => _service.RunAsync(() => action(_inner));
        public Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.ApplicationScript, T> func) => _service.RunAsync(() => func(_inner));
    }
}
    }
}
