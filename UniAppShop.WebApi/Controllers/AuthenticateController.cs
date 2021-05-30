using Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using UniAppShop.WebApi.Models;
using ViewModels.Login;

namespace UniAppShop.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthenticateController : BaseController
    {
        private readonly ILogger<AuthenticateController> _logger;

        public AuthenticateController(ILogger<AuthenticateController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        [Route("login")]
        public BaseResponse Login([FromBody] LoginRequest request)
        {
            //从数据库验证用户名，密码 
            //验证通过 否则 返回Unauthorized

            int userId = 2;
            string userName = request.phone;

            //创建claim
            var authClaims = new[] {
                new Claim(JwtRegisteredClaimNames.Sub,"Sub"),
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
                new Claim("Id",userId.ToString()),
                new Claim("userName",userName)
            };
            IdentityModelEventSource.ShowPII = true;
            //签名秘钥 可以放到json文件中
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("SDMC-CJAS1-SAD-DFSFA-SADHJVF-VF"));

            var token = new JwtSecurityToken(
                   issuer: "http://localhost:54644",
                   audience: "http://localhost:54644",
                   expires: DateTime.Now.AddSeconds(2),
                   claims: authClaims,
                   signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                   );

            //返回token和过期时间
            //return Ok(new
            //{
            //    token = new JwtSecurityTokenHandler().WriteToken(token),
            //    expiration = token.ValidTo
            //});
            LoginResponse loginResponse = new LoginResponse()
            {
                userId = userId,
                userName = userName,
                token = new JwtSecurityTokenHandler().WriteToken(token),
                expiration = token.ValidTo.ToString("yyyy-MM-dd HH:mm:ss")
            };
            return BaseResponse.ToResponse<LoginResponse>(BackResult.Success, data: loginResponse, message:"登录成功");
        }
    }
}
