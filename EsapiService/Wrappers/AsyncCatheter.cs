using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;
using VMS.TPS.Common.Model.API;
using VMS.TPS.Common.Model.Types;
using Esapi.Interfaces;

namespace Esapi.Wrappers
{
    public class AsyncCatheter : ICatheter
    {
        internal readonly VMS.TPS.Common.Model.API.Catheter _inner;

        // Store the inner ESAPI object reference
        // internal so other wrappers can access it
        // new to override any inherited _inner fields
        internal new readonly IEsapiService _service;

        public AsyncCatheter(VMS.TPS.Common.Model.API.Catheter inner, IEsapiService service) : base(inner, service)
        {
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


        public Task<double> GetSourcePosCenterDistanceFromTipAsync(ISourcePosition sourcePosition) => _service.RunAsync(() => _inner.GetSourcePosCenterDistanceFromTip(sourcePosition));

        public Task<double> GetTotalDwellTimeAsync() => _service.RunAsync(() => _inner.GetTotalDwellTime());

        public Task LinkRefLineAsync(IStructure refLine) => _service.RunAsync(() => _inner.LinkRefLine(refLine));

        public Task LinkRefPointAsync(IReferencePoint refPoint) => _service.RunAsync(() => _inner.LinkRefPoint(refPoint));

        public async Task<(bool Result, string message)> SetIdAsync(string id)
        {
            string message_temp;
            var result = await _service.RunAsync(() => _inner.SetId(id, out message_temp));
            return (result, message_temp);
        }

        public Task<SetSourcePositionsResult> SetSourcePositionsAsync(double stepSize, double firstSourcePosition, double lastSourcePosition) => _service.RunAsync(() => _inner.SetSourcePositions(stepSize, firstSourcePosition, lastSourcePosition));

        public Task UnlinkRefLineAsync(IStructure refLine) => _service.RunAsync(() => _inner.UnlinkRefLine(refLine));

        public Task UnlinkRefPointAsync(IReferencePoint refPoint) => _service.RunAsync(() => _inner.UnlinkRefPoint(refPoint));

        public double ApplicatorLength { get; private set; }
        public async Task SetApplicatorLengthAsync(double value)
        {
            ApplicatorLength = await _service.RunAsync(() =>
            {
                _inner.ApplicatorLength = value;
                return _inner.ApplicatorLength;
            });
        }

        public async Task<IReadOnlyList<IBrachyFieldReferencePoint>> GetBrachyFieldReferencePointsAsync()
        {
            return await _service.RunAsync(() => 
                _inner.BrachyFieldReferencePoints?.Select(x => new AsyncBrachyFieldReferencePoint(x, _service)).ToList());
        }


        public int BrachySolidApplicatorPartID { get; }

        public int ChannelNumber { get; private set; }
        public async Task SetChannelNumberAsync(int value)
        {
            ChannelNumber = await _service.RunAsync(() =>
            {
                _inner.ChannelNumber = value;
                return _inner.ChannelNumber;
            });
        }

        public Windows.Media.Color Color { get; }

        public double DeadSpaceLength { get; private set; }
        public async Task SetDeadSpaceLengthAsync(double value)
        {
            DeadSpaceLength = await _service.RunAsync(() =>
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
            Shape = await _service.RunAsync(() =>
            {
                _inner.Shape = value;
                return _inner.Shape;
            });
        }

        public async Task<IReadOnlyList<ISourcePosition>> GetSourcePositionsAsync()
        {
            return await _service.RunAsync(() => 
                _inner.SourcePositions?.Select(x => new AsyncSourcePosition(x, _service)).ToList());
        }


        public double StepSize { get; }

        public async Task<IBrachyTreatmentUnit> GetTreatmentUnitAsync()
        {
            return await _service.RunAsync(() => 
                _inner.TreatmentUnit is null ? null : new AsyncBrachyTreatmentUnit(_inner.TreatmentUnit, _service));
        }

        public Task RunAsync(Action<VMS.TPS.Common.Model.API.Catheter> action) => _service.RunAsync(() => action(_inner));
        public Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.Catheter, T> func) => _service.RunAsync(() => func(_inner));
    }
}
