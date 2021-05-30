using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace ViewModels.UploadFile
{
    public class ChangeHeadimgRequest
    {
        /// <summary>
        /// 头像文件
        /// </summary>
        public IFormFile headingFile { get; set; }
        /// <summary>
        /// 额外数据
        /// </summary>
        public int headingFile_Name_flag { get; set; }
    }
}
