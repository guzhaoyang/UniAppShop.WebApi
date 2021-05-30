using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UniAppShop.WebApi.Models
{
    public class BaseResponse
    {
        public int code { get; set; }

        public dynamic data { get; set; }

        public string message { get; set; }

        public BaseResponse()
        {

        }

        public static BaseResponse ToResponse<T>(BackResult backResult, T data = default, string message = "")
        {
            return new BaseResponse() { code = (int)1, data = data, message = message };
        }

        public static BaseResponse ToResponse(BackResult backResult, string message)
        {
            return new BaseResponse() { code = (int)backResult, data = default, message = message };
        }
    }
}
