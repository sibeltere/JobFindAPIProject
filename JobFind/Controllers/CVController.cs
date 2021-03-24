using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JobFind.BusinessLayer.Abstracts;
using JobFind.Constants;
using JobFind.Controllers;
using JobFind.DataLayer.DTOModels.Request;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class CVController : BaseApiController
{
    #region Fields
    private readonly IUserService _userService;
    private readonly ICVService _cvService;
    #endregion

    #region CTOR
    public CVController(IUserService userService, ICVService cvService)
    {
        this._userService = userService;
        this._cvService = cvService;
    }
    #endregion

    #region Methods
    [HttpPost("CreateCV")]
    public IActionResult CreateCV(CVDTO model)
    {
        var user = _userService.GetUserById(model.UserId);
        if (string.IsNullOrEmpty(user.Id))
        {
            return OK(StatusCodeType.USER_NOTFOUND, StatusMessage.USER_NOTFOUND, false);
        }

        if (user.ResponseCVDTO != null)
        {
            return OK(StatusCodeType.ALREADY_HASCV, StatusMessage.ALREADY_HASCV, false);
        }

        var response = _cvService.CreateCV(model);
        if (response==null)
            return OK(StatusCodeType.HAS_EXCEPTION, StatusMessage.HAS_EXCEPTION, response);

        return OK(StatusCodeType.SUCCESS, StatusMessage.SUCCESS, response);
    }


    [HttpPost("UpdateCV")]
    public IActionResult UpdateCV(UpdateCVDTO model)
    {
        var response = _cvService.UpdateCV(model);
        if (response == null)
            return OK(StatusCodeType.HAS_EXCEPTION, StatusMessage.HAS_EXCEPTION, response);

        return OK(StatusCodeType.SUCCESS, StatusMessage.SUCCESS, response);
    }
    #endregion

}
