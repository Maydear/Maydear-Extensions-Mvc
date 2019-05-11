using Maydear.Exceptions;
using System;

namespace Maydear.Mvc.Exceptions
{
    /// <summary>
    /// 当前用户不满足访问条件的异常
    /// </summary>
    [Serializable]
    public class ForbiddenException : StatusCodeException
    {
        /// <summary>
        /// 无参数构造函数
        /// </summary>
        public ForbiddenException()
            : base((int)Mvc.StatusCode.Forbidden, Mvc.StatusCode.Forbidden.GetDescription()) { }

        /// <summary>
        /// 带异常的构造函数
        /// </summary>
        /// <param name="excep"></param>
        public ForbiddenException(Exception excep)
            : base((int)Mvc.StatusCode.Forbidden, excep.Message, excep) { }
    }
}
