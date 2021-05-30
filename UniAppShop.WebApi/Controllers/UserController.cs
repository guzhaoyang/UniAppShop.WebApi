using Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UniAppShop.WebApi.Models;
using ViewModels.User;

namespace UniAppShop.WebApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class UserController : BaseController
    {
        private readonly ILogger<UserController> _logger;

        public UserController(ILogger<UserController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <returns></returns>
        [HttpPost("GetUserInfo")]
        public BaseResponse GetUserInfo()
        {
            try
            {
                //获取用户信息
                var Id = UserID;
                GetUserInfoResponse response = new GetUserInfoResponse()
                {
                    userHeadimg = "/static/logo.png",
                    userName = "古兆洋",
                    gender = "男",
                    phone = "13140167510"
                };
                return BaseResponse.ToResponse<GetUserInfoResponse>(BackResult.Success, data: response);
            }
            catch(Exception exc)
            {
                return BaseResponse.ToResponse(BackResult.Exception, message: exc.Message);
            }
        }

        /// <summary>
        /// 更新用户信息
        /// </summary>
        /// <returns></returns>
        [HttpPost("UpdateUserInfo")]
        public BaseResponse UpdateUserInfo(UpdateUserInfoRequest request)
        {
            try
            {
                //获取用户信息
                var Id = UserID;
                if(request == null || string.IsNullOrEmpty(request.userName))
                {
                    return BaseResponse.ToResponse(BackResult.Error, message: "参数解析错误或用户名不能为空");
                }

                //如果用户存在，就更新用户信息

                return BaseResponse.ToResponse(BackResult.Success, message:"修改用户信息成功");
            }
            catch (Exception exc)
            {
                return BaseResponse.ToResponse(BackResult.Exception, message: exc.Message);
            }
        }
    }
}
