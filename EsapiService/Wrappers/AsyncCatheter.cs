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
            if (inner == null) throw new ArgumentNullException(nameof(inner));
            if (service == null) throw new ArgumentNullException(nameof(service));

            _inner = inner;
            _service = service;

            ApplicatorLength = inner.ApplicatorLength;
            BrachySolidApplicatorPartID = inner.BrachySolidApplicatorPartID;
            ChannelNumber = inner.ChannelNumber;
            Color = inner.Color;
            DeadSpaceLength = inner.DeadSpaceLength;
            FirstSourcePosition = inner.FirstSourcePosition;
            GroupNumber = inner.GroupNumber;
            LastSourcePosition = inner.LastSourcePosition;
            Shape = inner.Shape;
            StepSize = inner.StepSize;
        }

        // Simple Method
        public Task<double> GetSourcePosCenterDistanceFromTipAsync(ISourcePosition sourcePosition) => 
            _service.PostAsync(context => _inner.GetSourcePosCenterDistanceFromTip(((AsyncSourcePosition)sourcePosition)._inner));

        // Simple Method
        public Task<double> GetTotalDwellTimeAsync() => 
            _service.PostAsync(context => _inner.GetTotalDwellTime());

        // Simple Void Method
        public Task LinkRefLineAsync(IStructure refLine) =>
            _service.PostAsync(context => _inner.LinkRefLine(((AsyncStructure)refLine)._inner));

        // Simple Void Method
        public Task LinkRefPointAsync(IReferencePoint refPoint) =>
            _service.PostAsync(context => _inner.LinkRefPoint(((AsyncReferencePoint)refPoint)._inner));

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
        public Task UnlinkRefLineAsync(IStructure refLine) =>
            _service.PostAsync(context => _inner.UnlinkRefLine(((AsyncStructure)refLine)._inner));

        // Simple Void Method
        public Task UnlinkRefPointAsync(IReferencePoint refPoint) =>
            _service.PostAsync(context => _inner.UnlinkRefPoint(((AsyncReferencePoint)refPoint)._inner));

        public double ApplicatorLength { get; private set; }
        public async Task SetApplicatorLengthAsync(double value)
        {
            ApplicatorLength = await _service.PostAsync(context => 
            {
                _inner.ApplicatorLength = value;
                return _inner.ApplicatorLength;
            });
        }

        public async Task<IReadOnlyList<IBrachyFieldReferencePoint>> GetBrachyFieldReferencePointsAsync()
        {
            return await _service.PostAsync(context => 
                _inner.BrachyFieldReferencePoints?.Select(x => new AsyncBrachyFieldReferencePoint(x, _service)).ToList());
        }


        public int BrachySolidApplicatorPartID { get; }

        public int ChannelNumber { get; private set; }
        public async Task SetChannelNumberAsync(int value)
        {
            ChannelNumber = await _service.PostAsync(context => 
            {
                _inner.ChannelNumber = value;
                return _inner.ChannelNumber;
            });
        }

        public System.Windows.Media.Color Color { get; }

        public double DeadSpaceLength { get; private set; }
        public async Task SetDeadSpaceLengthAsync(double value)
        {
            DeadSpaceLength = await _service.PostAsync(context => 
            {
                _inner.DeadSpaceLength = value;
                return _inner.DeadSpaceLength;
            });
        }

        public double FirstSourcePosition { get; }

        public int GroupNumber { get; }

        public double LastSourcePosition { get; }

        public VVector[] Shape { get; private set; }
        public async Task SetShapeAsync(VVector[] value)
        {
            Shape = await _service.PostAsync(context => 
            {
                _inner.Shape = value;
                return _inner.Shape;
            });
        }

        public async Task<IReadOnlyList<ISourcePosition>> GetSourcePositionsAsync()
        {
            return await _service.PostAsync(context => 
                _inner.SourcePositions?.Select(x => new AsyncSourcePosition(x, _service)).ToList());
        }


        public double StepSize { get; }

        public async Task<IBrachyTreatmentUnit> GetTreatmentUnitAsync()
        {
            return await _service.PostAsync(context => 
                _inner.TreatmentUnit is null ? null : new AsyncBrachyTreatmentUnit(_inner.TreatmentUnit, _service));
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
