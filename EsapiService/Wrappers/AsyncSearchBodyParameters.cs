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
    public class AsyncSearchBodyParameters : AsyncSerializableObject, ISearchBodyParameters
    {
        internal new readonly VMS.TPS.Common.Model.API.SearchBodyParameters _inner;

        // Store the inner ESAPI object reference
        // internal so other wrappers can access it
        // new to override any inherited _inner fields
        internal new readonly IEsapiService _service;

        public AsyncSearchBodyParameters(VMS.TPS.Common.Model.API.SearchBodyParameters inner, IEsapiService service) : base(inner, service)
        {
            _inner = inner;
            _service = service;

            FillAllCavities = inner.FillAllCavities;
            KeepLargestParts = inner.KeepLargestParts;
            LowerHUThreshold = inner.LowerHUThreshold;
            MREdgeThresholdHigh = inner.MREdgeThresholdHigh;
            MREdgeThresholdLow = inner.MREdgeThresholdLow;
            NumberOfLargestPartsToKeep = inner.NumberOfLargestPartsToKeep;
            PreCloseOpenings = inner.PreCloseOpenings;
            PreCloseOpeningsRadius = inner.PreCloseOpeningsRadius;
            PreDisconnect = inner.PreDisconnect;
            PreDisconnectRadius = inner.PreDisconnectRadius;
            Smoothing = inner.Smoothing;
            SmoothingLevel = inner.SmoothingLevel;
        }


        public Task LoadDefaultsAsync() => _service.PostAsync(context => _inner.LoadDefaults());

        public bool FillAllCavities { get; private set; }
        public async Task SetFillAllCavitiesAsync(bool value)
        {
            FillAllCavities = await _service.PostAsync(context => 
            {
                _inner.FillAllCavities = value;
                return _inner.FillAllCavities;
            });
        }

        public bool KeepLargestParts { get; private set; }
        public async Task SetKeepLargestPartsAsync(bool value)
        {
            KeepLargestParts = await _service.PostAsync(context => 
            {
                _inner.KeepLargestParts = value;
                return _inner.KeepLargestParts;
            });
        }

        public int LowerHUThreshold { get; private set; }
        public async Task SetLowerHUThresholdAsync(int value)
        {
            LowerHUThreshold = await _service.PostAsync(context => 
            {
                _inner.LowerHUThreshold = value;
                return _inner.LowerHUThreshold;
            });
        }

        public int MREdgeThresholdHigh { get; private set; }
        public async Task SetMREdgeThresholdHighAsync(int value)
        {
            MREdgeThresholdHigh = await _service.PostAsync(context => 
            {
                _inner.MREdgeThresholdHigh = value;
                return _inner.MREdgeThresholdHigh;
            });
        }

        public int MREdgeThresholdLow { get; private set; }
        public async Task SetMREdgeThresholdLowAsync(int value)
        {
            MREdgeThresholdLow = await _service.PostAsync(context => 
            {
                _inner.MREdgeThresholdLow = value;
                return _inner.MREdgeThresholdLow;
            });
        }

        public int NumberOfLargestPartsToKeep { get; private set; }
        public async Task SetNumberOfLargestPartsToKeepAsync(int value)
        {
            NumberOfLargestPartsToKeep = await _service.PostAsync(context => 
            {
                _inner.NumberOfLargestPartsToKeep = value;
                return _inner.NumberOfLargestPartsToKeep;
            });
        }

        public bool PreCloseOpenings { get; private set; }
        public async Task SetPreCloseOpeningsAsync(bool value)
        {
            PreCloseOpenings = await _service.PostAsync(context => 
            {
                _inner.PreCloseOpenings = value;
                return _inner.PreCloseOpenings;
            });
        }

        public double PreCloseOpeningsRadius { get; private set; }
        public async Task SetPreCloseOpeningsRadiusAsync(double value)
        {
            PreCloseOpeningsRadius = await _service.PostAsync(context => 
            {
                _inner.PreCloseOpeningsRadius = value;
                return _inner.PreCloseOpeningsRadius;
            });
        }

        public bool PreDisconnect { get; private set; }
        public async Task SetPreDisconnectAsync(bool value)
        {
            PreDisconnect = await _service.PostAsync(context => 
            {
                _inner.PreDisconnect = value;
                return _inner.PreDisconnect;
            });
        }

        public double PreDisconnectRadius { get; private set; }
        public async Task SetPreDisconnectRadiusAsync(double value)
        {
            PreDisconnectRadius = await _service.PostAsync(context => 
            {
                _inner.PreDisconnectRadius = value;
                return _inner.PreDisconnectRadius;
            });
        }

        public bool Smoothing { get; private set; }
        public async Task SetSmoothingAsync(bool value)
        {
            Smoothing = await _service.PostAsync(context => 
            {
                _inner.Smoothing = value;
                return _inner.Smoothing;
            });
        }

        public int SmoothingLevel { get; private set; }
        public async Task SetSmoothingLevelAsync(int value)
        {
            SmoothingLevel = await _service.PostAsync(context => 
            {
                _inner.SmoothingLevel = value;
                return _inner.SmoothingLevel;
            });
        }

        public Task RunAsync(Action<VMS.TPS.Common.Model.API.SearchBodyParameters> action) => _service.PostAsync((context) => action(_inner));
        public Task<T> RunAsync<T>(Func<VMS.TPS.Common.Model.API.SearchBodyParameters, T> func) => _service.PostAsync<T>((context) => func(_inner));
    }
}
