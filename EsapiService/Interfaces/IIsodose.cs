using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Media;
using VMS.TPS.Common.Model.API;
using VMS.TPS.Common.Model.Types;
using Esapi.Services;

namespace Esapi.Interfaces
{
    public interface IIsodose : ISerializableObject
    {
        Task WriteXmlAsync(System.Xml.XmlWriter writer);
        System.Windows.Media.Color Color { get; }
        VMS.TPS.Common.Model.Types.DoseValue Level { get; }
        System.Windows.Media.Media3D.MeshGeometry3D MeshGeometry { get; }

        /// <summary>
        /// Runs a function against the raw ESAPI VMS.TPS.Common.Model.API.Isodose object safely on the ESAPI thread.
        /// </summary>
        Task RunAsync(Action<VMS.TPS.Common.Model.API.Isodose> action);

        /// <summary>
        /// Runs a function against the raw ESAPI VMS.TPS.Common.Model.API.Isodose object safely on the ESAPI thread.
        /// </summary>
        Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.Isodose, T> func);
    }
}
