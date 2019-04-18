using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Maydear.Mvc
{
    /// <summary>
    /// 包裹对象结果
    /// </summary>
    public class PackageObjectResult : ObjectResult
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="packageObject">包裹对象</param>
        public PackageObjectResult(PackageObject packageObject)
            : base(packageObject)
        {
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="obj">对象结果</param>
        /// <param name="requestId">请求id</param>
        public PackageObjectResult(ObjectResult obj, Guid requestId)
            : this(new PackageObject()
            {
                Body = obj.Value,
                StatusCode = (int)Mvc.StatusCode.Normal,
                RequestId = requestId,
                Now = DateTimeOffset.Now
            })
        {
        }

        /// <summary>
        /// 值
        /// </summary>
        public new PackageObject Value { get; set; }
    }
}
