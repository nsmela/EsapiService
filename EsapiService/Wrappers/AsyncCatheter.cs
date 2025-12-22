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
    public class AsyncCatheter : AsyncApiDataObject, ICatheter, IEsapiWrapper<VMS.TPS.Common.Model.API.Catheter>
    {
        internal new readonly VMS.TPS.Common.Model.API.Catheter _inner;

        // Store the inner ESAPI object reference
        // internal so other wrappers can access it
        // new to override any inherited _inner fields
        internal new readonly IEsapiService _service;

        public AsyncCatheter(VMS.TPS.Common.Model.API.Catheter inner, IEsapiService service) : base(inner, service)
        {
            if (inner is null) throw new ArgumentNullException(nameof(inner));
            if (service is null) throw new ArgumentNullException(nameof(service));

            _inner = inner;
            _service = service;

            ApplicatorLength = inner.ApplicatorLength;
            BrachySolidApplicatorPartID = inner.BrachySolidApplicatorPartID;
            ChannelNumber = inner.ChannelNumber;
            Color = inner.Color;
            DeadSpaceLength = inner.DeadSpaceLength;
            Shape = inner.Shape;
            StepSize = inner.StepSize;
        }


        // Simple Method
        public Task<double> GetSourcePosCenterDistanceFromTipAsync(ISourcePosition sourcePosition) => 
            _service.PostAsync(context => _inner.GetSourcePosCenterDistanceFromTip(((AsyncSourcePosition)sourcePosition)._inner));

        // Simple Method
        public Task<double> GetTotalDwellTimeAsync() => 
            _service.PostAsync(context => _inner.GetTotalDwellTime());

        public double ApplicatorLength { get; }

        public async Task<IReadOnlyList<IBrachyFieldReferencePoint>> GetBrachyFieldReferencePointsAsync()
        {
            return await _service.PostAsync(context => 
                _inner.BrachyFieldReferencePoints?.Select(x => new AsyncBrachyFieldReferencePoint(x, _service)).ToList());
        }


        public int BrachySolidApplicatorPartID { get; }

        public int ChannelNumber { get; }

        public System.Windows.Media.Color Color { get; }

        public double DeadSpaceLength { get; }

        public VVector[] Shape { get; }

        public async Task<IReadOnlyList<ISourcePosition>> GetSourcePositionsAsync()
        {
            return await _service.PostAsync(context => 
                _inner.SourcePositions?.Select(x => new AsyncSourcePosition(x, _service)).ToList());
        }


        public double StepSize { get; }

        public async Task<IBrachyTreatmentUnit> GetTreatmentUnitAsync()
        {
            return await _service.PostAsync(context => {
                var innerResult = _inner.TreatmentUnit is null ? null : new AsyncBrachyTreatmentUnit(_inner.TreatmentUnit, _service);
                return innerResult;
            });
        }

        public Task RunAsync(Action<VMS.TPS.Common.Model.API.Catheter> action) => _service.PostAsync((context) => action(_inner));
        public Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.Catheter, T> func) => _service.PostAsync<T>((context) => func(_inner));

        public static implicit operator VMS.TPS.Common.Model.API.Catheter(AsyncCatheter wrapper) => wrapper._inner;

        // Internal Explicit Implementation to expose _inner safely for covariance
        VMS.TPS.Common.Model.API.Catheter IEsapiWrapper<VMS.TPS.Common.Model.API.Catheter>.Inner => _inner;

        // Explicit or Implicit implementation of Service
        // Since _service is private, we expose it via the interface
        IEsapiService IEsapiWrapper<VMS.TPS.Common.Model.API.Catheter>.Service => _service;
    }
}
