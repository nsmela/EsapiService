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
    public partial class AsyncCatheter : AsyncApiDataObject, ICatheter, IEsapiWrapper<VMS.TPS.Common.Model.API.Catheter>
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
        }


        // Simple Method
        public Task<double> GetSourcePosCenterDistanceFromTipAsync(ISourcePosition sourcePosition) => 
            _service.PostAsync(context => _inner.GetSourcePosCenterDistanceFromTip(((AsyncSourcePosition)sourcePosition)._inner));

        // Simple Method
        public Task<double> GetTotalDwellTimeAsync() => 
            _service.PostAsync(context => _inner.GetTotalDwellTime());

        // Simple Void Method
        public Task LinkRefLineAsync(IStructure refLine) 
        {
            _service.PostAsync(context => _inner.LinkRefLine(((AsyncStructure)refLine)._inner));
            return Task.CompletedTask;
        }

        // Simple Void Method
        public Task LinkRefPointAsync(IReferencePoint refPoint) 
        {
            _service.PostAsync(context => _inner.LinkRefPoint(((AsyncReferencePoint)refPoint)._inner));
            return Task.CompletedTask;
        }

        public async Task<(bool result, string message)> SetIdAsync(string id)
        {
            var postResult = await _service.PostAsync(context => {
                string message_temp = default(string);
                var result = _inner.SetId(id, out message_temp);
                return (result, message_temp);
            });
            return (postResult.Item1,
                    postResult.Item2);
        }


        // Simple Method
        public Task<SetSourcePositionsResult> SetSourcePositionsAsync(double stepSize, double firstSourcePosition, double lastSourcePosition) => 
            _service.PostAsync(context => _inner.SetSourcePositions(stepSize, firstSourcePosition, lastSourcePosition));

        // Simple Void Method
        public Task UnlinkRefLineAsync(IStructure refLine) 
        {
            _service.PostAsync(context => _inner.UnlinkRefLine(((AsyncStructure)refLine)._inner));
            return Task.CompletedTask;
        }

        // Simple Void Method
        public Task UnlinkRefPointAsync(IReferencePoint refPoint) 
        {
            _service.PostAsync(context => _inner.UnlinkRefPoint(((AsyncReferencePoint)refPoint)._inner));
            return Task.CompletedTask;
        }

        public double ApplicatorLength
        {
            get => _inner.ApplicatorLength;
            set => _inner.ApplicatorLength = value;
        }


        public async Task<IReadOnlyList<IBrachyFieldReferencePoint>> GetBrachyFieldReferencePointsAsync()
        {
            return await _service.PostAsync(context => 
                _inner.BrachyFieldReferencePoints?.Select(x => new AsyncBrachyFieldReferencePoint(x, _service)).ToList());
        }


        public int BrachySolidApplicatorPartID =>
            _inner.BrachySolidApplicatorPartID;


        public int ChannelNumber
        {
            get => _inner.ChannelNumber;
            set => _inner.ChannelNumber = value;
        }


        public System.Windows.Media.Color Color =>
            _inner.Color;


        public double DeadSpaceLength
        {
            get => _inner.DeadSpaceLength;
            set => _inner.DeadSpaceLength = value;
        }


        public double FirstSourcePosition =>
            _inner.FirstSourcePosition;


        public int GroupNumber =>
            _inner.GroupNumber;


        public double LastSourcePosition =>
            _inner.LastSourcePosition;


        public VVector[] Shape
        {
            get => _inner.Shape;
            set => _inner.Shape = value;
        }


        public async Task<IReadOnlyList<ISourcePosition>> GetSourcePositionsAsync()
        {
            return await _service.PostAsync(context => 
                _inner.SourcePositions?.Select(x => new AsyncSourcePosition(x, _service)).ToList());
        }


        public double StepSize =>
            _inner.StepSize;


        public async Task<IBrachyTreatmentUnit> GetTreatmentUnitAsync()
        {
            return await _service.PostAsync(context => {
                var innerResult = _inner.TreatmentUnit is null ? null : new AsyncBrachyTreatmentUnit(_inner.TreatmentUnit, _service);
                return innerResult;
            });
        }

        public Task RunAsync(Action<VMS.TPS.Common.Model.API.Catheter> action) => _service.PostAsync((context) => action(_inner));
        public Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.Catheter, T> func) => _service.PostAsync<T>((context) => func(_inner));

        // --- Validates --- //
        /// <summary>
        /// Verifies is the wrapped ESAPI object isn't null.
        /// </summary>
        public new bool IsValid() => !IsNotValid();

        /// <summary>
        /// Verifies is the wrapped ESAPI object is null.
        /// </summary>
        public new bool IsNotValid() => _inner is null;

        public static implicit operator VMS.TPS.Common.Model.API.Catheter(AsyncCatheter wrapper) => wrapper._inner;

        // Internal Explicit Implementation to expose _inner safely for covariance
        VMS.TPS.Common.Model.API.Catheter IEsapiWrapper<VMS.TPS.Common.Model.API.Catheter>.Inner => _inner;

        // Explicit or Implicit implementation of Service
        // Since _service is private, we expose it via the interface
        IEsapiService IEsapiWrapper<VMS.TPS.Common.Model.API.Catheter>.Service => _service;
    }
}
