using System;
using System.Threading.Tasks;

namespace Esapi.Services {
    /// <summary>
    /// Internal interface for a "message"
    /// to be processed by the Actor loop.
    /// </summary>
    public interface IActorMessage {
        /// <summary>
        /// The method that executes the work on the ESAPI thread.
        /// </summary>
        Task Process(IEsapiContext context);
    }

    /// <summary>
    /// A concrete message holding a Task to be completed.
    /// </summary>
    internal class ActorMessage<T> : IActorMessage {
        private readonly TaskCompletionSource<T> _tcs;
        private readonly Func<IEsapiContext, T> _work;

        public ActorMessage(Func<IEsapiContext, T> work, TaskCompletionSource<T> tcs) {
            _work = work;
            _tcs = tcs;
        }

        public Task Process(IEsapiContext context) {
            try {
                var result = _work(context);
                _tcs.SetResult(result);
            } catch (Exception e) {
                _tcs.SetException(e);
            }
            return Task.CompletedTask;
        }
    }

    /// <summary>
    /// A concrete message for a void (Task-only) action.
    /// </summary>
    internal class ActorActionMessage : IActorMessage {
        private readonly TaskCompletionSource<object> _tcs;
        private readonly Action<IEsapiContext> _work;

        public ActorActionMessage(Action<IEsapiContext> work, TaskCompletionSource<object> tcs) {
            _work = work;
            _tcs = tcs;
        }

        public Task Process(IEsapiContext context) {
            try {
                _work(context);
                _tcs.SetResult(null);
            } catch (Exception e) {
                _tcs.SetException(e);
            }
            return Task.CompletedTask;
        }
    }

}
