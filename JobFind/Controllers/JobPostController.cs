using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JobFind.BusinessLayer.Abstracts;
using JobFind.Constants;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace JobFind.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobPostController : BaseApiController
    {
        #region Fields
       
        private readonly IJobPostService _jobPostService;
        private readonly IMemoryCache _memCache;
        #endregion

        #region CTOR
        public JobPostController(IJobPostService jobPostService, IMemoryCache memCache)
        {
            this._jobPostService = jobPostService;
            this._memCache = memCache;
        }
        #endregion

        #region Methods
        [HttpGet("GetAllApplyByJobPost")]
        public async Task<IActionResult> GetAllApplyByJobPost(string jobPostId)
        {
            const string cacheKey = "allApplyList";

            bool isCached = _memCache.TryGetValue(cacheKey, out object list);
            if (isCached)
                return OK(StatusCodeType.SUCCESS, StatusMessage.SUCCESS, list);

            var allApply = await _jobPostService.GetAllApplyByJobPost(jobPostId);
            if (allApply == null)
                return OK(StatusCodeType.HAS_EXCEPTION, StatusMessage.HAS_EXCEPTION, false);

            _memCache.Set(cacheKey, allApply, new MemoryCacheEntryOptions
            {
                AbsoluteExpiration = DateTime.Now.AddSeconds(10), //10 saniye boyunca cacheden okur
                Priority = CacheItemPriority.Normal
            });

            return OK(StatusCodeType.SUCCESS, StatusMessage.SUCCESS, allApply);
        }
        #endregion
    }
}
