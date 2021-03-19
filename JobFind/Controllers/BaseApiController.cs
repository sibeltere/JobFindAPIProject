using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JobFind.Constants;
using JobFind.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JobFind.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseApiController : ControllerBase
    {
        [NonAction]
        public IActionResult OK(StatusCodeType statusCodeType, string statusMessage, object result = null)
        {
            var model = new ResponseModel<object>
            {
                StatusCode = (int)statusCodeType,
                StatusMessage = statusMessage,
                Result = result
            };

            return Ok(model);
        }
    }
}
