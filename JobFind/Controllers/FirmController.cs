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
    public class FirmController : BaseApiController
    {
        #region Fields
        private readonly IFirmService _firmService;
        #endregion

        #region CTOR
        public FirmController(IFirmService firmService)
        {
            this._firmService = firmService;
        }
        #endregion

        #region Methods
        [HttpPost("CreateFirm")]
        public IActionResult CreateFirm(FirmDTO model)
        {
            var response = _firmService.CreateFirm(model);
            if (!response)
                return OK(StatusCodeType.HAS_EXCEPTION, StatusMessage.HAS_EXCEPTION, response);

            return OK(StatusCodeType.SUCCESS, StatusMessage.SUCCESS, response);
        }
        #endregion
    }
}
