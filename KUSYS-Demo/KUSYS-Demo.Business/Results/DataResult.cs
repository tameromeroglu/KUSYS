using KUSYS_Demo.Business.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KUSYS_Demo.Business.Results
{
    public class DataResult<T>
    {
        public DataResult(ResultStatusEnum resultStatus, T data)
        {
            ResultStatus = resultStatus;
            Data = data;
        }
        public DataResult(ResultStatusEnum resultStatus, string message, T data)
        {
            ResultStatus = resultStatus;
            Message = message;
            Data = data;
        }
        public DataResult(ResultStatusEnum resultStatus, string message, T data, Exception exception)
        {
            ResultStatus = resultStatus;
            Message = message;
            Data = data;
            Exception = exception;
        }
        public ResultStatusEnum ResultStatus { get; }
        public string Message { get; }
        public Exception Exception { get; }
        public T Data { get; }
    }
}
