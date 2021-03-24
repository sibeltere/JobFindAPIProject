using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JobFind.BusinessLayer.Abstracts;
using JobFind.Constants;
using JobFind.DataLayer.DTOModels;
using JobFind.DataLayer.DTOModels.Request;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace JobFind.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FirmController : BaseApiController
    {
        #region Fields
        private readonly IFirmService _firmService;
        private readonly IMemoryCache _memCache;
        #endregion

        #region CTOR
        public FirmController(IFirmService firmService, IMemoryCache memCache)
        {
            this._firmService = firmService;
            this._memCache = memCache;
        }
        #endregion

        #region Methods
        [HttpPost("CreateFirm")]
        public IActionResult CreateFirm(FirmDTO model)
        {
            var response = _firmService.CreateFirm(model);
            if (string.IsNullOrEmpty(response.Id))
                return OK(StatusCodeType.HAS_EXCEPTION, StatusMessage.HAS_EXCEPTION, response);

            return OK(StatusCodeType.SUCCESS, StatusMessage.SUCCESS, response);
        }

        [HttpGet("GetAllFirm")]
        public async Task<IActionResult> GetAllFirm()
        {
            const string cacheKey = "allFirmList";

            bool isCached = _memCache.TryGetValue(cacheKey, out object list);
            if (isCached)
                return OK(StatusCodeType.SUCCESS, StatusMessage.SUCCESS, list);

            var allFirm = await _firmService.GetAllFirm();
            if (allFirm == null)
                return OK(StatusCodeType.HAS_EXCEPTION, StatusMessage.HAS_EXCEPTION, false);

            _memCache.Set(cacheKey, allFirm, new MemoryCacheEntryOptions
            {
                AbsoluteExpiration = DateTime.Now.AddSeconds(10), //10 saniye boyunca cacheden okur
                Priority = CacheItemPriority.Normal
            });

            return OK(StatusCodeType.SUCCESS, StatusMessage.SUCCESS, allFirm);
        }

        [HttpPost("AddJobPost")]
        public IActionResult AddJobPost(JobPostDTO model)
        {
            var firm = _firmService.GetFirmById(model.FirmId);
            if (string.IsNullOrEmpty(firm.Id))
            {
                return OK(StatusCodeType.FIRM_NOTFOUND, StatusMessage.FIRM_NOTFOUND, false);
            }

            var response = _firmService.AddFirmJobPost(model);
            if (response == null)
                return OK(StatusCodeType.HAS_EXCEPTION, StatusMessage.HAS_EXCEPTION, false);

            return OK(StatusCodeType.SUCCESS, StatusMessage.SUCCESS, response);
        }


        [HttpPost("DeleteFirm")]
        public IActionResult DeleteFirm(string firmId)
        {
            var response = _firmService.DeleteFirm(firmId);
            if (!response)
                return OK(StatusCodeType.HAS_EXCEPTION, StatusMessage.HAS_EXCEPTION, response);

            return OK(StatusCodeType.SUCCESS, StatusMessage.SUCCESS, response);
        }

        [HttpPost("UpdateFirm")]
        public IActionResult UpdateFirm(UpdateFirmDTO model)
        {
            var firm = _firmService.GetFirmById(model.Id);
            if (string.IsNullOrEmpty(firm.Id))
            {
                return OK(StatusCodeType.FIRM_NOTFOUND, StatusMessage.FIRM_NOTFOUND, false);
            }

            var response = _firmService.UpdateFirm(model);
            if (response == null)
                return OK(StatusCodeType.HAS_EXCEPTION, StatusMessage.HAS_EXCEPTION, response);

            return OK(StatusCodeType.SUCCESS, StatusMessage.SUCCESS, response);
        }
        #endregion
    }
}
