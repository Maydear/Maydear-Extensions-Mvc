using System;
using Maydear.Exceptions;

namespace Maydear.Mvc.Exceptions
{
    /// <summary>
    /// 请求所希望得到的资源未被在服务器上发现得异常。
    /// </summary>
    public class NotFoundException : StatusCodeException
    {
        /// <summary>
        /// 无参构造函数
        /// </summary>
        public NotFoundException()
            : base((int)Mvc.StatusCode.NotFound, Mvc.StatusCode.Forbidden.GetDescription()) { }

        /// <summary>
        /// 带资源类型的构造函数
        /// </summary>
        /// <param name="type"></param>
        public NotFoundException(Type type)
            : base((int)Mvc.StatusCode.Forbidden, $"未找到类型为\"{type.Name}\"的资源。") { }
    }
}
