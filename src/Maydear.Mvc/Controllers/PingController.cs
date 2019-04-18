using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Maydear.Mvc.Controllers
{
    /// <summary>
    /// 服务监听
    /// </summary>
    [Route("Ping")]
    [AllowAnonymous]
    public class PingController : ControllerBase
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public OkObjectResult Get()
        {
            return new OkObjectResult("OK");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpHead]
        public OkResult Head()
        {
            return new OkResult();
        }
    }
}
