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
    public class AsyncBrachySolidApplicator : AsyncApiDataObject, IBrachySolidApplicator, IEsapiWrapper<VMS.TPS.Common.Model.API.BrachySolidApplicator>
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

            ApplicatorSetName = inner.ApplicatorSetName;
            ApplicatorSetType = inner.ApplicatorSetType;
            Category = inner.Category;
            GroupNumber = inner.GroupNumber;
            Note = inner.Note;
            PartName = inner.PartName;
            PartNumber = inner.PartNumber;
            Summary = inner.Summary;
            UID = inner.UID;
            Vendor = inner.Vendor;
            Version = inner.Version;
        }


        public string ApplicatorSetName { get; private set; }


        public string ApplicatorSetType { get; private set; }


        public string Category { get; private set; }


        public async Task<IReadOnlyList<ICatheter>> GetCathetersAsync()
        {
            return await _service.PostAsync(context => 
                _inner.Catheters?.Select(x => new AsyncCatheter(x, _service)).ToList());
        }


        public int GroupNumber { get; private set; }


        public string Note { get; private set; }


        public string PartName { get; private set; }


        public string PartNumber { get; private set; }


        public string Summary { get; private set; }


        public string UID { get; private set; }


        public string Vendor { get; private set; }


        public string Version { get; private set; }


        public Task RunAsync(Action<VMS.TPS.Common.Model.API.BrachySolidApplicator> action) => _service.PostAsync((context) => action(_inner));
        public Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.BrachySolidApplicator, T> func) => _service.PostAsync<T>((context) => func(_inner));

        // updates simple properties that might have changed
        public new void Refresh()
        {
            base.Refresh();

            ApplicatorSetName = _inner.ApplicatorSetName;
            ApplicatorSetType = _inner.ApplicatorSetType;
            Category = _inner.Category;
            GroupNumber = _inner.GroupNumber;
            Note = _inner.Note;
            PartName = _inner.PartName;
            PartNumber = _inner.PartNumber;
            Summary = _inner.Summary;
            UID = _inner.UID;
            Vendor = _inner.Vendor;
            Version = _inner.Version;
        }

        public static implicit operator VMS.TPS.Common.Model.API.BrachySolidApplicator(AsyncBrachySolidApplicator wrapper) => wrapper._inner;

        // Internal Explicit Implementation to expose _inner safely for covariance
        VMS.TPS.Common.Model.API.BrachySolidApplicator IEsapiWrapper<VMS.TPS.Common.Model.API.BrachySolidApplicator>.Inner => _inner;

        // Explicit or Implicit implementation of Service
        // Since _service is private, we expose it via the interface
        IEsapiService IEsapiWrapper<VMS.TPS.Common.Model.API.BrachySolidApplicator>.Service => _service;
    }
}
