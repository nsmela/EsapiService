using System;
using System.Collections.Generic;
using System.Linq;
using VMS.TPS.Common.Model.Types;

namespace VMS.TPS.Common.Model.API
{
    public class Block : ApiDataObject
    {
        public Block()
        {
        }

        public AddOnMaterial AddOnMaterial { get; set; }
        public bool IsDiverging { get; set; }
        public System.Windows.Point[][] Outline { get; set; }
        public double TransmissionFactor { get; set; }
        public Tray Tray { get; set; }
        public double TrayTransmissionFactor { get; set; }
        public BlockType Type { get; set; }
    }
}
