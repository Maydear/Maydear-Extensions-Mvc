using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MaydearMvcSample.Controllers
{
    [Route("[Controller]")]
    public class ExtController : ControllerBase
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public object Get()
        {
            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet("xml")]
        public IActionResult GetList()
        {
            return Content("<xml>user</xml>", "text/xml");
        }
    }
}
