using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace UniAppShop.WebApi.Controllers
{
    /// <summary>
    /// ControllerBase控制器的封装
    /// </summary>
    public class BaseController : ControllerBase
    {
        /// <summary>
        /// 登录人的用户ID
        /// </summary>
        protected int UserID
        {
            get
            {
                var userIDString = User.Claims.SingleOrDefault(s => s.Type == "Id")?.Value;
                if (string.IsNullOrEmpty(userIDString))
                {
                    throw new Exception("没有用户ID，请确认用户是否登录");
                }
                else
                {
                    var result = int.TryParse(userIDString, out int userID);
                    if (result)
                    {
                        return userID;
                    }
                    else
                    {
                        throw new Exception("用户ID为非整型，请确认");
                    }
                }
            }
        }
    }
}
