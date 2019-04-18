using Maydear.Exceptions;
using System;
using System.Runtime.Serialization;

namespace Maydear.Mvc.Exceptions
{
    /// <summary>
    /// 业务错误类：数据业务逻辑不符
    /// </summary>
    [Serializable]
    public class PackageObjectException : MaydearException
    {
        /// <summary>
        /// 错误码
        /// </summary>
        public int StatusCode { get; set; }

        /// <summary>
        /// 错误信息
        /// </summary>
        public string Notification { get; set; }

        /// <summary>
        /// 初始化<see cref="PackageObjectException"/>类的新实例。
        /// </summary>
        /// <remarks>
        /// 本构造函数使用框架预设<para>通用状态码</para>信息.
        /// </remarks>
        /// <param name="statusCode">服务状态码</param>
        /// <param name="notification">提示内容</param>
        public PackageObjectException(int statusCode, string notification)
            : base($"{statusCode}::{notification}") { }

        /// <summary>
        /// 使用指定<see cref="System.Exception"/>的异常对象初始化<see cref="PackageObjectException"/>类的新实例。
        /// </summary>
        /// <remarks>
        /// 本构造函数使用框架预设<para>通用状态码</para>信息.并且以<see cref="System.Exception"/>作为参数。
        /// </remarks>
        /// <param name="statusCode">服务状态码</param>
        /// <param name="notification">提示内容</param>
        /// <param name="innerException">
        /// 导致当前异常的异常，如果<see cref="System.Exception"/> 参数不为空引用，则在处理内部异常的 catch 块中引发当前异常。
        /// </param>
        public PackageObjectException(int statusCode, string notification, Exception innerException)
            : base($"{statusCode}::{notification}", innerException) { }



        /// <summary>
        /// 用序列化数据初始化<see cref="PackageObjectException"/>类的新实例。
        /// </summary>
        /// <remarks>
        /// 在反序列化过程中调用该构造函数来重建通过流传输的异常对象.
        /// </remarks>
        /// <param name="info">保存序列化对象<see cref="System.Runtime.Serialization.SerializationInfo"/>数据的对象。</param>
        /// <param name="context">有关源或目标的上下文信息。</param>
        protected PackageObjectException(SerializationInfo info, StreamingContext context) : base(info, context) { }

    }
}
