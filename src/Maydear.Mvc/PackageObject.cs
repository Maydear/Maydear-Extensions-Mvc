using System;
using System.Collections.Generic;
using System.Text;

namespace Maydear.Mvc
{
    /// <summary>
    /// 
    /// </summary>
    public class PackageObject : IPackageObject<object>
    {
        /// <summary>
        /// 请求识别号
        /// </summary>
        public Guid RequestId { get; set; }

        /// <summary>
        /// 现在的时间
        /// </summary>
        public DateTimeOffset Now { get; set; } = DateTimeOffset.Now;

        /// <summary>
        /// 状态码
        /// </summary>
        public int StatusCode { get; set; }

        /// <summary>
        /// 消息提示
        /// </summary>
        public string Notification { get; set; }

        /// <summary>
        /// 数据内容
        /// </summary>
        public object Body { get; set; }
    }
}
