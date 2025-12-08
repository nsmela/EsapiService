using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;
using VMS.TPS.Common.Model.API;
using VMS.TPS.Common.Model.Types;
using Esapi.Interfaces;
using Esapi.Services;

namespace Esapi.Wrappers
{
    public class AsyncBrachySolidApplicator : AsyncApiDataObject, IBrachySolidApplicator
    {
        internal readonly VMS.TPS.Common.Model.API.BrachySolidApplicator _inner;

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

        public async Task<IReadOnlyList<ICatheter>> GetCathetersAsync()
        {
            return await _service.RunAsync(() => 
                _inner.Catheters?.Select(x => new AsyncCatheter(x, _service)).ToList());
        }


        public int GroupNumber { get; }

        public string Note { get; }

        public string PartName { get; }

        public string PartNumber { get; }

        public string Summary { get; }

        public string UID { get; }

        public string Vendor { get; }

        public string Version { get; }

        public Task RunAsync(Action<VMS.TPS.Common.Model.API.BrachySolidApplicator> action) => _service.RunAsync(() => action(_inner));
        public Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.BrachySolidApplicator, T> func) => _service.RunAsync(() => func(_inner));
    }
}
