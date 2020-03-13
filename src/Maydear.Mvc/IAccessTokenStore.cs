using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Maydear.Mvc
{
    /// <summary>
    /// 访问令牌临时存储接口
    /// </summary>
    public interface IAccessTokenStore
    {
        /// <summary>
        /// 存储
        /// </summary>
        /// <param name="tokenValue">存储到token的值</param>
        /// <returns>返回AccessToken键</returns>
        Task<string> StoreAsync(string tokenValue);

        /// <summary>
        /// 更新/续订
        /// </summary>
        /// <param name="key">AccessToken键</param>
        /// <param name="tokenValue">更新的AccessToken值</param>
        /// <returns>返回异步任务</returns>
        Task RenewAsync(string key, string tokenValue);

        /// <summary>
        /// 取回AccessToken的值
        /// </summary>
        /// <param name="key">AccessToken键</param>
        /// <returns></returns>
        Task<string> RetrieveAsync(string key);

        /// <summary>
        /// 移除指定的访问令牌
        /// </summary>
        /// <param name="key">访问令牌键</param>
        /// <returns></returns>
        Task RemoveAsync(string key);
    }
}
