using System;
using Maydear.Exceptions;

namespace Maydear.Mvc.Exceptions
{
    /// <summary>
    /// 当前请求需要用户验证异常
    /// </summary>
    [Serializable]
    public class UnAuthorizedException : StatusCodeException
    {
        /// <summary>
        /// 无参数构造函数
        /// </summary>
        public UnAuthorizedException()
            : base((int)Maydear.Mvc.StatusCode.UnAuthorized, Maydear.Mvc.StatusCode.UnAuthorized.GetDescription()) { }
    }
}
