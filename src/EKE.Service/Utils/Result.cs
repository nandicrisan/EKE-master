using System;

namespace EKE.Service.Utils
{
    /// <summary>
    /// Status for the result of an operation
    /// </summary>
   // [Serializable]
    public enum ResultStatus
    {
        EXCEPTION = 500,
        ERROR = 502,
        OK = 200,
        WARNING = 1,
        INVALID_PRECOND = -3,
        UNPROCESSABLE_ENTITY = 422,

        NOT_FOUND = 60,
        AXAPTA_FOUND_ONLY = 62,

        PRICE_ZERO = 120,
        FAILED_TO_ADD = 121,
        EMPTY = 122,

        ALREADYEXISTS = 123,
        INACTIVE = 124
    }

    /// <summary>
    /// Result class to be used on non data retrieving methods
    /// </summary>
    //[Serializable]
    public class Result
    {
        #region Properties
        public ResultStatus Status { get; set; }
        public string Message { get; set; }
        public string StatusText { get; set; }
        #endregion

        public Result(ResultStatus status)
        {
            Status = status;
            StatusText = Status.ToString();
        }

        public Result()
            : this(ResultStatus.OK)
        {
        }

        public Result(ResultStatus status, string message)
            : this(status)
        {
            Message = message;
        }

        public Result(Exception ex)
            : this(ResultStatus.EXCEPTION, ex.Message)
        {
        }

        public bool IsOk()
        {
            return Status == ResultStatus.OK;
        }
    }

    /// <summary>
    /// Template result for any operation
    /// </summary>
    /// <typeparam name="T"></typeparam>
    //[Serializable]
    public class Result<T>
    {
        #region Properties
        public T Data { get; set; }
        public ResultStatus Status { get; set; }
        public string Message { get; set; }
        public string StatusText { get; set; }
        #endregion

        public Result(ResultStatus status)
        {
            Status = status;
            StatusText = Status.ToString();
        }

        public Result()
            : this(ResultStatus.OK)
        {
        }

        public Result(ResultStatus status, string message)
            : this(status)
        {
            Message = message;
        }

        public Result(ResultStatus status, string message, T data)
            : this(status, message)
        {
            Data = data;
        }

        public Result(T data)
            : this(ResultStatus.OK)
        {
            Data = data;
        }

        public Result(Exception ex)
            : this(ResultStatus.EXCEPTION, ex.Message)
        {
        }

        public bool IsOk()
        {
            return ResultStatus.OK == Status;
        }
    }
}
