using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JobFind.BusinessLayer.Abstracts;
using JobFind.Constants;
using JobFind.DataLayer.DTOModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JobFind.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : BaseApiController
    {

        #region Fields
        private readonly IUserService _userService;
        #endregion

        #region CTOR
        public UserController(IUserService userService)
        {
            this._userService = userService;
        }
        #endregion

        #region Methods
        [HttpPost("AddUser")]
        public IActionResult AddUser(UserDTO model)
        {
            _userService.AddUser(model);
            return OK(StatusCodeType.SUCCESS, StatusMessage.SUCCESS, true);
        }
        #endregion
    }
}
