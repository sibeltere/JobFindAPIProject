using JobFind.BusinessLayer.Abstracts;
using JobFind.Constants;
using JobFind.DataLayer.DTOModels;
using JobFind.DataLayer.DTOModels.Request;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

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
        [HttpPost("CreateUser")]
        public IActionResult CreateUser(UserDTO model)
        {
            var user = _userService.GetUserByEmail(model.Email);
            if (user != null)
            {
                return OK(StatusCodeType.ALREADY_HASEMAIL, StatusMessage.ALREADY_HASEMAIL,false);
            }
            var response = _userService.CreateUser(model);
            if (!response)
                return OK(StatusCodeType.HAS_EXCEPTION, StatusMessage.HAS_EXCEPTION, response);

            return OK(StatusCodeType.SUCCESS, StatusMessage.SUCCESS, response);
        }

        [HttpGet("GetAllUser")]
        public async Task<IActionResult> GetAllUser()
        {
            var response = await _userService.GetAllUser();
            if (response == null)
                return OK(StatusCodeType.HAS_EXCEPTION, StatusMessage.HAS_EXCEPTION, response);

            return OK(StatusCodeType.SUCCESS, StatusMessage.SUCCESS, response);
        }

        #endregion
    }
}
