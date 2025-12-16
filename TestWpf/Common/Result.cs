using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestWpf.Common
{
    public class Error
    {
        public string Message { get; }
        public Exception Exception { get; }

        public Error(string message, Exception exception = null)
        {
            Message = message;
            Exception = exception;
        }

        public static readonly Error None = new Error(string.Empty);
        public static implicit operator Error(string message) => new Error(message);
    }

    public class Result
    {
        // -- Properties -- //
        public bool IsSuccess { get; } = false;
        public bool IsFailure => !IsSuccess;
        public bool IsError => Errors.Any();

        public string Messages => String.Join("\r\n", Errors.Select(x => x.Message));

        public List<Error> Errors { get; } = new List<Error>();

        // -- Constructors -- //
        private Result(bool success) { IsSuccess = success; }
        private Result(List<Error> errors) => Errors = errors;

        // -- Factories -- //
        public static Result Pass() => new Result(true);
        public static Result Fail(string errorMessage) => new Result(new List<Error>() { errorMessage });
        public static Result Fail(Error error) => new Result(new List<Error>() { error });
        public static Result Fail(List<Error> errors) => new Result(errors);

        // Helper to wrap dangerous code (e.g. IO)
        public static Result Try(Func<Result> action)
        {
            try
            {
                return action();
            }
            catch (Exception ex)
            {
                return Result.Fail(new Error($"Action failed: {ex.Message}", ex));
            }
        }

        public static implicit operator Result(Error error) => Fail(error); // returning an error assumes it's a failure
        public static implicit operator Result(string errorMessage) => Fail(new List<Error>() { errorMessage });
    }

    /// <summary>
    /// Result of a complex operation
    /// </summary>
    public sealed class Result<T>
    {
        public T Value { get; }

        // -- Properties -- //
        public bool IsSuccess { get; } = false;
        public bool IsFailure => !IsSuccess;
        public bool IsError => Errors.Any();
        public string Messages => String.Join("\r\n", Errors.Select(x => x.Message));
        public List<Error> Errors { get; } = new List<Error>();

        // -- Constructors -- //
        private Result(T value) => (IsSuccess, Value) = (true, value);
        private Result(List<Error> errors) => (IsSuccess, Errors) = (false, errors);

        // -- Factories -- //
        public static Result<T> Pass(T value) => new Result<T>(value);
        public static Result<T> Fail(string errorMessage) => new Result<T>(new List<Error>() { errorMessage });
        public static Result<T> Fail(Error error) => new Result<T>(new List<Error>() { error });
        public static Result<T> Fail(List<Error> errors) => new Result<T>(errors);

        // Helper to wrap dangerous code (e.g. IO)
        public static Result<T> Try(Func<Result<T>> action)
        {
            try
            {
                return action();
            }
            catch (Exception ex)
            {
                return new Error($"Action failed: {ex.Message}", ex);
            }
        }

        public static implicit operator Result<T>(T data) => Pass(data); // returning the value assumes it's successful
        public static implicit operator Result<T>(Error error) => Fail(error); // returning an error assumes it's a failure
        public static implicit operator Result<T>(string errorMessage) => Fail(new List<Error>() { errorMessage });
    }
}
