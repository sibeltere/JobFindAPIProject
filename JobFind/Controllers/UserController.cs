using JobFind.BusinessLayer.Abstracts;
using JobFind.Constants;
using JobFind.DataLayer.DTOModels;
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
        [HttpPost("CreateUser")]
        public IActionResult CreateUser(UserDTO model)
        {
            var response = _userService.CreateUser(model);
            if(!response)
                return OK(StatusCodeType.HAS_EXCEPTION, StatusMessage.HAS_EXCEPTION, response);

            return OK(StatusCodeType.SUCCESS, StatusMessage.SUCCESS, response);
        }
        #endregion
    }
}
