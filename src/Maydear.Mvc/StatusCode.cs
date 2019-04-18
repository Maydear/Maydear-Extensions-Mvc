using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Maydear.Mvc
{
    /// <summary>
    /// 状态码
    /// </summary>
    public enum StatusCode
    {
        /// <summary>
        /// 正常
        /// </summary>
        [Description("正常")]
        Normal = 2000,

        /// <summary>
        /// 参数验证失败，实体属性验证条件不符合。
        /// </summary>
        [Description("参数验证失败。")]
        ValidFailure = 2400,

        /// <summary>
        /// 当前请求需要用户验证
        /// </summary>
        [Description("当前请求需要用户验证")]
        UnAuthorized = 2401,

        /// <summary>
        /// 当前用户不满足访问条件
        /// </summary>
        [Description("当前用户不满足访问条件")]
        Forbidden = 2403,

        /// <summary>
        /// 请求所希望得到的资源未被在服务器上发现
        /// </summary>
        [Description("请求所希望得到的资源未被在服务器上发现")]
        NotFound = 2404,

        /// <summary>
        /// 系统异常
        /// </summary>
        [Description("系统异常.")]
        Error = 99999
    }
}
