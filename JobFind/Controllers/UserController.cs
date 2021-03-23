using JobFind.BusinessLayer.Abstracts;
using JobFind.Constants;
using JobFind.DataLayer.DTOModels;
using JobFind.DataLayer.DTOModels.Request;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System;
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
        private readonly ICVService _cvService;
        private readonly IJobPostService _jobPostService;
        private readonly IMemoryCache _memCache;
        #endregion

        #region CTOR
        public UserController(IUserService userService, ICVService cvService, IJobPostService jobPostService, IMemoryCache memCache)
        {
            this._userService = userService;
            this._cvService = cvService;
            this._memCache = memCache;
            this._jobPostService = jobPostService;
        }
        #endregion

        #region Methods
        [HttpPost("CreateUser")]
        public IActionResult CreateUser(UserDTO model)
        {
            var user = _userService.GetUserByEmail(model.Email);
            if (!string.IsNullOrEmpty(user.Id))
            {
                return OK(StatusCodeType.ALREADY_HASEMAIL, StatusMessage.ALREADY_HASEMAIL, false);
            }

            var response = _userService.CreateUser(model);
            if (!response)
                return OK(StatusCodeType.HAS_EXCEPTION, StatusMessage.HAS_EXCEPTION, response);

            return OK(StatusCodeType.SUCCESS, StatusMessage.SUCCESS, response);
        }

        [HttpPost("DeleteUser")]
        public IActionResult DeleteUser(string userId)
        {
            var response = _userService.DeleteUser(userId);
            if (!response)
                return OK(StatusCodeType.HAS_EXCEPTION, StatusMessage.HAS_EXCEPTION, response);

            return OK(StatusCodeType.SUCCESS, StatusMessage.SUCCESS, response);
        }

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
            if (!response)
                return OK(StatusCodeType.HAS_EXCEPTION, StatusMessage.HAS_EXCEPTION, response);

            return OK(StatusCodeType.SUCCESS, StatusMessage.SUCCESS, response);
        }

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

        [HttpGet("GetAllUser")]
        public async Task<IActionResult> GetAllUser()
        {
            const string cacheKey = "allUserList";

            bool isCached = _memCache.TryGetValue(cacheKey, out object list);
            if (isCached)
                return OK(StatusCodeType.SUCCESS, StatusMessage.SUCCESS, list);

            var allUser = await _userService.GetAllUser();
            if (allUser == null)
                return OK(StatusCodeType.HAS_EXCEPTION, StatusMessage.HAS_EXCEPTION, allUser);

            _memCache.Set(cacheKey, allUser, new MemoryCacheEntryOptions
            {
                AbsoluteExpiration = DateTime.Now.AddMinutes(2), //iki dakika boyunca cacheden okur
                Priority = CacheItemPriority.Normal
            });

            return OK(StatusCodeType.SUCCESS, StatusMessage.SUCCESS, allUser);
        }

        #endregion
    }
}
