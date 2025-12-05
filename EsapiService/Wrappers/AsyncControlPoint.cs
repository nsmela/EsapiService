namespace EsapiService.Wrappers
{
    public class AsyncControlPoint : IControlPoint
    {
        internal readonly VMS.TPS.Common.Model.API.ControlPoint _inner;

        // Store the inner ESAPI object reference
        // internal so other wrappers can access it
        // new to override any inherited _inner fields
        internal new readonly IEsapiService _service;

        public AsyncControlPoint(VMS.TPS.Common.Model.API.ControlPoint inner, IEsapiService service) : base(inner, service)
        {
            _inner = inner;
            _service = service;

            CollimatorAngle = inner.CollimatorAngle;
            GantryAngle = inner.GantryAngle;
            Index = inner.Index;
            LeafPositions = inner.LeafPositions;
            MetersetWeight = inner.MetersetWeight;
            PatientSupportAngle = inner.PatientSupportAngle;
            TableTopLateralPosition = inner.TableTopLateralPosition;
            TableTopLongitudinalPosition = inner.TableTopLongitudinalPosition;
            TableTopVerticalPosition = inner.TableTopVerticalPosition;
        }

        public void WriteXml(System.Xml.XmlWriter writer) => _inner.WriteXml(writer);
        public IBeam Beam => _inner.Beam is null ? null : new AsyncBeam(_inner.Beam, _service);

        public double CollimatorAngle { get; }
        public double GantryAngle { get; }
        public int Index { get; }
        public System.Collections.Generic.IReadOnlyList<double> JawPositions => _inner.JawPositions?.ToList();
        public float[,] LeafPositions { get; }
        public double MetersetWeight { get; }
        public double PatientSupportAngle { get; }
        public double TableTopLateralPosition { get; }
        public double TableTopLongitudinalPosition { get; }
        public double TableTopVerticalPosition { get; }

        public Task RunAsync(Action<VMS.TPS.Common.Model.API.ControlPoint> action) => _service.RunAsync(() => action(_inner));
        public Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.ControlPoint, T> func) => _service.RunAsync(() => func(_inner));
    }
}
    }
}
