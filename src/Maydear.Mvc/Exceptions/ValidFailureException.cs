using Maydear.Exceptions;
using System;

namespace Maydear.Mvc.Exceptions
{
    /// <summary>
    /// 不良请求，Validation验证不通过时触发得异常
    /// </summary>
    [Serializable]
    public class ValidFailureException : PackageObjectException
    {
        /// <summary>
        /// 无参数构造函数
        /// </summary>
        public ValidFailureException()
            : base((int)Mvc.StatusCode.ValidFailure, Mvc.StatusCode.ValidFailure.GetDescription()) { }

        /// <summary>
        /// 传递错误信息参数构造函数
        /// </summary>
        /// <param name="notification">错误信息</param>
        public ValidFailureException(string notification)
            : base((int)Mvc.StatusCode.ValidFailure, notification) { }
    }
}
