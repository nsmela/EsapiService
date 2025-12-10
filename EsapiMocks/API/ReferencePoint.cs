using System;
using System.Collections.Generic;
using System.Linq;
using VMS.TPS.Common.Model.Types;

namespace VMS.TPS.Common.Model.API
{
    public class ReferencePoint : ApiDataObject
    {
        public ReferencePoint()
        {
        }

        public bool AddLocation(Image Image, double x, double y, double z, System.Text.StringBuilder errorHint) => default;
        public bool ChangeLocation(Image Image, double x, double y, double z, System.Text.StringBuilder errorHint) => default;
        public bool HasLocation(PlanSetup planSetup) => default;
        public bool RemoveLocation(Image Image, System.Text.StringBuilder errorHint) => default;
    }
}
