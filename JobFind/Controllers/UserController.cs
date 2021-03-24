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
        /// <summary>
        /// Yeni Kullanıcı Oluşturur
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("CreateUser")]
        public IActionResult CreateUser(UserDTO model)
        {
            var user = _userService.GetUserByEmail(model.Email);
            if (!string.IsNullOrEmpty(user.Id))
            {
                return OK(StatusCodeType.ALREADY_HASEMAIL, StatusMessage.ALREADY_HASEMAIL, false);
            }

            var response = _userService.CreateUser(model);
            if (string.IsNullOrEmpty(response.Id))
                return OK(StatusCodeType.HAS_EXCEPTION, StatusMessage.HAS_EXCEPTION, false);

            return OK(StatusCodeType.SUCCESS, StatusMessage.SUCCESS, response);
        }

        /// <summary>
        /// Kullanıcı Siler
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpPost("DeleteUser")]
        public IActionResult DeleteUser(string userId)
        {
            var response = _userService.DeleteUser(userId);
            if (!response)
                return OK(StatusCodeType.HAS_EXCEPTION, StatusMessage.HAS_EXCEPTION, response);

            return OK(StatusCodeType.SUCCESS, StatusMessage.SUCCESS, response);
        }

        /// <summary>
        /// Kullanıcı Günceller
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("UpdateUser")]
        public IActionResult UpdateUser(UpdateUserDTO model)
        {
            var user = _userService.GetUserByEmail(model.Email);
            if (!string.IsNullOrEmpty(user.Id))
            {
                return OK(StatusCodeType.ALREADY_HASEMAIL, StatusMessage.ALREADY_HASEMAIL, false);
            }

            var response = _userService.UpdateUser(model);
            if (response == null)
                return OK(StatusCodeType.HAS_EXCEPTION, StatusMessage.HAS_EXCEPTION, response);

            return OK(StatusCodeType.SUCCESS, StatusMessage.SUCCESS, response);
        }

        /// <summary>
        /// Tüm kullanıcıları çeker
        /// </summary>
        /// <returns></returns>
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
                AbsoluteExpiration = DateTime.Now.AddSeconds(10), //10 saniye boyunca cacheden okur
                Priority = CacheItemPriority.Normal
            });

            return OK(StatusCodeType.SUCCESS, StatusMessage.SUCCESS, allUser);
        }

        #endregion
    }
}
