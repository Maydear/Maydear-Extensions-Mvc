using Maydear.Exceptions;
using System;

namespace Maydear.Mvc.Exceptions
{
    /// <summary>
    /// 系统错误
    /// </summary>
    public class ErrorException : StatusCodeException
    {
        /// <summary>
        /// 无参构造函数
        /// </summary>
        public ErrorException()
            : base((int)Mvc.StatusCode.Error, Mvc.StatusCode.Error.GetDescription()) { }

        /// <summary>
        /// 带异常的构造函数
        /// </summary>
        /// <param name="excep"></param>
        public ErrorException(Exception excep)
            : base((int)Mvc.StatusCode.Error, excep.Message, excep) { }
    }
}
