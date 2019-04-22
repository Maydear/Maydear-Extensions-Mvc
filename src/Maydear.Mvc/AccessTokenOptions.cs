using System;
using System.Collections.Generic;
using System.Text;

namespace Maydear.Mvc
{
    /// <summary>
    /// 访问令牌的配置对象
    /// </summary>
    public class AccessTokenOptions
    {
        /// <summary>
        /// 生存时间
        /// </summary>
        public long Expires { get; set; }
    }
}
