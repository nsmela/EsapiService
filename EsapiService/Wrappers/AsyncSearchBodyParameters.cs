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
    public partial class AsyncSearchBodyParameters : AsyncSerializableObject, ISearchBodyParameters, IEsapiWrapper<VMS.TPS.Common.Model.API.SearchBodyParameters>
    {
        internal new readonly VMS.TPS.Common.Model.API.SearchBodyParameters _inner;

        // Store the inner ESAPI object reference
        // internal so other wrappers can access it
        // new to override any inherited _inner fields
        internal new readonly IEsapiService _service;

        public AsyncSearchBodyParameters(VMS.TPS.Common.Model.API.SearchBodyParameters inner, IEsapiService service) : base(inner, service)
        {
            if (inner is null) throw new ArgumentNullException(nameof(inner));
            if (service is null) throw new ArgumentNullException(nameof(service));

            _inner = inner;
            _service = service;
        }


        // Simple Void Method
        public Task LoadDefaultsAsync() 
        {
            _service.PostAsync(context => _inner.LoadDefaults());
            return Task.CompletedTask;
        }

        public bool FillAllCavities
        {
            get => _inner.FillAllCavities;
            set => _inner.FillAllCavities = value;
        }


        public bool KeepLargestParts
        {
            get => _inner.KeepLargestParts;
            set => _inner.KeepLargestParts = value;
        }


        public int LowerHUThreshold
        {
            get => _inner.LowerHUThreshold;
            set => _inner.LowerHUThreshold = value;
        }


        public int MREdgeThresholdHigh
        {
            get => _inner.MREdgeThresholdHigh;
            set => _inner.MREdgeThresholdHigh = value;
        }


        public int MREdgeThresholdLow
        {
            get => _inner.MREdgeThresholdLow;
            set => _inner.MREdgeThresholdLow = value;
        }


        public int NumberOfLargestPartsToKeep
        {
            get => _inner.NumberOfLargestPartsToKeep;
            set => _inner.NumberOfLargestPartsToKeep = value;
        }


        public bool PreCloseOpenings
        {
            get => _inner.PreCloseOpenings;
            set => _inner.PreCloseOpenings = value;
        }


        public double PreCloseOpeningsRadius
        {
            get => _inner.PreCloseOpeningsRadius;
            set => _inner.PreCloseOpeningsRadius = value;
        }


        public bool PreDisconnect
        {
            get => _inner.PreDisconnect;
            set => _inner.PreDisconnect = value;
        }


        public double PreDisconnectRadius
        {
            get => _inner.PreDisconnectRadius;
            set => _inner.PreDisconnectRadius = value;
        }


        public bool Smoothing
        {
            get => _inner.Smoothing;
            set => _inner.Smoothing = value;
        }


        public int SmoothingLevel
        {
            get => _inner.SmoothingLevel;
            set => _inner.SmoothingLevel = value;
        }


        public Task RunAsync(Action<VMS.TPS.Common.Model.API.SearchBodyParameters> action) => _service.PostAsync((context) => action(_inner));
        public Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.SearchBodyParameters, T> func) => _service.PostAsync<T>((context) => func(_inner));

        // --- Validates --- //
        /// <summary>
        /// Verifies is the wrapped ESAPI object isn't null.
        /// </summary>
        public new bool IsValid() => !IsNotValid();

        /// <summary>
        /// Verifies is the wrapped ESAPI object is null.
        /// </summary>
        public new bool IsNotValid() => _inner is null;

        public static implicit operator VMS.TPS.Common.Model.API.SearchBodyParameters(AsyncSearchBodyParameters wrapper) => wrapper._inner;

        // Internal Explicit Implementation to expose _inner safely for covariance
        VMS.TPS.Common.Model.API.SearchBodyParameters IEsapiWrapper<VMS.TPS.Common.Model.API.SearchBodyParameters>.Inner => _inner;

        // Explicit or Implicit implementation of Service
        // Since _service is private, we expose it via the interface
        IEsapiService IEsapiWrapper<VMS.TPS.Common.Model.API.SearchBodyParameters>.Service => _service;
    }
}
