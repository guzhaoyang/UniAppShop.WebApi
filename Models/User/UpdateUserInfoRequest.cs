using System;
using System.Collections.Generic;
using System.Text;

namespace ViewModels.User
{
    public class UpdateUserInfoRequest
    {
        /// <summary>
        /// 用户头像
        /// </summary>
        public string userHeadimg { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        public string userName { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        public string gender { get; set; }

        /// <summary>
        /// 绑定手机
        /// </summary>
        public string phone { get; set; }
    }
}
