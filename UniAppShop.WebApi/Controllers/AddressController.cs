using Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UniAppShop.WebApi.Models;
using ViewModels.Address;

namespace UniAppShop.WebApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class AddressController : BaseController
    {
        private readonly ILogger<AddressController> _logger;

        public AddressController(ILogger<AddressController> logger)
        {
            _logger = logger;
        }

        #region uniApp接口

        /// <summary>
        /// 获取用户的地址列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("GetUserAddressList")]
        public BaseResponse GetUserAddressList(GetUserAddressListRequest request)
        {
            try
            {
                int id = UserID;
                //判断用户是否存在

                List<GetUserAddressListResponse> list = new List<GetUserAddressListResponse>() 
                {
                    //new GetUserAddressListResponse{ linkman = "gu" + id, address = "gu", addressType = "", isDefault = new List<string>(){ "default"}, phone = "1", sex = "先生", tower = "5号203室"}
                };
                return BaseResponse.ToResponse<List<GetUserAddressListResponse>>(BackResult.Success, data: list);
            }
            catch(Exception exc)
            {
                return BaseResponse.ToResponse(BackResult.Exception, message: exc.Message);
            }
        }

        #endregion
    }
}
