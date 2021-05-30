using System;
using System.Collections.Generic;
using System.Text;

namespace ViewModels.Login
{
    public class LoginResponse
    {       
        /// <summary>
        /// 用户ID
        /// </summary>
        public int userId { get; set; }

        /// <summary>
        /// 用户名称
        /// </summary>
        public string userName { get; set; }

        /// <summary>
        /// 姓名
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// 联系电话
        /// </summary>
        public string phone { get; set; }

        public string token { get; set; }

        public string expiration { get; set; }
    }
}
