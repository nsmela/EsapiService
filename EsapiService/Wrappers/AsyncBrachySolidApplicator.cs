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
    public partial class AsyncBrachySolidApplicator : AsyncApiDataObject, IBrachySolidApplicator, IEsapiWrapper<VMS.TPS.Common.Model.API.BrachySolidApplicator>
    {
        internal new readonly VMS.TPS.Common.Model.API.BrachySolidApplicator _inner;

        // Store the inner ESAPI object reference
        // internal so other wrappers can access it
        // new to override any inherited _inner fields
        internal new readonly IEsapiService _service;

        public AsyncBrachySolidApplicator(VMS.TPS.Common.Model.API.BrachySolidApplicator inner, IEsapiService service) : base(inner, service)
        {
            if (inner is null) throw new ArgumentNullException(nameof(inner));
            if (service is null) throw new ArgumentNullException(nameof(service));

            _inner = inner;
            _service = service;
        }


        public string ApplicatorSetName =>
            _inner.ApplicatorSetName;


        public string ApplicatorSetType =>
            _inner.ApplicatorSetType;


        public string Category =>
            _inner.Category;


        public async Task<IReadOnlyList<ICatheter>> GetCathetersAsync()
        {
            return await _service.PostAsync(context => 
                _inner.Catheters?.Select(x => new AsyncCatheter(x, _service)).ToList());
        }


        public int GroupNumber =>
            _inner.GroupNumber;


        public string Note =>
            _inner.Note;


        public string PartName =>
            _inner.PartName;


        public string PartNumber =>
            _inner.PartNumber;


        public string Summary =>
            _inner.Summary;


        public string UID =>
            _inner.UID;


        public string Vendor =>
            _inner.Vendor;


        public string Version =>
            _inner.Version;


        public Task RunAsync(Action<VMS.TPS.Common.Model.API.BrachySolidApplicator> action) => _service.PostAsync((context) => action(_inner));
        public Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.BrachySolidApplicator, T> func) => _service.PostAsync<T>((context) => func(_inner));

        // --- Validates --- //
        /// <summary>
        /// Verifies is the wrapped ESAPI object isn't null.
        /// </summary>
        public new bool IsValid() => !IsNotValid();

        /// <summary>
        /// Verifies is the wrapped ESAPI object is null.
        /// </summary>
        public new bool IsNotValid() => _inner is null;

        public static implicit operator VMS.TPS.Common.Model.API.BrachySolidApplicator(AsyncBrachySolidApplicator wrapper) => wrapper._inner;

        // Internal Explicit Implementation to expose _inner safely for covariance
        VMS.TPS.Common.Model.API.BrachySolidApplicator IEsapiWrapper<VMS.TPS.Common.Model.API.BrachySolidApplicator>.Inner => _inner;

        // Explicit or Implicit implementation of Service
        // Since _service is private, we expose it via the interface
        IEsapiService IEsapiWrapper<VMS.TPS.Common.Model.API.BrachySolidApplicator>.Service => _service;
    }
}
