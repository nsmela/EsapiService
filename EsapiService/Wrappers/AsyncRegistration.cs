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
    public class AsyncRegistration : AsyncApiDataObject, IRegistration, IEsapiWrapper<VMS.TPS.Common.Model.API.Registration>
    {
        internal new readonly VMS.TPS.Common.Model.API.Registration _inner;

        // Store the inner ESAPI object reference
        // internal so other wrappers can access it
        // new to override any inherited _inner fields
        internal new readonly IEsapiService _service;

public AsyncRegistration(VMS.TPS.Common.Model.API.Registration inner, IEsapiService service) : base(inner, service)
        {
            _inner = inner;
            _service = service;

            CreationDateTime = inner.CreationDateTime;
            RegisteredFOR = inner.RegisteredFOR;
            SourceFOR = inner.SourceFOR;
            StatusDateTime = inner.StatusDateTime;
            StatusUserDisplayName = inner.StatusUserDisplayName;
            StatusUserName = inner.StatusUserName;
            TransformationMatrix = inner.TransformationMatrix;
            UID = inner.UID;
        }

        public DateTime? CreationDateTime { get; }

        public string RegisteredFOR { get; }

        public string SourceFOR { get; }

        public DateTime? StatusDateTime { get; }

        public string StatusUserDisplayName { get; }

        public string StatusUserName { get; }

        public double[,] TransformationMatrix { get; }

        public string UID { get; }

        public Task RunAsync(Action<VMS.TPS.Common.Model.API.Registration> action) => _service.PostAsync((context) => action(_inner));
        public Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.Registration, T> func) => _service.PostAsync<T>((context) => func(_inner));

        public static implicit operator VMS.TPS.Common.Model.API.Registration(AsyncRegistration wrapper) => wrapper;

        // Internal Explicit Implementation to expose _inner safely for covariance
        VMS.TPS.Common.Model.API.Registration IEsapiWrapper<VMS.TPS.Common.Model.API.Registration>.Inner => _inner;
    }
}
