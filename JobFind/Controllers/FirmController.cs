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
        private readonly IJobPostService _jobPostService;
        private readonly IMemoryCache _memCache;
        #endregion

        #region CTOR
        public FirmController(IFirmService firmService, IJobPostService jobPostService, IMemoryCache memCache)
        {
            this._firmService = firmService;
            this._jobPostService = jobPostService;
            this._memCache = memCache;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Yeni bir firma oluşturur.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("CreateFirm")]
        public IActionResult CreateFirm(FirmDTO model)
        {
            var response = _firmService.CreateFirm(model);
            if (string.IsNullOrEmpty(response.Id))
                return OK(StatusCodeType.HAS_EXCEPTION, StatusMessage.HAS_EXCEPTION, response);

            return OK(StatusCodeType.SUCCESS, StatusMessage.SUCCESS, response);
        }

        /// <summary>
        /// Tüm firmaları çeker
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// Firma için ilan oluşturur.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("AddFirmJobPost")]
        public IActionResult AddFirmJobPost(JobPostDTO model)
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

        /// <summary>
        /// Firmanın ilanını günceller
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("UpdateFirmJobPost")]
        public IActionResult UpdateFirmJobPost(UpdateJobPostDTO model)
        {
            var jobPost = _jobPostService.GetJobPostById(model.Id);
            if (string.IsNullOrEmpty(jobPost.Id))
            {
                return OK(StatusCodeType.JOBPOST_NOTFOUND, StatusMessage.JOBPOST_NOTFOUND, false);
            }

            var firm = _firmService.GetFirmById(model.FirmId);
            if (string.IsNullOrEmpty(firm.Id))
            {
                return OK(StatusCodeType.FIRM_NOTFOUND, StatusMessage.FIRM_NOTFOUND, false);
            }

            var response = _firmService.UpdateFirmJobPost(model);
            if (response == null)
                return OK(StatusCodeType.HAS_EXCEPTION, StatusMessage.HAS_EXCEPTION, false);

            return OK(StatusCodeType.SUCCESS, StatusMessage.SUCCESS, response);
        }

        /// <summary>
        /// Firma siler
        /// </summary>
        /// <param name="firmId"></param>
        /// <returns></returns>
        [HttpPost("DeleteFirm")]
        public IActionResult DeleteFirm(string firmId)
        {
            var response = _firmService.DeleteFirm(firmId);
            if (!response)
                return OK(StatusCodeType.HAS_EXCEPTION, StatusMessage.HAS_EXCEPTION, response);

            return OK(StatusCodeType.SUCCESS, StatusMessage.SUCCESS, response);
        }

        /// <summary>
        /// Firma bilgilerini günceller
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
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
