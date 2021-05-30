using Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UniAppShop.WebApi.Models;
using ViewModels.Message;

namespace UniAppShop.WebApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class MessageController : BaseController
    {
        private readonly ILogger<LoginController> _logger;

        public MessageController(ILogger<LoginController> logger)
        {
            _logger = logger;
        }

        [HttpPost("GetMessageList")]
        public BaseResponse GetMessageList(MessageListRequest request)
        {
            try
            {
                //获取用户信息
                var Id = UserID;

                List<MessageListResponse> list = new List<MessageListResponse>()
                {
                    new MessageListResponse { text = "送你一杯咖啡", time = "2021-05-11 19:56", title = "您送出的免费咖啡，好友已成功领取品尝！"},
                    new MessageListResponse { text = "您的咖啡已在赴约途中", time = "2021-05-11 19:56", title = "您的咖啡已在赴约途中，预计20：10与您见面。"},
                };
                return BaseResponse.ToResponse<List<MessageListResponse>>(BackResult.Success, data: list);
            }
            catch (Exception exc)
            {
                return BaseResponse.ToResponse(BackResult.Exception, exc.Message);
            }
        }
    }
}
