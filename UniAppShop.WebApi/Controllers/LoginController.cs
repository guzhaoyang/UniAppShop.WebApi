using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using UniAppShop.WebApi.Models;
using ViewModels.Login;
using Common;
using Microsoft.AspNetCore.Authorization;
using UniAppShop.IService;

namespace UniAppShop.WebApi.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("[controller]")]
    public class LoginController : BaseController
    {
        private readonly ILogger<LoginController> _logger;
        private readonly ILoginService _loginService;

        public LoginController(ILogger<LoginController> logger,
            ILoginService loginService)
        {
            _logger = logger;
            _loginService = loginService;
        }

        [HttpGet("login")]
        public BaseResponse Login([FromQuery]LoginRequest request)
        {
            try
            {
                #region 参数值校验

                if (request == null || string.IsNullOrEmpty(request.phone))
                {
                    return BaseResponse.ToResponse(BackResult.Error, message: "参数校验失败或手机号不能为空");
                }
                if (string.IsNullOrEmpty(request.password))
                {
                    return BaseResponse.ToResponse(BackResult.Error, message: "密码不能为空");
                }
                #endregion
                var login = _loginService.GetLogin(request.phone, request.password);
                if(login == null)
                {
                    return BaseResponse.ToResponse(BackResult.Fail, "用户名或密码错误");
                }
                //if (request.phone != "admin" || request.password != "123456")
                //{
                //    return BaseResponse.ToResponse(BackResult.Fail, "用户名或密码错误");
                //}
                LoginResponse loginResponse = new LoginResponse()
                {
                    userName = login.Name
                };

                return BaseResponse.ToResponse<LoginResponse>(BackResult.Success, loginResponse, "登录成功");
            }
            catch(Exception exc)
            {
                return BaseResponse.ToResponse(BackResult.Exception, exc.Message);
            }
        }

        [HttpPost("login")]
        public BaseResponse LoginPost([FromBody]LoginRequest request)
        {
            try
            {
                BaseResponse response = new BaseResponse() { code = (int)BackResult.Success, data = request.name, message = "Post登录成功" };
                return response;
            }
            catch(Exception exc)
            {
                return BaseResponse.ToResponse(BackResult.Exception, exc.Message);
            }
        }
    }
}
