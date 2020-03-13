using Maydear.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MaydearMvcSample.Controllers
{
    [Route("[Controller]")]
    public class AuthController : ControllerBase
    {
        [HttpPost]
        public Task<string> Login(string value)
        {
            return HttpContext.SignInAsync(value);
        }

        [HttpGet("list")]
        [AllowAnonymous]
        public string List(string value)
        {
            return value;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        public Task<string> Get()
        {
            return HttpContext.GetAccessTokenValueAsync();
        }
    }
}
