    using System.Threading.Tasks;
namespace EsapiService.Wrappers
{
    using System.Linq;
    using System.Collections.Generic;
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

            BrachySolidApplicatorPartID = inner.BrachySolidApplicatorPartID;
            Color = inner.Color;
            FirstSourcePosition = inner.FirstSourcePosition;
            GroupNumber = inner.GroupNumber;
            LastSourcePosition = inner.LastSourcePosition;
            StepSize = inner.StepSize;
        }

        public void WriteXml(System.Xml.XmlWriter writer) => _inner.WriteXml(writer);
        public double GetSourcePosCenterDistanceFromTip(VMS.TPS.Common.Model.API.SourcePosition sourcePosition) => _inner.GetSourcePosCenterDistanceFromTip(sourcePosition);
        public double GetTotalDwellTime() => _inner.GetTotalDwellTime();
        public void LinkRefLine(VMS.TPS.Common.Model.API.Structure refLine) => _inner.LinkRefLine(refLine);
        public void LinkRefPoint(VMS.TPS.Common.Model.API.ReferencePoint refPoint) => _inner.LinkRefPoint(refPoint);
        public async System.Threading.Tasks.Task<(bool Result, string message)> SetIdAsync(string id)
        {
            string message_temp;
            var result = await _service.RunAsync(() => _inner.SetId(id, out message_temp));
            return (result, message_temp);
        }
        public VMS.TPS.Common.Model.Types.SetSourcePositionsResult SetSourcePositions(double stepSize, double firstSourcePosition, double lastSourcePosition) => _inner.SetSourcePositions(stepSize, firstSourcePosition, lastSourcePosition);
        public void UnlinkRefLine(VMS.TPS.Common.Model.API.Structure refLine) => _inner.UnlinkRefLine(refLine);
        public void UnlinkRefPoint(VMS.TPS.Common.Model.API.ReferencePoint refPoint) => _inner.UnlinkRefPoint(refPoint);
        public double ApplicatorLength => _inner.ApplicatorLength;
        public async Task SetApplicatorLengthAsync(double value) => _service.RunAsync(() => _inner.ApplicatorLength = value);
        public System.Collections.Generic.IReadOnlyList<IBrachyFieldReferencePoint> BrachyFieldReferencePoints => _inner.BrachyFieldReferencePoints?.Select(x => new AsyncBrachyFieldReferencePoint(x, _service)).ToList();
        public int BrachySolidApplicatorPartID { get; }
        public int ChannelNumber => _inner.ChannelNumber;
        public async Task SetChannelNumberAsync(int value) => _service.RunAsync(() => _inner.ChannelNumber = value);
        public System.Windows.Media.Color Color { get; }
        public double DeadSpaceLength => _inner.DeadSpaceLength;
        public async Task SetDeadSpaceLengthAsync(double value) => _service.RunAsync(() => _inner.DeadSpaceLength = value);
        public double FirstSourcePosition { get; }
        public int GroupNumber { get; }
        public double LastSourcePosition { get; }
        public VMS.TPS.Common.Model.Types.VVector[] Shape => _inner.Shape;
        public async Task SetShapeAsync(VMS.TPS.Common.Model.Types.VVector[] value) => _service.RunAsync(() => _inner.Shape = value);
        public System.Collections.Generic.IReadOnlyList<ISourcePosition> SourcePositions => _inner.SourcePositions?.Select(x => new AsyncSourcePosition(x, _service)).ToList();
        public double StepSize { get; }
        public IBrachyTreatmentUnit TreatmentUnit => _inner.TreatmentUnit is null ? null : new AsyncBrachyTreatmentUnit(_inner.TreatmentUnit, _service);


        public Task RunAsync(Action<VMS.TPS.Common.Model.API.Catheter> action) => _service.RunAsync(() => action(_inner));
        public Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.Catheter, T> func) => _service.RunAsync(() => func(_inner));
    }
}
    }
}
