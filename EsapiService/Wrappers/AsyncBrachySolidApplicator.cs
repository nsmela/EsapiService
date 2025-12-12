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
            _inner = inner;
            _service = service;

            ApplicatorSetName = inner.ApplicatorSetName;
            ApplicatorSetType = inner.ApplicatorSetType;
            Category = inner.Category;
            Catheters = inner.Catheters;
            GroupNumber = inner.GroupNumber;
            Note = inner.Note;
            PartName = inner.PartName;
            PartNumber = inner.PartNumber;
            Summary = inner.Summary;
            UID = inner.UID;
            Vendor = inner.Vendor;
            Version = inner.Version;
        }

        public string ApplicatorSetName { get; }

        public string ApplicatorSetType { get; }

        public string Category { get; }

        public IEnumerable<Catheter> Catheters { get; }

        public int GroupNumber { get; }

        public string Note { get; }

        public string PartName { get; }

        public string PartNumber { get; }

        public string Summary { get; }

        public string UID { get; }

        public string Vendor { get; }

        public string Version { get; }

        public Task RunAsync(Action<VMS.TPS.Common.Model.API.BrachySolidApplicator> action) => _service.PostAsync((context) => action(_inner));
        public Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.BrachySolidApplicator, T> func) => _service.PostAsync<T>((context) => func(_inner));

        public static implicit operator VMS.TPS.Common.Model.API.BrachySolidApplicator(AsyncBrachySolidApplicator wrapper) => wrapper;

        // Internal Explicit Implementation to expose _inner safely for covariance
        VMS.TPS.Common.Model.API.BrachySolidApplicator IEsapiWrapper<VMS.TPS.Common.Model.API.BrachySolidApplicator>.Inner => _inner;
    }
}
