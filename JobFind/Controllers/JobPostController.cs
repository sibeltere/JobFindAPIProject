using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JobFind.BusinessLayer.Abstracts;
using JobFind.Constants;
using JobFind.DataLayer.DTOModels.Request;
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
        private readonly IUserService _userService;
        private readonly IMemoryCache _memCache;
        #endregion

        #region CTOR
        public JobPostController(IJobPostService jobPostService, IUserService userService, IMemoryCache memCache)
        {
            this._jobPostService = jobPostService;
            this._userService = userService;
            this._memCache = memCache;
        }
        #endregion

        #region Methods

        [HttpPost("ApplyJobPost")]
        public IActionResult ApplyJobPost(ApplyJobPostDTO model)
        {
            var user = _userService.GetUserById(model.UserId);
            if (string.IsNullOrEmpty(user.Id))
            {
                return OK(StatusCodeType.USER_NOTFOUND, StatusMessage.USER_NOTFOUND, false);
            }

            var jobPost = _jobPostService.GetJobPostById(model.JobPostId);
            if (string.IsNullOrEmpty(jobPost.Id))
            {
                return OK(StatusCodeType.JOBPOST_NOTFOUND, StatusMessage.JOBPOST_NOTFOUND, false);
            }

            var hasApplyUser = _jobPostService.AnyApplyUser(model);
            if (hasApplyUser)
                return OK(StatusCodeType.ALREADY_HASJOBPOST, StatusMessage.ALREADY_HASJOBPOST, false);

            var response = _jobPostService.ApplyJobPost(model);
            if (!response)
                return OK(StatusCodeType.HAS_EXCEPTION, StatusMessage.HAS_EXCEPTION, response);

            return OK(StatusCodeType.SUCCESS, StatusMessage.SUCCESS, response);
        }


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
